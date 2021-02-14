using System;

namespace carShop.Models
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public double Salary { get; set; }
    }
}