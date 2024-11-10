using MediatR;
using Microservices.CQRS.Example.CQRS.Commands.Requests;
using Microservices.CQRS.Example.CQRS.Commands.Responses;
using Microservices.CQRS.Example.Models;

namespace Microservices.CQRS.Example.CQRS.Handlers.CommandHandlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, CreateProductCommandResponse>
    {
        public Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var id = Guid.NewGuid();

            ApplicationDbContext.Products.Add(new()
            {
                Id = id,
                Name = request.Name,
                Quantity = request.Quantity,
                Price = request.Price,
                CreateTime = DateTime.Now,
            });

            return Task.FromResult(new CreateProductCommandResponse
            {
                ProductId = id,
                IsSuccess = true,
            });
        }
    }
}
