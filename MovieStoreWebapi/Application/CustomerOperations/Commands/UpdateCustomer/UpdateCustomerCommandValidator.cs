using FluentValidation;


namespace MovieStoreWebapi.Application.CustomerOperations.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
    {
        public UpdateCustomerCommandValidator()
        {
            RuleFor(command => command.CustomerId).GreaterThan(0);
            RuleFor(command => command.Model.FirstName).NotEmpty().NotNull();
            RuleFor(command => command.Model.LastName).NotEmpty().NotNull();
            RuleFor(command => command.Model.Email).NotEmpty().NotNull();
            RuleFor(command => command.Model.Password).NotEmpty().NotNull();
        }
    }
}