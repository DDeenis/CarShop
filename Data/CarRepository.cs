using carShop.Context;
using carShop.Models;

namespace carShop.Data
{
    public class CarRepository : RepositoryBase<Car>, ICarRepository
    {
        public CarRepository(CarShopContext context) : base(context)
        {
        }
    }
}