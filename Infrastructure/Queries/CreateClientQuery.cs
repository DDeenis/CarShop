using carShop.Dtos;
using MediatR;

namespace carShop.Infrastructure.Queries
{
    public class CreateClientQuery : IRequest<ClientDto>
    {
        public CreateClientQuery(ClientCreateDto client)
        {
            Client = client ?? throw new System.ArgumentNullException(nameof(client));
        }

        public ClientCreateDto Client { get; }
    }
}