using MediatR;
using Microservices.CQRS.Example.CQRS.Commands.Responses;

namespace Microservices.CQRS.Example.CQRS.Commands.Requests
{
    public class DeleteProductCommandRequest : IRequest<DeleteProductCommandResponse>
    {
        public Guid ProductId { get; set; }
    }
}
