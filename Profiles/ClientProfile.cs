using AutoMapper;
using carShop.Dtos;
using carShop.Models;

namespace carShop.Profiles
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<Client, ClientDto>();
            CreateMap<ClientDto, Client>();

            CreateMap<Client, ClientCreateDto>();
            CreateMap<ClientCreateDto, Client>();
        }
    }
}