using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Application.Abstraction;
using OnlineShop.Domain.Models;

namespace OnlineShop.Application.CQRS.Products.Commands
{
    public class ProductUpdateCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
        public string UnitOfMeasurement { get; set; }
        public Guid CategoryId { get; set; }
    }

    public class ProductUpdateCommandHandler : IRequestHandler<ProductUpdateCommand>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public ProductUpdateCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task Handle(ProductUpdateCommand request, CancellationToken cancellationToken)
        {
            Product? product = await _dbContext.Products.FindAsync(request.Id);
            if (product == null) throw new NotFoundException(nameof(Product), request.Id);

            Category? category = await _dbContext.Categories.FindAsync(request.CategoryId);
            if (category == null) throw new NotFoundException(nameof(Category), request.CategoryId);

           
            Product? mapped = _mapper.Map<Product>(request);

            var existingEntity = _dbContext.Products.Local.FirstOrDefault(c => c.Id == mapped.Id);
            if (existingEntity != null)
            {
                _dbContext.Entry(existingEntity).State = EntityState.Detached;
            }

            _dbContext.Products.Update(mapped);
            await _dbContext.SaveChangesAsync();
        }
    }
}
