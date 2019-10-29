using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLayer.Entities;

namespace BusinessLayer.Services.Interfaces
{
    public interface IWorkingTimeRepository
    {
        Task<IEnumerable<WorkingTime>> GetByFieldId(long fieldId);
        void UpdateFieldWorkingTime(long fieldId, IEnumerable<WorkingTime> currentWorkingTime,
            IEnumerable<WorkingTime> newWorkingTime);
        Task<IEnumerable<WorkingTime>> DeleteByFieldId(long fieldId);
    }
}