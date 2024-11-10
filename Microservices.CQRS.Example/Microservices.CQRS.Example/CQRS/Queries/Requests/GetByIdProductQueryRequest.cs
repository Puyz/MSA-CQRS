using MediatR;
using Microservices.CQRS.Example.CQRS.Queries.Responses;

namespace Microservices.CQRS.Example.CQRS.Queries.Requests
{
    public class GetByIdProductQueryRequest : IRequest<GetByIdProductQueryResponse>
    {
        public Guid ProductId { get; set; }
    }
}
