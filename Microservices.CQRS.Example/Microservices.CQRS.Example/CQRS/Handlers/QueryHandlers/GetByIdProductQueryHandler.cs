using MediatR;
using Microservices.CQRS.Example.CQRS.Queries.Requests;
using Microservices.CQRS.Example.CQRS.Queries.Responses;
using Microservices.CQRS.Example.Models;

namespace Microservices.CQRS.Example.CQRS.Handlers.QueryHandlers
{
    public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQueryRequest, GetByIdProductQueryResponse>
    {
        public Task<GetByIdProductQueryResponse> Handle(GetByIdProductQueryRequest request, CancellationToken cancellationToken)
        {
            var product = ApplicationDbContext.Products.FirstOrDefault(p => p.Id == request.ProductId);

            return product == null
                ? throw new Exception("Ürün yok")
                : Task.FromResult(new GetByIdProductQueryResponse
                {
                    Id = product.Id,
                    Name = product.Name,
                    Quantity = product.Quantity,
                    Price = product.Price,
                    CreateTime = product.CreateTime
                });
        }
    }
}
