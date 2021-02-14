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
    public class PatchCarQueryHandler : IRequestHandler<PatchCarQuery, Unit>
    {
        private readonly IRepositoryManager repository;
        private readonly IMapper mapper;

        public PatchCarQueryHandler(IRepositoryManager repository, IMapper mapper)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public Task<Unit> Handle(PatchCarQuery request, CancellationToken cancellationToken)
        {
            if(request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var carFromRepository = repository.Cars.Get(c => c.Id == request.CarId);

            if(carFromRepository is null)
            {
                return Task.FromCanceled<Unit>(new CancellationToken(canceled: true));
            }

            var carToPatch = mapper.Map<CarUpdateDto>(carFromRepository);

            request.JsonPatchDocument.ApplyTo(carToPatch);

            mapper.Map(carToPatch, carFromRepository);

            repository.Cars.Update(carFromRepository);
            repository.SaveChanges();

            return Task.FromResult(new Unit());
        }
    }
}