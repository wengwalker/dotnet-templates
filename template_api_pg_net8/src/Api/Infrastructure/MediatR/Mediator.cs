using Api.Infrastructure.MediatR.Interfaces;
using System.Collections.Concurrent;

namespace Api.Infrastructure.MediatR;

public class Mediator : IMediator
{
    private readonly IServiceProvider _serviceProvider;

    private static readonly ConcurrentDictionary<Type, Type> _requestHandlerTypes = new();

    public Mediator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        var requestType = request.GetType();

        var handlerType = _requestHandlerTypes.GetOrAdd(requestType,
            t => typeof(IRequestHandler<,>).MakeGenericType(t, typeof(TResponse)));

        var handler = _serviceProvider.GetService(handlerType)
            ?? throw new InvalidOperationException($"Handler not found for {requestType.Name}");

        return (Task<TResponse>)handlerType.GetMethod("Handle")!
            .Invoke(handler, [ request, cancellationToken ])!;
    }
}
