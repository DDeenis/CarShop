using System;
using System.Collections.Generic;
using carShop.Dtos;
using carShop.Models;
using MediatR;

namespace carShop.Infrastructure.Queries
{
    public class GetCarQuery : IRequest<CarDto>
    {
        public GetCarQuery(Guid carId, bool trackChanges = true)
        {
            CarId = carId;
            TrackChanges = trackChanges;
        }

        public Guid CarId { get; set; }
        public bool TrackChanges { get; }
    }
}