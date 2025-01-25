using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class HouseRepository : IHouseRepository
    {
        private readonly List<House> _houses = new();
        public Task<IEnumerable<House>> GetAllAsync()
        {
            return Task.FromResult(_houses.AsEnumerable());
        }

        public Task<House> GetByIdAsync(int id)
        {
            var house = _houses.FirstOrDefault(h => h.Id == id);
            return Task.FromResult(house);
        }

        public Task AddAsync(House house)
        {
            _houses.Add(house);
            return Task.CompletedTask;
        }

        // Service Update A
        public Task UpdateAsync(House house)
        {
            var existing = _houses.FirstOrDefault(h => h.Id == house.Id);
            if (existing != null)
            {
                existing.Address = house.Address;
                existing.NumberOfRooms = house.NumberOfRooms;
                existing.Price = house.Price;
            }
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            var house = _houses.FirstOrDefault(h => h.Id == id);
            if (house != null)
            {
                _houses.Remove(house);
            }
            return Task.CompletedTask;
        }
    }
}
