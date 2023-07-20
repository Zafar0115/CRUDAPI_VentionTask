using MediatR;
using OnlineShop.Application.Abstraction;
using OnlineShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Application.CQRS.Products.Commands
{
    public class ProductDeleteCommand:IRequest
    {
        public Guid Id { get; set; }
    }
    public class ProductDeleteCommandHandler : IRequestHandler<ProductDeleteCommand>
    {
        private readonly IApplicationDbContext _dbContext;

        public ProductDeleteCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(ProductDeleteCommand request, CancellationToken cancellationToken)
        {
            Product? product = await _dbContext.Products.FindAsync(request.Id);
            if (product == null) throw new NotFoundException(nameof(Product), request.Id);

             _dbContext.Products.Remove(product);
             await _dbContext.SaveChangesAsync();
        }
    }
}
