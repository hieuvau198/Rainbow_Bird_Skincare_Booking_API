using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IHouseService
    {
        Task<IEnumerable<HouseDto>> GetAllAsync();
        Task AddAsync(HouseDto houseDto);
        Task UpdateAsync(HouseDto houseDto);
    }
}
