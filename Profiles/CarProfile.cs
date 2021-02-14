using AutoMapper;
using carShop.Dtos;
using carShop.Models;

namespace carShop.Profiles
{
    public class CarProfile : Profile
    {
        public CarProfile()
        {
            CreateMap<Car, CarDto>();
            CreateMap<CarDto, Car>();

            CreateMap<Car, CarCreateDto>();
            CreateMap<CarCreateDto, Car>();

            CreateMap<Car, CarUpdateDto>();
            CreateMap<CarUpdateDto, Car>();
        }
    }
}