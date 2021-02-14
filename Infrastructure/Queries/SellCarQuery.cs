using System;
using carShop.Models;
using MediatR;

namespace carShop.Infrastructure.Queries
{
    public class SellCarQuery : IRequest<TransactionInfo>
    {
        public SellCarQuery(Guid carId, Guid clientId)
        {
            CarId = carId;
            ClientId = clientId;
        }

        public Guid CarId { get; }
        public Guid ClientId { get; }
    }
}