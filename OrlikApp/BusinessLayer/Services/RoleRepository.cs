using BusinessLayer.Contexts;
using BusinessLayer.Entities;
using BusinessLayer.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class RoleRepository : IRoleRepository
    {
        private readonly OrlikAppContext _context;
        private readonly ILogger<RoleRepository> _logger;

        public RoleRepository(OrlikAppContext context, ILogger<RoleRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Role>> GetAll()
        {
            return await _context.Roles.AsNoTracking().ToListAsync();
        }
    }
}
