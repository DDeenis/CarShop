using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using carShop.Dtos;
using carShop.Infrastructure.Queries;
using carShop.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace carShop.Controllers
{
    [ApiController]
    [Route("api/clients")]
    public class ClientController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IMediator mediator;
        private readonly ILogger<ClientController> logger;

        public ClientController(IMapper mapper, IMediator mediator, ILogger<ClientController> logger)
        {
            this.mapper = mapper;
            this.mediator = mediator;
            this.logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ClientDto>> GetAllClients([FromQuery] ClientQueryParameters clientQueryParameters)
        {
            var clientsToReturn = mediator.Send(new GetClientsQuery(clientQueryParameters)).GetAwaiter().GetResult();

            return Ok(clientsToReturn);
        }

        [HttpPost]
        public ActionResult<ClientDto> CreateClient(ClientCreateDto client)
        {
            var clientToReturn = mediator.Send(new CreateClientQuery(client)).GetAwaiter().GetResult();

            return Ok(clientToReturn);
        }
    }
}