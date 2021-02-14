using carShop.Context;
using carShop.Models;

namespace carShop.Data
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(CarShopContext context) : base(context)
        {
        }
    }
}