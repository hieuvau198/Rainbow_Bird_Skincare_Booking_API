using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class HouseService : IHouseService
    {
        private readonly IHouseRepository _repository;

        public HouseService(IHouseRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<HouseDto>> GetAllAsync()
        {
            var houses = await _repository.GetAllAsync();
            return houses.Select(h => new HouseDto
            {
                Address = h.Address,
                NumberOfRooms = h.NumberOfRooms,
                Price = h.Price
            });
        }

        public async Task AddAsync(HouseDto houseDto)
        {
            var house = new House
            {
                Address = houseDto.Address,
                NumberOfRooms = houseDto.NumberOfRooms,
                Price = houseDto.Price
            };
            await _repository.AddAsync(house);
        }

        public Task UpdateAsync(HouseDto houseDto)
        {
            throw new NotImplementedException();
        }
    }
}
