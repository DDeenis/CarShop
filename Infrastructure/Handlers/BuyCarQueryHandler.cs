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
    public class BuyCarQueryHandler : IRequestHandler<BuyCarQuery, TransactionInfo>
    {
        private readonly IRepositoryManager repository;
        private readonly IMapper mapper;

        public BuyCarQueryHandler(IRepositoryManager repository, IMapper mapper)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public Task<TransactionInfo> Handle(BuyCarQuery request, CancellationToken cancellationToken)
        {
            if(request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var carFromRepository = repository.Cars.Get(expression: c => c.Id == request.CarId, trackChanges: true);

            if(carFromRepository is null || carFromRepository.OwnerName.ToLowerInvariant() == "Car Shop".ToLowerInvariant())
            {
                Task.FromCanceled<TransactionInfo>(new CancellationToken(canceled: true));
            }

            carFromRepository.OwnerName = "Car Shop";
            repository.SaveChanges();

            var transactionInfo = new TransactionInfo();
            transactionInfo.Car = mapper.Map<CarDto>(carFromRepository);
            transactionInfo.CustomerName = "Car Shop";

            return Task.FromResult(transactionInfo);
        }
    }
}