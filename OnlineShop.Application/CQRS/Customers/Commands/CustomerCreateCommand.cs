using AutoMapper;
using MediatR;
using OnlineShop.Application.Abstraction;
using OnlineShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OnlineShop.Application.CQRS.Customers.Commands
{
    public class CustomerCreateCommand:IRequest<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [JsonPropertyName("BirhtDate format: YYYY-MM-DD")]
        public string BirthDate { get; set; }
    }

    public class CustomerCreateCommandHandler : IRequestHandler<CustomerCreateCommand, Guid>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public CustomerCreateCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CustomerCreateCommand request, CancellationToken cancellationToken)
        {
            Customer? customer=_mapper.Map<Customer>(request);
            await _dbContext.Customers.AddAsync(customer);
            await _dbContext.SaveChangesAsync();
            return customer.Id;
        }
    }
}
