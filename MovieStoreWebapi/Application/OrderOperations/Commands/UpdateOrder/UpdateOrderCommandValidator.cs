using FluentValidation;
using MovieStoreWebapi.Application.OrderOperations.Commands.UpdateOrder;


namespace MovieStoreWebapi.Application.Commands.OrderOperations.UpdateOrder
{
    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(command => command.OrderId).GreaterThan(0);
            RuleFor(command => command.Model.CustomerId).GreaterThan(0);
            RuleFor(command => command.Model.MovieId).GreaterThan(0);
            RuleFor(command => command.Model.Price).GreaterThan(0);
            RuleFor(command => command.Model.PurchaseDate).NotEmpty();
        }
    }
}