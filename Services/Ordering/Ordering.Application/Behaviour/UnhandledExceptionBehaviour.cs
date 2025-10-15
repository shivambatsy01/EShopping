using MediatR;
using Microsoft.Extensions.Logging;

namespace Ordering.Application.Behaviour;

/// <summary>
/// A MediatR pipeline behavior that catches all unhandled exceptions
/// Logs the detail and throw the exception again
/// thrown during the request handling process and logs them.
/// </summary>
/// <typeparam name="TRequest">The request type (command or query).</typeparam>
/// <typeparam name="TResponse">The expected response type.</typeparam>
public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly ILogger<TRequest> _logger;

    /// <summary>
    /// Injects the logger for the given request type.
    /// </summary>
    /// <param name="logger">The logger instance used to log errors.</param>
    public UnhandledExceptionBehaviour(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Wraps request execution in a try-catch block.
    /// Logs any unhandled exception that occurs during processing.
    /// </summary>
    /// <param name="request">The incoming request object.</param>
    /// <param name="next">The next delegate in the MediatR pipeline.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The handler's response, or rethrows the exception after logging.</returns>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            // Proceed to the next behavior or handler
            return await next();
        }
        catch (Exception ex)
        {
            // Log the error details for debugging/troubleshooting
            var requestName = typeof(TRequest).Name;

            _logger.LogError(ex,
                "Unhandled Exception for Request {Name} {@Request}", requestName, request);

            // Optionally, you could handle the exception (e.g., return a custom response)
            // but by default, we rethrow so that global exception middleware can handle it.
            throw;
        }
    }
}