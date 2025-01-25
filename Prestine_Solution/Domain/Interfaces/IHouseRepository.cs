using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IHouseRepository
    {
        Task<IEnumerable<House>> GetAllAsync();
        Task<House> GetByIdAsync(int id);
        Task AddAsync(House house);
        Task UpdateAsync(House house);
        Task DeleteAsync(int id);
    }
}
