using MediatR;
using Microservices.CQRS.Example.CQRS.Commands.Requests;
using Microservices.CQRS.Example.CQRS.Queries.Requests;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.CQRS.Example.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromRoute] GetAllProductQueryRequest requestModel)
        {
            return Ok(await _mediator.Send(requestModel));
        }

        [HttpGet("{ProductId}")]
        public async Task<IActionResult> GetById([FromRoute] GetByIdProductQueryRequest requestModel)
        {
            return Ok(await _mediator.Send(requestModel));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductCommandRequest requestModel)
        {
            return Ok(await _mediator.Send(requestModel));
        }

        [HttpDelete("{ProductId}")]
        public async Task<IActionResult> Delete([FromRoute] DeleteProductCommandRequest requestModel)
        {
            return Ok(await _mediator.Send(requestModel));
        }
    }
}
