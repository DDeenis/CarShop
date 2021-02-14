using carShop.Dtos;
using MediatR;

namespace carShop.Infrastructure.Queries
{
    public class UpdateCarQuery : IRequest<Unit>
    {
        public UpdateCarQuery(CarUpdateDto car)
        {
            Car = car ?? throw new System.ArgumentNullException(nameof(car));
        }

        public CarUpdateDto Car { get; }
    }
}