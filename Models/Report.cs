using System.Collections.Generic;
using System.Linq;
using carShop.Dtos;

namespace carShop.Models
{
    public class Report
    {
        public IEnumerable<CarDto> Cars { get; set; }
        public int CarCount => Cars.Count();
    }
}