using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using carShop.Data;
using carShop.Dtos;
using carShop.Infrastructure.Queries;
using carShop.Models;
using MediatR;

namespace carShop.Infrastructure.Handlers
{
    public class SellCarQueryHandler : IRequestHandler<SellCarQuery, TransactionInfo>
    {
        private readonly IRepositoryManager repository;
        private readonly IMapper mapper;

        public SellCarQueryHandler(IRepositoryManager repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public Task<TransactionInfo> Handle(SellCarQuery request, CancellationToken cancellationToken)
        {
            if(request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var carToSell = repository.Cars.Get(c => c.Id == request.CarId, trackChanges: true);
            var client = repository.Clients.Get(c => c.Id == request.ClientId, trackChanges: true);

            if(carToSell is null || client is null)
            {
                return Task.FromCanceled<TransactionInfo>(new CancellationToken(canceled: true));
            }

            var transactionInfo = new TransactionInfo();
            transactionInfo.Car = mapper.Map<CarDto>(carToSell);
            transactionInfo.CustomerName = client.FullName;

            carToSell.OwnerName = client.FullName;
            repository.SaveChanges();

            return Task.FromResult(transactionInfo);
        }
    }
}