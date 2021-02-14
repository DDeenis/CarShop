using System;
using System.Collections.Generic;

namespace carShop.Models
{
    public class Client
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public IEnumerable<Car> Cars { get; set; }
    }
}