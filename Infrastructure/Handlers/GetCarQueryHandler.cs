using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using carShop.Data;
using carShop.Dtos;
using carShop.Infrastructure.Queries;
using MediatR;

namespace carShop.Infrastructure.Handlers
{
    public class GetCarQueryHandler : IRequestHandler<GetCarQuery, CarDto>
    {
        private readonly IRepositoryManager repository;
        private readonly IMapper mapper;

        public GetCarQueryHandler(IRepositoryManager repository, IMapper mapper)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        // public GetCarQueryHandler(IRepositoryManager repository, IMapper mapper)
        // {

        // }

        public Task<CarDto> Handle(GetCarQuery request, CancellationToken cancellationToken)
        {
            if(request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var carFromRepository = repository.Cars.Get(expression: c => c.Id == request.CarId,
                                                        trackChanges: request.TrackChanges);

            var carToReturn = mapper.Map<CarDto>(carFromRepository);

            return Task.FromResult(carToReturn);
        }
    }
}