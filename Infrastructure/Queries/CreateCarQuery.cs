using carShop.Dtos;
using carShop.Models;
using MediatR;

namespace carShop.Infrastructure.Queries
{
    public class CreateCarQuery : IRequest<Car>
    {
        public CreateCarQuery(CarCreateDto car)
        {
            Car = car ?? throw new System.ArgumentNullException(nameof(car));
        }

        public CarCreateDto Car { get; }
    }
}