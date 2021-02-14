using System;
using System.Collections.Generic;
using carShop.Dtos;
using carShop.Models;
using MediatR;

namespace carShop.Infrastructure.Queries
{
    public class GetCarsByQuery : IRequest<IEnumerable<CarDto>>
    {
        public GetCarsByQuery(CarQueryParameters carQueryParameters)
        {
            CarQueryParameters = carQueryParameters;
        }

        public CarQueryParameters CarQueryParameters { get; }
    }
}