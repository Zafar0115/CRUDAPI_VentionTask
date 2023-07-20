using AutoMapper;
using OnlineShop.Application.CQRS.Products.Commands;
using OnlineShop.Application.CQRS.Products.Queries;
using OnlineShop.Domain.Models;

namespace OnlineShop.Application.Common.Mappings
{
    public class ProductMapping:Profile
    {
        public ProductMapping()
        {
            CreateMap<ProductCreateCommand, Product>();
            CreateMap<ProductDeleteCommand, Product>();
            CreateMap<ProductUpdateCommand, Product>();
            CreateMap<Product, ProductGetByIdQueryResponse>();
            CreateMap<Product, ProductGetAllQueryResponse>();
            
        }
    }
}
