using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class House
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public int NumberOfRooms { get; set; }
        public double Price { get; set; }
        // validation
        public bool IsLargeHouse()
        {
            return NumberOfRooms > 5;
        }
    }
}
