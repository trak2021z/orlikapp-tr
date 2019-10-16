using System.Threading.Tasks;
using BusinessLayer.Entities;
using BusinessLayer.Helpers.Pagination;
using BusinessLayer.Models.Field;
using Web.Helpers.Pagination;

namespace BusinessLayer.Services.Interfaces
{
    public interface IFieldRepository
    {
        Task<Field> Get(long id);
        Task<PagedResult<Field>> GetPagedList(FieldSearch search, Pager pager);
        Task<Field> GetWithRelations(long id);
    }
}