using System.Collections.Generic;
using carShop.Dtos;
using carShop.Models;
using MediatR;

namespace carShop.Infrastructure.Queries
{
    public class GetClientsQuery : IRequest<IEnumerable<ClientDto>>
    {
        public GetClientsQuery(ClientQueryParameters clientQueryParameters)
        {
            ClientQueryParameters = clientQueryParameters;
        }

        public ClientQueryParameters ClientQueryParameters { get; }
    }
}