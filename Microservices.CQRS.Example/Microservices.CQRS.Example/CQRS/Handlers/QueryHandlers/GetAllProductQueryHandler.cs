using MediatR;
using Microservices.CQRS.Example.CQRS.Queries.Requests;
using Microservices.CQRS.Example.CQRS.Queries.Responses;
using Microservices.CQRS.Example.Models;

namespace Microservices.CQRS.Example.CQRS.Handlers.QueryHandlers
{
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest, List<GetAllProductQueryResponse>>
    {
        public Task<List<GetAllProductQueryResponse>> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
        {
            var products = ApplicationDbContext.Products.Select(p => new GetAllProductQueryResponse
            {
                Id = p.Id,
                Name = p.Name,
                Quantity = p.Quantity,
                Price = p.Price,
                CreateTime = p.CreateTime
            }).ToList();
            return Task.FromResult(products);
        }
    }
}
