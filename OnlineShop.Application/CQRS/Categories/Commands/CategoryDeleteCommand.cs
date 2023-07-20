using MediatR;
using Microsoft.AspNetCore.Builder;
using OnlineShop.Application.Abstraction;
using OnlineShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Application.CQRS.Categories.Commands
{
    public class CategoryDeleteCommand:IRequest
    {
        public Guid Id { get; set; }
    }
    public class CategoryDeleteCommandHandler : IRequestHandler<CategoryDeleteCommand>
    {
        private readonly IApplicationDbContext _dbContext;

        public CategoryDeleteCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(CategoryDeleteCommand request, CancellationToken cancellationToken)
        {
            Category? category = await _dbContext.Categories.FindAsync(request.Id);
            if (category == null) throw new NotFoundException(nameof(Category), request.Id);

            _dbContext.Categories.Remove(category);
            await _dbContext.SaveChangesAsync();
        }
    }
}
