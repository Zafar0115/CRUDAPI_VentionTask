using AutoMapper;
using MediatR;
using OnlineShop.Application.Abstraction;
using OnlineShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Application.CQRS.Customers.Queries
{
    public class CustomerGetAllQuery:IRequest<IEnumerable<CustomerGetAllQueryResponse>>
    {
    }

    public class CustomerGetAllQueryHandler : IRequestHandler<CustomerGetAllQuery, IEnumerable<CustomerGetAllQueryResponse>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public CustomerGetAllQueryHandler(IApplicationDbContext dbContext,IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public Task<IEnumerable<CustomerGetAllQueryResponse>> Handle(CustomerGetAllQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Customer> customers = _dbContext.Customers;
            IEnumerable<CustomerGetAllQueryResponse> response = _mapper.Map<IEnumerable<CustomerGetAllQueryResponse>>(customers);
            return Task.FromResult(response);
        }
    }
    public class CustomerGetAllQueryResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly BirthDate { get; set; }
    }

}
