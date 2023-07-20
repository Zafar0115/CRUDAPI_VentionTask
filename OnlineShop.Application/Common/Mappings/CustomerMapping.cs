using AutoMapper;
using OnlineShop.Application.CQRS.Customers.Commands;
using OnlineShop.Application.CQRS.Customers.Queries;
using OnlineShop.Domain.Models;

namespace OnlineShop.Application.Common.Mappings
{
    public class CustomerMapping:Profile
    {
        public CustomerMapping()
        {
            CreateMap<CustomerCreateCommand, Customer>()
                .ForMember(dest=>dest.BirthDate,opt=>opt.MapFrom(src=>DateOnly.Parse(src.BirthDate)));
            CreateMap<CustomerUpdateCommand, Customer>()
                .ForMember(dest => dest.BirthDate, opt => opt.MapFrom(src => DateOnly.Parse(src.BirthDate)));
            CreateMap<CustomerDeleteCommand, Customer>();

            CreateMap<Customer, CustomerGetAllQueryResponse>();
            CreateMap<Customer, CustomerGetByIdQueryResponse>();
        }
    }
}
