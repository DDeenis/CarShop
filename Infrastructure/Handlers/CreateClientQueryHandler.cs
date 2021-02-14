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
    public class CreateClientQueryHandler : IRequestHandler<CreateClientQuery, ClientDto>
    {
        private readonly IRepositoryManager repository;
        private readonly IMapper mapper;

        public CreateClientQueryHandler(IRepositoryManager repository, IMapper mapper)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public Task<ClientDto> Handle(CreateClientQuery request, CancellationToken cancellationToken)
        {
            if(request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var clientToCreate = mapper.Map<Client>(request.Client);

            repository.Clients.Create(clientToCreate);
            repository.SaveChanges();

            var clientToReturn = mapper.Map<ClientDto>(clientToCreate);

            return Task.FromResult(clientToReturn);
        }
    }
}