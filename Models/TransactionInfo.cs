using System;
using carShop.Dtos;

namespace carShop.Models
{
    public class TransactionInfo
    {
        public CarDto Car { get; set; }
        public string CustomerName { get; set; }
        public bool Success { get; set; } = true;
        public string Seller => Car.OwnerName;
        public double Price => Car.Price;
        public DateTimeOffset DateTime => DateTimeOffset.UtcNow;
    }
}