using System.Security.Claims;
using System.Threading.Tasks;
using BusinessLayer.Entities;
using BusinessLayer.Helpers.Pagination;
using BusinessLayer.Models.Match;
using Web.Helpers.Pagination;

namespace BusinessLayer.Services.Interfaces
{
    public interface IMatchRepository
    {
        Task<Match> GetWithRelations(long id);
        Task<PagedResult<Match>> GetPagedList(MatchSearch filter, Pager pager, bool isConfirmed = true);
        Task<Match> Create(Match match, ClaimsPrincipal loggedUser);
        Task<Match> Delete(Match match);
    }
}