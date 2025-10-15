using FluentValidation;
using MediatR;

namespace Ordering.Application.Behaviour;

//Boilerplate code from fluent validations


/// <summary>
/// A MediatR pipeline behavior that automatically runs all FluentValidation validators
/// for a given request before the corresponding handler executes.
/// </summary>
/// <typeparam name="TRequest">The request type (command or query).</typeparam>
/// <typeparam name="TResponse">The expected response type.</typeparam>
public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    
    /// <summary>
    /// Injects all validators registered for the given request type.
    /// </summary>
    /// <param name="validators">A collection of FluentValidation validators.</param>
    public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }
    
    /// <summary>
    /// This method runs before the request handler.
    /// It executes all validators for the request and throws an exception if validation fails.
    /// </summary>
    /// <param name="request">The incoming request object.</param>
    /// <param name="next">The next delegate in the MediatR pipeline.</param>
    /// <param name="cancellationToken">A cancellation token.</param>
    /// <returns>The handler's response, if validation succeeds.</returns>
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        // If there are no validators, just continue down the pipeline.
        if (!_validators.Any())
            return await next();

        // Create a FluentValidation context for the request.
        var context = new ValidationContext<TRequest>(request);

        // Run all validators asynchronously in parallel.
        var validationResults = await Task.WhenAll(
            _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        // Gather all validation failures (if any).
        var failures = validationResults
            .SelectMany(r => r.Errors)
            .Where(f => f != null)
            .ToList();

        // If there are validation errors, throw an exception that will stop the request pipeline.
        if (failures.Count != 0)
            throw new ValidationException(failures);

        return await next();
    }
}