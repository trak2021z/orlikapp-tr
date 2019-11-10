using System.Collections.Generic;
using System.Security.Principal;
using System.Threading.Tasks;
using BusinessLayer.Entities;

namespace BusinessLayer.Services.Interfaces
{
    public interface IMatchMemberRepository
    {
        Task<IEnumerable<MatchMember>> DeleteByMatchId(long matchId);
        Task<IEnumerable<MatchMember>> GetByMatchId(long matchId);
        Task<MatchMember> JoinToMatch(long matchId, IPrincipal loggedUser);
    }
}