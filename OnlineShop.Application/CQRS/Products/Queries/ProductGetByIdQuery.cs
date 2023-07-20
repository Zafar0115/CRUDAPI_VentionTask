using AutoMapper;
using MediatR;
using OnlineShop.Application.Abstraction;
using OnlineShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Application.CQRS.Products.Queries
{
    public class ProductGetByIdQuery:IRequest<ProductGetByIdQueryResponse>
    {
        public Guid Id { get; set; }
    }
    public class ProductGetByIdQueryHandler : IRequestHandler<ProductGetByIdQuery, ProductGetByIdQueryResponse>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public ProductGetByIdQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<ProductGetByIdQueryResponse> Handle(ProductGetByIdQuery request, CancellationToken cancellationToken)
        {
            Product? product = await _dbContext.Products.FindAsync(request.Id);
            if (product is null) throw new NotFoundException(nameof(Product), request.Id);
            ProductGetByIdQueryResponse? response = _mapper.Map<ProductGetByIdQueryResponse>(product);
            return response;
        }
    }
    public class ProductGetByIdQueryResponse
    {
        public string Name { get; set; }
        public double Amount { get; set; }
        public string UnitOfMeasurement { get; set; }
        public Guid CategoryId { get; set; }
    }
}
