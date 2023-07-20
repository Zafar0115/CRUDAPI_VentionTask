using MediatR;
using OnlineShop.Application.Abstraction;
using OnlineShop.Domain.Models;

namespace OnlineShop.Application.CQRS.Customers.Commands
{
    public class CustomerDeleteCommand:IRequest
    {
        public Guid Id { get; set; }
    }

    public class CustomerDeleteCommandHandler : IRequestHandler<CustomerDeleteCommand>
    {
        private readonly IApplicationDbContext _dbContext;

        public CustomerDeleteCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(CustomerDeleteCommand request, CancellationToken cancellationToken)
        {
            Customer? customer = await _dbContext.Customers.FindAsync(request.Id);
            if (customer == null) throw new NotFoundException(nameof(Customer), request.Id);

            _dbContext.Customers.Remove(customer);
            await _dbContext.SaveChangesAsync();
        }
    }
}
