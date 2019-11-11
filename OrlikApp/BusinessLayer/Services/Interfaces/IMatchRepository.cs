using System.Collections.Generic;
using System.Security.Principal;
using System.Threading.Tasks;
using BusinessLayer.Entities;
using BusinessLayer.Helpers.Pagination;
using BusinessLayer.Models.Match;
using Web.Helpers.Pagination;

namespace BusinessLayer.Services.Interfaces
{
    public interface IMatchRepository
    {
        Task<Match> Get(long id);
        Task<Match> GetWithMatchMembers(long id);
        Task<Match> GetWithRelations(long id);
        Task<PagedResult<Match>> GetPagedList(MatchSearch filter, Pager pager, IPrincipal loggedUser);
        Task<Match> Create(Match match, IPrincipal loggedUser);
        Task<Match> Delete(Match match);
        Task<Match> Confirm(long matchId);
    }
}