using System;
using MediatR;

namespace carShop.Infrastructure.Queries
{
    public class DeleteCarQuery : IRequest<Unit>
    {
        public DeleteCarQuery(Guid carId)
        {
            CarId = carId;
        }

        public Guid CarId { get; }
    }
}