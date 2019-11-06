using BusinessLayer.Contexts;
using BusinessLayer.Entities;
using BusinessLayer.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class MatchMemberRepository : IMatchMemberRepository
    {
        private readonly OrlikAppContext _context;
        private readonly ILogger<MatchMemberRepository> _logger;

        public MatchMemberRepository(OrlikAppContext context, ILogger<MatchMemberRepository> logger)
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
    }
}
