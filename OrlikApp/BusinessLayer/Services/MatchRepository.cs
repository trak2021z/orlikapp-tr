using BusinessLayer.Contexts;
using BusinessLayer.Entities;
using BusinessLayer.Helpers;
using BusinessLayer.Helpers.Pagination;
using BusinessLayer.Models.Match;
using BusinessLayer.Models.Role;
using BusinessLayer.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Web.Helpers.Pagination;

namespace BusinessLayer.Services
{
    public class MatchRepository : IMatchRepository
    {
        private readonly OrlikAppContext _context;
        private readonly ILogger<MatchRepository> _logger;
        private readonly IFieldRepository _fieldRepository;
        private readonly IWorkingTimeRepository _workingTimeRepository;
        private readonly IMatchMemberRepository _matchMemberRepository;

        public MatchRepository(OrlikAppContext context, ILogger<MatchRepository> logger,
            IFieldRepository fieldRepository, IWorkingTimeRepository workingTimeRepository,
            IMatchMemberRepository matchMemberRepository)
        {
            _context = context;
            _logger = logger;
            _fieldRepository = fieldRepository;
            _workingTimeRepository = workingTimeRepository;
            _matchMemberRepository = matchMemberRepository;
        }

        #region Get()
        public async Task<Match> Get(long id)
        {
            try
            {
                return await _context.Matches.AsNoTracking()
                    .FirstOrDefaultAsync(u => u.Id == id);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }
        #endregion

        #region GetWithMatchMembers()
        public async Task<Match> GetWithMatchMembers(long id)
        {
            try
            {
                return await _context.Matches.AsNoTracking()
                    .Include(m => m.MatchMembers)
                    .FirstOrDefaultAsync(u => u.Id == id);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }
        #endregion

        #region GetWithRelations()
        public async Task<Match> GetWithRelations(long id)
        {
            try
            {
                return await _context.Matches.AsNoTracking()
                    .Include(m => m.MatchMembers)
                    .Include(m => m.Field)
                    .Include(m => m.Founder)
                    .FirstOrDefaultAsync(u => u.Id == id);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }
        #endregion

        #region GetPagedList()
        public async Task<PagedResult<Match>> GetPagedList(MatchSearch search, Pager pager, IPrincipal loggedUser)
        {
            try
            {
                var query = _context.Matches.AsNoTracking();

                if (search.FieldId.HasValue)
                {
                    query = query.Where(m => m.FieldId == search.FieldId);
                }

                if (loggedUser.IsInRole(RoleNames.User))
                {
                    query = query.Where(m => m.IsConfirmed == true && m.StartDate > DateTime.Now);
                }
                else if (loggedUser.IsInRole(RoleNames.FieldKeeper))
                {
                    query = query.Include(m => m.Field)
                        .Where(m => m.Field.KeeperId == long.Parse(loggedUser.Identity.Name));
                }

                if (!loggedUser.IsInRole(RoleNames.User) && search.OnlyUnconfirmed)
                {
                    query = query.Where(m => m.IsConfirmed == false);
                }

                var queryResultNumber = query.Count();

                query = query.OrderBy(m => m.Id).Skip(pager.Offset).Take(pager.Size);

                var queryResult = await query
                    .Include(m => m.MatchMembers).ThenInclude(mm => mm.Player)
                    .Include(m => m.Field).ThenInclude(f => f.Type)
                    .Include(m => m.Founder)
                    .ToListAsync();

                var result = new PagedResult<Match>
                {
                    Items = queryResult,
                    RowNumber = queryResultNumber
                };

                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }
        #endregion

        #region Create()
        public async Task<Match> Create(Match match, IPrincipal loggedUser)
        {
            try
            {
                var field = await _fieldRepository.Get(match.FieldId);
                var isStartDateValid = 
                    await _workingTimeRepository.IsDateInFieldWorkingTime(field.Id, match.StartDate);

                if (!isStartDateValid)
                {
                    throw new BusinessLogicException("Boisko jest zamknięte podczas wybranej daty rozpoczęcia",
                        (int)MatchError.InvalidStartDate);
                }

                if (field.AutoConfirm)
                {
                    await CheckFieldOccupation(match);
                    match.IsConfirmed = true;
                }
                else
                {
                    match.IsConfirmed = false;
                }

                match.FounderId = long.Parse(loggedUser.Identity.Name);

                _context.Matches.Add(match);
                await _context.SaveChangesAsync();

                return match;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }
        #endregion

        #region Delete()
        public async Task<Match> Delete(Match match)
        {
            try
            {
                await _matchMemberRepository.DeleteByMatchId(match.Id);

                _context.Matches.Remove(match);
                await _context.SaveChangesAsync();

                return match;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }
        #endregion

        #region Confirm()
        public async Task<Match> Confirm(long matchId)
        {
            try
            {
                var match = await _context.Matches.FirstOrDefaultAsync(m => m.Id == matchId);
                if (match.IsConfirmed)
                {
                    throw new BusinessLogicException("Mecz jest już zaakceptowany",
                        (int)MatchError.AlreadyConfirmed);
                }

                await CheckFieldOccupation(match);

                match.IsConfirmed = true;
                await _context.SaveChangesAsync();

                return match;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }
        #endregion

        #region CheckFieldOccupation()
        public async Task CheckFieldOccupation(Match match)
        {
            var conflictedMatch = await _context.Matches.AsNoTracking()
                .FirstOrDefaultAsync(m => m.IsConfirmed && m.FieldId == match.FieldId &&
                    ((m.StartDate <= match.StartDate && m.EndDate > match.StartDate) 
                    || (m.StartDate < match.EndDate && m.EndDate >= match.EndDate)));

            if (conflictedMatch != null)
            {
                string conflictedMatchTimeSpan = conflictedMatch.StartDate.ToString("dd/MM/yyyy H:mm") + " - " +
                    conflictedMatch.EndDate.ToString("dd/MM/yyyy H:mm");

                throw new BusinessLogicException("Wybrany czas koliduje z innym meczem: " +
                    conflictedMatchTimeSpan, (int)MatchError.OccupiedField);
            }
        }
        #endregion
    }
}
