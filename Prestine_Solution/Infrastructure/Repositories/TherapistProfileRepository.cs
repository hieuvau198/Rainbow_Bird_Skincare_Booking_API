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
    public class TherapistProfileRepository : GenericRepository<TherapistProfile>, ITherapistProfileRepository
    {
        public TherapistProfileRepository(SkincareDbContext context) : base(context)
        {
        }

        public override async Task<TherapistProfile?> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(tp => tp.Therapist)
                .FirstOrDefaultAsync(tp => tp.ProfileId == id);
        }

        public async Task<TherapistProfile?> GetByTherapistIdAsync(int therapistId)
        {
            return await _dbSet
                .Include(tp => tp.Therapist)
                .FirstOrDefaultAsync(tp => tp.TherapistId == therapistId);
        }
    }
}
