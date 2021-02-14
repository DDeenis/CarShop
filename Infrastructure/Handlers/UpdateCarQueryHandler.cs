using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using carShop.Data;
using carShop.Infrastructure.Queries;
using carShop.Models;
using MediatR;

namespace carShop.Infrastructure.Handlers
{
    public class UpdateCarQueryHandler : IRequestHandler<UpdateCarQuery, Unit>
    {
        private readonly IRepositoryManager repository;
        private readonly IMapper mapper;

        public UpdateCarQueryHandler(IRepositoryManager repository, IMapper mapper)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public Task<Unit> Handle(UpdateCarQuery request, CancellationToken cancellationToken)
        {
            if(request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var carToUpdate = mapper.Map<Car>(request.Car);

            repository.Cars.Update(carToUpdate);
            repository.SaveChanges();

            return Task.FromResult(new Unit());
        }
    }
}