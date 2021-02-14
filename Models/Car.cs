using System;

namespace carShop.Models
{
    public class Car
    {
        public Guid Id { get; set; }
        public string ModelName { get; set; }
        public string OwnerName { get; set; }
        public double MaxSpeed { get; set; }
        public double WorkTime { get; set; }
        public double Price { get; set; }
    }
}