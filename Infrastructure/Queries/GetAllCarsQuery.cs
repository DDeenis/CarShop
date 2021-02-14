using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using carShop.Dtos;
using carShop.Models;
using MediatR;

namespace carShop.Infrastructure.Queries
{
    public class GetAllCarsQuery : IRequest<IEnumerable<CarDto>>
    {
        public GetAllCarsQuery(bool trackChanges = true)
        {
            TrackChanges = trackChanges;
        }

        public GetAllCarsQuery(Expression<Func<Car, bool>> expression, bool trackChanges = true) : this(trackChanges)
        {
            Expression = expression ?? throw new ArgumentNullException(nameof(expression));
        }

        public bool TrackChanges { get; }
        public Expression<Func<Car, bool>> Expression { get; }
    }
}