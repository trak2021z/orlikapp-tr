using BusinessLayer.Contexts;
using BusinessLayer.Entities;
using BusinessLayer.Helpers;
using BusinessLayer.Models.Match;
using BusinessLayer.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class MatchMemberRepository : IMatchMemberRepository
    {
        private readonly SRBContext _context;
        private readonly ILogger<MatchMemberRepository> _logger;

        public MatchMemberRepository(SRBContext context, ILogger<MatchMemberRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        #region GetByMatchId()
        public async Task<IEnumerable<MatchMember>> GetByMatchId(long matchId)
        {
            try
            {
                return await _context.MatchMembers.AsNoTracking().Where(mm => mm.MatchId == matchId).ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }
        #endregion

        #region UserAlreadyJoinedToMatch()
        public async Task<bool> UserAlreadyJoinedToMatch(long matchId, long userId)
        {
            try
            {
                return await _context.MatchMembers.AsNoTracking()
                    .AnyAsync(mm => mm.MatchId == matchId && mm.PlayerId == userId);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }
        #endregion

        #region DeleteByMatchId()
        public async Task<IEnumerable<MatchMember>> DeleteByMatchId(long matchId)
        {
            var matchMembers = await GetByMatchId(matchId);
            foreach (var matchMember in matchMembers)
            {
                _context.MatchMembers.Remove(matchMember);
                await _context.SaveChangesAsync();
            }

            return matchMembers;
        }
        #endregion

        #region JoinToMatch()
        public async Task<MatchMember> JoinToMatch(long matchId, IPrincipal loggedUser)
        {
            var userId = long.Parse(loggedUser.Identity.Name);

            if (await UserAlreadyJoinedToMatch(matchId, userId))
            {
                throw new BusinessLogicException("Użytkownik już dołączył do wybranego meczu",
                        (int)MatchError.UserAlreadyJoinded);
            }

            await CheckJoiningAbility(matchId, userId);

            var matchMember = new MatchMember
            {
                PlayerId = userId,
                MatchId = matchId,
                JoiningDate = DateTime.Now
            };

            _context.MatchMembers.Add(matchMember);
            await _context.SaveChangesAsync();

            return matchMember;
        }
        #endregion

        #region CheckJoiningAbility()
        private async Task CheckJoiningAbility(long matchId, long userId)
        {
            try
            {
                var match = await _context.Matches.AsNoTracking()
                    .Include(m => m.MatchMembers)
                    .FirstOrDefaultAsync(u => u.Id == matchId);

                if (match.EndOfJoiningDate < DateTime.Now)
                {
                    throw new BusinessLogicException("Czas dołączenia do meczu upłynął",
                        (int)MatchError.JoiningDateExpired);
                }
                else if (match.FounderId == userId)
                {
                    throw new BusinessLogicException("Jesteś już uczestnikiem własnego meczu",
                        (int)MatchError.JoiningToOwnMatch);
                }
                else if (match.WantedPlayersLeftAmmonut < 1)
                {
                    throw new BusinessLogicException("Brak wolnych miejsc w wybranym meczu",
                        (int)MatchError.WantedPlayersLeftAmmountLessThanOne);
                }
                else if (!match.IsConfirmed)
                {
                    throw new BusinessLogicException("Mecz nie został zatwierdzony",
                        (int)MatchError.Unconfirmed);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }
        #endregion
    }
}
