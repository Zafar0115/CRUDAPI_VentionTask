using FluentValidation;
using OnlineShop.Domain.Models;

namespace OnlineShop.Application.Common.FluentValidation
{
    public class CustomerValidator:AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(c=>c.FirstName).NotEmpty().Length(2,100).Matches("^[A-Za-z]+$");
            RuleFor(c=>c.LastName).NotEmpty().Length(2,100).Matches("^[A-Za-z]+$");
        }
    }
}
