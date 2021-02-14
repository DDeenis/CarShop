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
    public class CreateCarQueryHandler : IRequestHandler<CreateCarQuery, Car>
    {
        private readonly IRepositoryManager repository;
        private readonly IMapper mapper;

        public CreateCarQueryHandler(IRepositoryManager repository, IMapper mapper)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public Task<Car> Handle(CreateCarQuery request, CancellationToken cancellationToken)
        {
            if(request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var carToCreate = mapper.Map<Car>(request.Car);

            repository.Cars.Create(carToCreate);
            repository.SaveChanges();

            // var carToReturn = mapper.Map<CarDto>(carToCreate);

            return Task.FromResult(carToCreate);
        }
    }
}