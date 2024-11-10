namespace Microservices.CQRS.Example.CQRS.Commands.Responses
{
    public class CreateProductCommandResponse
    {
        public Guid ProductId { get; set; }
        public bool IsSuccess { get; set; }

    }
}
