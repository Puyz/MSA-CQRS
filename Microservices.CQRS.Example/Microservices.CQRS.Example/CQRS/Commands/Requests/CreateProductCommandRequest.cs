using MediatR;
using Microservices.CQRS.Example.CQRS.Commands.Responses;

namespace Microservices.CQRS.Example.CQRS.Commands.Requests
{
    public class CreateProductCommandRequest : IRequest<CreateProductCommandResponse>
    {
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
