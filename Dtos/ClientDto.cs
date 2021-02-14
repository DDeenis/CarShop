using System.Collections.Generic;
using carShop.Models;

namespace carShop.Dtos
{
    public class ClientDto
    {
        public string FullName { get; set; }
        public IEnumerable<Car> Cars { get; set; }
    }
}