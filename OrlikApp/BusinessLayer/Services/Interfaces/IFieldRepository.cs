using System.Collections.Generic;
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
        Task<Field> Create(Field field);
        Task<Field> Update(long id, Field field, IEnumerable<WorkingTime> workingTime);
        Task<Field> Delete(Field field);
    }
}