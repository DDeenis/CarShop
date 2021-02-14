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
    public class GetAllCarsQueryHandler : IRequestHandler<GetAllCarsQuery, IEnumerable<CarDto>>
    {
        private readonly IRepositoryManager repository;
        private readonly IMapper mapper;

        public GetAllCarsQueryHandler(IRepositoryManager repository, IMapper mapper)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public Task<IEnumerable<CarDto>> Handle(GetAllCarsQuery request, CancellationToken cancellationToken)
        {
            if(request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            IQueryable<Car> query;

            if(request.Expression is null)
            {
                query = repository.Cars.GetAll(request.TrackChanges);
            }
            else
            {
                query = repository.Cars.GetAll(request.Expression, request.TrackChanges);
            }

            var queryToReturn = mapper.Map<IEnumerable<CarDto>>(query.AsEnumerable());

            return Task.FromResult(queryToReturn);
        }
    }
}