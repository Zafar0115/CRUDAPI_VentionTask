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
    public class ProductGetAllQuery:IRequest<IEnumerable<ProductGetAllQueryResponse>>
    {
    }

    public class ProductGetAllQueryHandler : IRequestHandler<ProductGetAllQuery, IEnumerable<ProductGetAllQueryResponse>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public ProductGetAllQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductGetAllQueryResponse>> Handle(ProductGetAllQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Product> products = _dbContext.Products;
            IEnumerable<ProductGetAllQueryResponse> result=_mapper.Map<IEnumerable<ProductGetAllQueryResponse>>(products);
            return result;
        }
    }

    public class ProductGetAllQueryResponse
    {
        public string Name { get; set; }
        public double Amount { get; set; }
        public string UnitOfMeasurement { get; set; }
        public Guid CategoryId { get; set; }
    }
}
