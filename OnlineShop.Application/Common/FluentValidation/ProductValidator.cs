using FluentValidation;
using OnlineShop.Domain.Models;

namespace OnlineShop.Application.Common.FluentValidation
{
    public class ProductValidator:AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name).NotEmpty().Length(2, 50).Matches("^[A-Za-z]+$");
            RuleFor(p => p.UnitOfMeasurement).NotEmpty().Length(1, 50);
            RuleFor(p => p.Amount).GreaterThan(0);
        }
    }
}
