using AutoMapper;
using OnlineShop.Application.CQRS.Orders.Commands;
using OnlineShop.Application.CQRS.Orders.Queries;
using OnlineShop.Domain.Models;

namespace OnlineShop.Application.Common.Mappings
{
    public class OrderMapping:Profile
    {
        public OrderMapping()
        {
            CreateMap<OrderCreateCommand, Order>();
            CreateMap<OrderUpdateCommand, Order>();
            CreateMap<OrderDeleteCommand, Order>();

            CreateMap<Order, OrderGetAllQueryResponse>();
            CreateMap<Order, OrderGetByIdQueryResponse>();
        }
    }
}
