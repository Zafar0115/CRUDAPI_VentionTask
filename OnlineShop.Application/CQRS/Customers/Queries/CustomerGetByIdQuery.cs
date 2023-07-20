using AutoMapper;
using MediatR;
using OnlineShop.Application.Abstraction;
using OnlineShop.Domain.Models;

namespace OnlineShop.Application.CQRS.Customers.Queries
{
    public class CustomerGetByIdQuery:IRequest<CustomerGetByIdQueryResponse>
    {
        public Guid Id { get; set; }
    }
    public class CustomerGetByIdQueryHandler : IRequestHandler<CustomerGetByIdQuery, CustomerGetByIdQueryResponse>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public CustomerGetByIdQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<CustomerGetByIdQueryResponse> Handle(CustomerGetByIdQuery request, CancellationToken cancellationToken)
        {
            Customer? customer = await _dbContext.Customers.FindAsync(request.Id);
            if (customer == null) throw new NotFoundException(nameof(Customer),request.Id);

            CustomerGetByIdQueryResponse? response = _mapper.Map<CustomerGetByIdQueryResponse>(customer);
            return response;
        }
    }

    public class CustomerGetByIdQueryResponse
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateOnly BirthDate { get; set; }
    }
}
