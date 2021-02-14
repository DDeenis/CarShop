using System;
using System.Collections.Generic;
using System.Linq;
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
    public class GetCarsByQueryHandler : IRequestHandler<GetCarsByQuery, IEnumerable<CarDto>>
    {
        private readonly IRepositoryManager repository;
        private readonly IMapper mapper;

        public GetCarsByQueryHandler(IRepositoryManager repository, IMapper mapper)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public Task<IEnumerable<CarDto>> Handle(GetCarsByQuery request, CancellationToken cancellationToken)
        {
            if(request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            IQueryable<Car> query = repository.Cars.GetAll(trackChanges: false);

            if(!string.IsNullOrWhiteSpace(request.CarQueryParameters.ModelName))
            {
                string modelName = request.CarQueryParameters.ModelName?.Trim()?.ToLowerInvariant()
                    ?? throw new ArgumentNullException(nameof(request.CarQueryParameters.ModelName));

                query = query.Where(c => c.ModelName.ToLower() == modelName);
            }

            if(!string.IsNullOrWhiteSpace(request.CarQueryParameters.OwnerName))
            {
                string ownerName = request.CarQueryParameters.OwnerName?.Trim()?.ToLowerInvariant()
                    ?? throw new ArgumentNullException(nameof(request.CarQueryParameters.OwnerName));

                query = query.Where(c => c.OwnerName.ToLower() == ownerName);
            }

            var carsToReturn = mapper.Map<IEnumerable<CarDto>>(query.AsEnumerable());

            return Task.FromResult(carsToReturn);
        }
    }
}