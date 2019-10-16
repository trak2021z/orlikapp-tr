﻿using BusinessLayer.Contexts;
using BusinessLayer.Entities;
using BusinessLayer.Helpers.Pagination;
using BusinessLayer.Models.Field;
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

        public FieldRepository(OrlikAppContext context, ILogger<UserRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        #region Get()
        public async Task<Field> Get(long id)
        {
            try
            {
                return await _context.Set<Field>().AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }
        #endregion

        #region GetWithKeeper()
        public async Task<Field> GetWithRelations(long id)
        {
            try
            {
                return await _context.Set<Field>().AsNoTracking().Include(f => f.Keeper).Include(f => f.Type)
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
                var query = _context.Set<Field>().AsNoTracking();

                if (!string.IsNullOrEmpty(search.Street))
                {
                    query = query.Where(f => f.Street.Equals(search.Street));

                    if (search.StreetNumber.HasValue)
                    {
                        query = query.Where(f => f.StreetNumber == search.StreetNumber);
                    }
                }

                query = query.OrderBy(f => f.Id).Skip(pager.Offset).Take(pager.Size);
                var result = await query.Include(f => f.Type).Include(f => f.Keeper).ToListAsync();

                var pagedList = new PagedResult<Field>
                {
                    Items = result,
                    RowNumber = result.Count
                };

                return pagedList;
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