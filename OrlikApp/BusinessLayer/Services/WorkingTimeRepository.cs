﻿using BusinessLayer.Contexts;
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
    }
}