using MediatR;
using Microservices.CQRS.Example.CQRS.Commands.Requests;
using Microservices.CQRS.Example.CQRS.Commands.Responses;
using Microservices.CQRS.Example.Models;

namespace Microservices.CQRS.Example.CQRS.Handlers.CommandHandlers
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommandRequest, DeleteProductCommandResponse>
    {
        public Task<DeleteProductCommandResponse> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product = ApplicationDbContext.Products.FirstOrDefault(p => p.Id == request.ProductId);
            if (product != null)
            {
                ApplicationDbContext.Products.Remove(product);
                return Task.FromResult(new DeleteProductCommandResponse() { IsSuccess = true });
            }

            return Task.FromResult(new DeleteProductCommandResponse() { IsSuccess = false });
        }
    }
}
