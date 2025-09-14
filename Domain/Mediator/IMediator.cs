using FluentValidation;

namespace Domain.Mediator
{
    public interface IMediator
    {
        public Task<TResponse> Send<TResponse>(IRequest<TResponse> request);
        //public Task Send(IRequest request);
    }
    public interface IRequest<TResponse> { }
    public interface IRequest { }

    public interface IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken = default);
    }

    //public interface IRequestHandler<TRequest> where TRequest : IRequest
    //{
    //    public Task Handle(TRequest request, CancellationToken cancellationToken = default);
    //}

}
