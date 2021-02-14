using System;
using carShop.Dtos;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace carShop.Infrastructure.Queries
{
    public class PatchCarQuery : IRequest<Unit>
    {
        public PatchCarQuery(Guid carId, JsonPatchDocument<CarUpdateDto> jsonPatchDocument)
        {
            CarId = carId;
            JsonPatchDocument = jsonPatchDocument ?? throw new ArgumentNullException(nameof(jsonPatchDocument));
        }

        public Guid CarId { get; }
        public JsonPatchDocument<CarUpdateDto> JsonPatchDocument { get; }
    }
}