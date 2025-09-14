using FluentValidation;
using FluentValidation.Results;

namespace Domain.Validators
{
    public class ValidationBehavior<TRequest, TResponse>(IValidator<TRequest> validator) : 
        IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        public async Task<TResponse> Handle(TRequest request,  RequestHandlerDelegate<TResponse> next)
        {
            if(validator == null) return await next();

            var validationResult = await validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .Select(e => new ValidationFailure(e.PropertyName, e.ErrorMessage))
                    .ToList();
                throw new ValidationException(errors);
            }
            return await next();
        }
    }



    public interface IPipelineBehavior<TRequest, TResponse>
    {
        Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next);
    }

    public delegate Task<TResponse> RequestHandlerDelegate<TResponse>();

}
