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
    public class DeleteCarQueryHandler : IRequestHandler<DeleteCarQuery, Unit>
    {
        private readonly IRepositoryManager repository;
        private readonly IMapper mapper;

        public DeleteCarQueryHandler(IRepositoryManager repository, IMapper mapper)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public Task<Unit> Handle(DeleteCarQuery request, CancellationToken cancellationToken)
        {
            if(request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var carToDelete = repository.Cars.Get(c => c.Id == request.CarId);

            if(carToDelete is null)
            {
                return Task.FromCanceled<Unit>(new CancellationToken(canceled: true));
            }

            repository.Cars.Delete(carToDelete);
            repository.SaveChanges();

            return Task.FromResult(new Unit());
        }
    }
}