using System;
using carShop.Models;
using MediatR;

namespace carShop.Infrastructure.Queries
{
    public class BuyCarQuery : IRequest<TransactionInfo>
    {
        public BuyCarQuery(Guid carId)
        {
            CarId = carId;
        }

        public Guid CarId { get; }
    }
}