﻿using BusinessLayer.Contexts;
using BusinessLayer.Entities;
using BusinessLayer.Helpers;
using BusinessLayer.Helpers.Pagination;
using BusinessLayer.Models.Enums;
using BusinessLayer.Models.Field;
using BusinessLayer.Models.User;
using BusinessLayer.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Helpers.Pagination;

namespace BusinessLayer.Services
{
    public class FieldRepository : IFieldRepository
    {
        private readonly OrlikAppContext _context;
        private readonly ILogger<UserRepository> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IWorkingTimeRepository _workingTimeRepository;

        public FieldRepository(OrlikAppContext context, ILogger<UserRepository> logger,
            IUserRepository userRepository, IWorkingTimeRepository workingTimeRepository)
        {
            _context = context;
            _logger = logger;
            _userRepository = userRepository;
            _workingTimeRepository = workingTimeRepository;
        }

        #region Get()
        public async Task<Field> Get(long id)
        {
            try
            {
                return await _context.Fields.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }
        #endregion

        #region GetWithRelations()
        public async Task<Field> GetWithRelations(long id)
        {
            try
            {
                return await _context.Fields.AsNoTracking()
                    .Include(f => f.Keeper)
                    .Include(f => f.Type)
                    .Include(f => f.WorkingTime)
                    .FirstOrDefaultAsync(u => u.Id == id);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }
        #endregion

        #region GetWithWorkingTime()
        public async Task<Field> GetWithWorkingTime(long id)
        {
            try
            {
                return await _context.Fields.AsNoTracking()
                    .Include(f => f.WorkingTime)
                    .FirstOrDefaultAsync(u => u.Id == id);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }
        #endregion

        #region GetPagedList()
        public async Task<PagedResult<Field>> GetPagedList(FieldSearch search, Pager pager)
        {
            try
            {
                var query = _context.Fields.AsNoTracking();

                if (!string.IsNullOrEmpty(search.Street))
                {
                    query = query.Where(f => f.Street.Equals(search.Street));

                    if (search.StreetNumber.HasValue)
                    {
                        query = query.Where(f => f.StreetNumber == search.StreetNumber);
                    }
                }

                query = query.OrderBy(f => f.Id).Skip(pager.Offset).Take(pager.Size);
                var queryResult = await query.Include(f => f.Type).Include(f => f.Keeper).ToListAsync();

                var result = new PagedResult<Field>
                {
                    Items = queryResult,
                    RowNumber = queryResult.Count
                };

                return result;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }
        #endregion

        #region Create()
        public async Task<Field> Create(Field field)
        {
            try
            {
                if (field.KeeperId.HasValue)
                {
                    await _userRepository.CheckKeeperPermission(field.KeeperId.Value);
                }

                _context.Fields.Add(field);
                await _context.SaveChangesAsync();

                return field;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }
        #endregion

        #region Update()
        public async Task<Field> Update(long id, Field field, IEnumerable<WorkingTime> workingTime)
        {
            try
            {
                if (field.KeeperId.HasValue)
                {
                    await _userRepository.CheckKeeperPermission(field.KeeperId.Value);
                }

                field.Id = id;
                _context.Fields.Update(field);

                var currentWorkingTime = await _workingTimeRepository.GetByFieldId(id);

                foreach (var currentWorkingTimeItem in currentWorkingTime)
                {
                    if (!workingTime.Any(wt => wt.Equals(currentWorkingTimeItem)))
                    {
                        _context.WorkingTimes.Remove(currentWorkingTimeItem);
                    }
                }

                foreach (var workingTimeItem in workingTime)
                {
                    if (!currentWorkingTime.Any(cwt => cwt.Equals(workingTimeItem)))
                    {
                        workingTimeItem.FieldId = id;
                        _context.WorkingTimes.Add(workingTimeItem);
                    }
                }

                await _context.SaveChangesAsync();

                return field;
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }
        #endregion

        #region Delete()
        public async Task<Field> Delete(Field field)
        {
            try
            {
                await _workingTimeRepository.DeleteByFieldId(field.Id);

                _context.Fields.Remove(field);
                await _context.SaveChangesAsync();

                return field;
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
