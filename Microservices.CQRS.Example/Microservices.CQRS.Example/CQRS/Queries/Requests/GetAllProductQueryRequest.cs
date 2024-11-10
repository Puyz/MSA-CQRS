using MediatR;
using Microservices.CQRS.Example.CQRS.Queries.Responses;

namespace Microservices.CQRS.Example.CQRS.Queries.Requests
{
    public class GetAllProductQueryRequest : IRequest<List<GetAllProductQueryResponse>>
    {
    }
}
