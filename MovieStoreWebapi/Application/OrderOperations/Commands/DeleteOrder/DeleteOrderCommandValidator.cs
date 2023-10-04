using FluentValidation;

namespace MovieStoreWebapi.Application.OrderOperations.Commands.DeleteOrder
{
    public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
    {
        public DeleteOrderCommandValidator()
        {
             RuleFor(command => command.OrderId).GreaterThan(0).NotNull().NotEmpty();

        }
    }
}