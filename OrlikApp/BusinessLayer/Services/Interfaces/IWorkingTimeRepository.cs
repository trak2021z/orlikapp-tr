using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.Entities;

namespace BusinessLayer.Services.Interfaces
{
    public interface IWorkingTimeRepository
    {
        Task<IEnumerable<WorkingTime>> GetByFieldId(long fieldId);
    }
}