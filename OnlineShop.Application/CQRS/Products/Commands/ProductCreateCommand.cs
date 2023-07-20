using AutoMapper;
using FluentValidation;
using MediatR;
using OnlineShop.Application.Abstraction;
using OnlineShop.Domain.Models;
using ValidationException = OnlineShop.Application.Common.Exceptions.ValidationException;

namespace OnlineShop.Application.CQRS.Products.Commands
{
    public class ProductCreateCommand:IRequest<Guid>
    {
        public string Name { get; set; }
        public double Amount { get; set; }
        public string UnitOfMeasurement { get; set; }
        public Guid CategoryId { get; set; }
    }

    public class ProductCreateCommandHandler : IRequestHandler<ProductCreateCommand, Guid>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public ProductCreateCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(ProductCreateCommand request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(request);

            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return product.Id;
        }
    }
}
