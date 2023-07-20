using FluentValidation;
using OnlineShop.Domain.Models;

namespace OnlineShop.Application.Common.FluentValidation
{
    public class CategoryValidator:AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(c => c.CategoryName).NotEmpty();
        }
    }
}
