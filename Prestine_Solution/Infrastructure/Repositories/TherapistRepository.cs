using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class TherapistRepository : GenericRepository<Therapist>, ITherapistRepository
    {
        public TherapistRepository(SkincareDbContext context) : base(context)
        {
        }

        public override async Task<IEnumerable<Therapist>> GetAllAsync()
        {
            return await _dbSet
                .Include(t => t.User)
                .ToListAsync();
        }

        public override async Task<Therapist?> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(t => t.User)
                .FirstOrDefaultAsync(t => t.TherapistId == id);
        }

        public async Task<Therapist?> GetByUserIdAsync(int userId)
        {
            return await _dbSet
                .Include(t => t.User)
                .FirstOrDefaultAsync(t => t.UserId == userId);
        }

        public async Task<IEnumerable<Therapist>> GetAllWithUserDetailsAsync()
        {
            return await _dbSet
                .Include(t => t.User)
                .ToListAsync();
        }
    }
}
