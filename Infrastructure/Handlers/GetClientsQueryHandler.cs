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
    public class GetClientsQueryHandler : IRequestHandler<GetClientsQuery, IEnumerable<ClientDto>>
    {
        private readonly IRepositoryManager repository;
        private readonly IMapper mapper;

        public GetClientsQueryHandler(IRepositoryManager repository, IMapper mapper)
        {
            this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public Task<IEnumerable<ClientDto>> Handle(GetClientsQuery request, CancellationToken cancellationToken)
        {
            if(request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            IQueryable<Client> query = repository.Clients.GetAll(trackChanges: false);

            if(!string.IsNullOrWhiteSpace(request?.ClientQueryParameters?.FullName))
            {
                string fullName = request.ClientQueryParameters.FullName.Trim().ToLowerInvariant();

                query = query.Where(c => c.FullName.ToLower() == fullName);
            }

            var queryToReturn = mapper.Map<IEnumerable<ClientDto>>(query.AsEnumerable());

            return Task.FromResult(queryToReturn);
        }
    }
}