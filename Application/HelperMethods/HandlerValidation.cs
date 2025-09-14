//using FluentValidation;

//namespace Application.HelperMethods
//{
//    public class HandlerValidation
//    {

//        public static void Validate<T, Y>(this IServiceProvider serviceProvider, T request, Y command)
//            where Y : AbstractValidator<T>
//        {
//            //var validator 
//            if (validator == null)
//            {
//                throw new ArgumentNullException(nameof(validator));
//            }
//            var validationResult = await validator.ValidateAsync(request, cancellationToken);

//            if (!validationResult.IsValid)
//            {
//                var errors = validationResult.Errors
//                    .Select(e => new ValidationFailure(e.PropertyName, e.ErrorMessage))
//                    .ToList();
//                throw new ValidationException(errors);
//            }
//            if (request == null)
//            {
//                throw new ArgumentNullException(nameof(request));
//            }
//        }
//    }
//}
