
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;

namespace Domain.Mediator
{
    //public class Mediator : IMediator
    //{

    //    private readonly IServiceProvider _serviceProvider;

    //    public Mediator(IServiceProvider serviceProvider)
    //    {
    //        _serviceProvider = serviceProvider;
    //    }

    //    public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request)
    //    {
    //        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
    //        var handler = _serviceProvider.GetRequiredService(handlerType);
    //        return await (Task<TResponse>)handlerType
    //            .GetMethod("Handle")
    //            .Invoke(handler, new object[] { request, default(CancellationToken) });
    //    }
    //    public async Task<TResponse> Send<TResponse>(IRequest request)
    //    {
    //        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
    //        var handler = _serviceProvider.GetRequiredService(handlerType);
    //        return await (Task<TResponse>)handlerType
    //            .GetMethod("Handle")
    //            .Invoke(handler, new object[] { request, default(CancellationToken) });
    //    }
    //}
    public class Mediator : IMediator
    {

        private readonly IServiceProvider _serviceProvider;
        //private readonly IValidator _validator;

        public Mediator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            //_validator = validator;
        }

        public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request)
        {
            var handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
            var handler = _serviceProvider.GetRequiredService(handlerType);
            return await (Task<TResponse>)handlerType
               .GetMethod("Handle")
               .Invoke(handler, new object[] { request, default(CancellationToken) });


        }


        //public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request)
        //{

        //    var behaviors = _serviceProvider.GetServices<IPipelineBehavior<IRequest<TResponse>, TResponse>>().Reverse().ToList();

        //    var handler = _serviceProvider.GetRequiredService<IRequestHandler<IRequest<TResponse>, TResponse>>();

        //    RequestHandlerDelegate<TResponse> next = () => handler.Handle(request);

        //    foreach (var behavior in behaviors)
        //    {
        //        var current = next;
        //        next = () => behavior.Handle(request, current);
        //    }

        //    return await next();
        //}

        //public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request)
        //{
        //    var behaviors = _serviceProvider.GetServices<IPipelineBehavior<IRequest<TResponse>, TResponse>>()
        //        .Reverse().ToList();

        //    var handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
        //    var handle = _serviceProvider.GetService(handlerType);

        //    var result = ((Task<TResponse>)handle
        //        .GetType()
        //        .GetMethod("Handle")!
        //        .Invoke(handle, new object[] { request, CancellationToken.None })!)
        //        .GetAwaiter()
        //        .GetResult();

        //    RequestHandlerDelegate<TResponse> next = () => Task.FromResult(result);


        //    foreach (var behavior in behaviors)
        //    {
        //        var current = result;
        //        next = () => behavior.Handle(request, current);
        //    }

        //    return await next();
        //}
        //public async Task Send(IRequest request)
        //{
        //    var handlerType = typeof(IRequestHandler<>).MakeGenericType(request.GetType());
        //    var handler = _serviceProvider.GetRequiredService(handlerType);
        //    await (Task)handlerType
        //       .GetMethod("Handle")
        //       .Invoke(handler, new object[] { request, default(CancellationToken) });
        //}
    }
}
