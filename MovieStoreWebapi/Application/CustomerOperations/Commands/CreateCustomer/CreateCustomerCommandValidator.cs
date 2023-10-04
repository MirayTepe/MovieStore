using FluentValidation;


namespace MovieStoreWebapi.Application.CustomerOperations.Commands.CreateCustomer
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(command => command.Model.FirstName).NotEmpty().MinimumLength(2).MaximumLength(50);
            RuleFor(command => command.Model.LastName).NotEmpty().MinimumLength(2).MaximumLength(50);
            RuleFor(command => command.Model.Email).NotNull().NotEmpty();
            RuleFor(command => command.Model.Password).NotNull().NotEmpty();

        }
    }
}