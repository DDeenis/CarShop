using System;
using System.Collections.Generic;
using AutoMapper;
using carShop.Dtos;
using carShop.Infrastructure.Queries;
using carShop.Models;
using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace carShop.Controllers
{
    [ApiController]
    [Route("api/cars")]
    [EnableCors]
    public class CarController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;
        private readonly ILogger<CarController> logger;

        public CarController(IMediator mediator, IMapper mapper, ILogger<CarController> logger)
        {
            this.mediator = mediator;
            this.mapper = mapper;
            this.logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CarDto>> GetAllCars([FromQuery] CarQueryParameters carQueryParameters)
        {
            var carsToReturn = mediator.Send(new GetCarsByQuery(carQueryParameters)).GetAwaiter().GetResult();

            return Ok(carsToReturn);
        }

        [HttpGet("{carId}", Name="GetCar")]
        public ActionResult<CarDto> GetCar(Guid carId)
        {
            var carToReturn = mediator.Send(new GetCarQuery(carId)).GetAwaiter().GetResult();

            if(carToReturn is null)
            {
                logger.LogWarning($"Car with id {carId} not found");

                return NotFound();
            }

            return Ok(carToReturn);
        }

        [HttpGet]
        [Route("report")]
        public ActionResult<Report> GetReport()
        {
            var report = new Report();

            report.Cars = mediator.Send(new GetAllCarsQuery(expression: c => c.OwnerName.ToLower() == "Car Shop".ToLower(),
                                                            trackChanges: false))
                                                            .GetAwaiter().GetResult();

            return Ok(report);
        }

        [HttpGet]
        [Route("buy")]
        public ActionResult<IEnumerable<CarDto>> GetAvailableCars()
        {
            var carsToReturn = mediator.Send(new GetAllCarsQuery(c => c.OwnerName.ToLower() != "Car Shop".ToLower(), trackChanges: false))
                .GetAwaiter().GetResult();

            return Ok(carsToReturn);
        }

        [HttpPost]
        public ActionResult<CarDto> CreateCar([FromBody] CarCreateDto carCreateDto)
        {
            if(carCreateDto is null)
            {
                return UnprocessableEntity();
            }

            var carFromRepository = mediator.Send(new CreateCarQuery(carCreateDto)).GetAwaiter().GetResult();

            var carToReturn = mapper.Map<CarDto>(carFromRepository);

            return CreatedAtRoute(nameof(GetCar), new { carId = carFromRepository.Id }, carToReturn);
        }

        [HttpPost]
        [Route("buy/{carId}")]
        public ActionResult<TransactionInfo> BuyCar(Guid carId)
        {
            var transactionInfoTask = mediator.Send(new BuyCarQuery(carId));

            if(transactionInfoTask.IsCanceled)
            {
                return BadRequest();
            }

            return Ok(transactionInfoTask.GetAwaiter().GetResult());
        }

        [HttpPost]
        [Route("sell")]
        public ActionResult<TransactionInfo> SellCar([FromQuery] Guid carId, [FromQuery] Guid clientId)
        {
            var transactionInfoTask = mediator.Send(new SellCarQuery(carId, clientId));

            if(transactionInfoTask.IsCanceled)
            {
                return BadRequest();
            }

            return Ok(transactionInfoTask.GetAwaiter().GetResult());
        }

        [HttpPut]
        public ActionResult UpdateCar([FromBody] CarUpdateDto carUpdateDto)
        {
            if(carUpdateDto is null)
            {
                return UnprocessableEntity();
            }

            mediator.Send(new UpdateCarQuery(carUpdateDto));

            return NoContent();
        }

        [HttpPatch("{carId}")]
        public ActionResult PartiallyUpdateCar(Guid carId, JsonPatchDocument<CarUpdateDto> jsonPatchDocument)
        {
            if(jsonPatchDocument is null)
            {
                return BadRequest();
            }

            var task = mediator.Send(new PatchCarQuery(carId, jsonPatchDocument));

            if(task.IsCanceled)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{carId}")]
        public ActionResult DeleteCar(Guid carId)
        {
            var task = mediator.Send(new DeleteCarQuery(carId));

            if(task.IsCanceled)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}