using BusinessLayer.Contexts;
using BusinessLayer.Entities;
using BusinessLayer.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class WorkingTimeRepository : IWorkingTimeRepository
    {
        private readonly OrlikAppContext _context;
        private readonly ILogger<WorkingTimeRepository> _logger;

        public WorkingTimeRepository(OrlikAppContext context, ILogger<WorkingTimeRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        #region GetByFieldId()
        public async Task<IEnumerable<WorkingTime>> GetByFieldId(long fieldId)
        {
            return await _context.WorkingTimes.AsNoTracking().Where(wt => wt.FieldId == fieldId).ToListAsync();
        }
        #endregion

        #region UpdateCollection()
        public void UpdateFieldWorkingTime(long fieldId, IEnumerable<WorkingTime> currentWorkingTime,
            IEnumerable<WorkingTime> newWorkingTime)
        {
            foreach (var currentWorkingTimeItem in currentWorkingTime)
            {
                if (!newWorkingTime.Any(wt => wt.Equals(currentWorkingTimeItem)))
                {
                    _context.WorkingTimes.Remove(currentWorkingTimeItem);
                }
            }

            foreach (var workingTimeItem in newWorkingTime)
            {
                if (!currentWorkingTime.Any(cwt => cwt.Equals(workingTimeItem)))
                {
                    workingTimeItem.FieldId = fieldId;
                    _context.WorkingTimes.Add(workingTimeItem);
                }
            }
        }
        #endregion

        #region DeleteByFieldId()
        public async Task<IEnumerable<WorkingTime>> DeleteByFieldId(long fieldId)
        {
            try
            {
                var workingTime = await GetByFieldId(fieldId);
                foreach (var workingTimeItem in workingTime)
                {
                    _context.Remove(workingTimeItem);
                }
                await _context.SaveChangesAsync();

                return workingTime;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }
        #endregion

        #region IsDateInFieldWorkingTime()
        public async Task<bool> IsDateInFieldWorkingTime(long fieldId, DateTime date)
        {
            var result = await _context.WorkingTimes.AsNoTracking()
                .AnyAsync(wt => wt.FieldId == fieldId
                             && wt.Day == date.DayOfWeek
                             && wt.OpenHour <= date.TimeOfDay
                             && wt.CloseHour >= date.TimeOfDay);

            return result;
        }
        #endregion
    }
}
