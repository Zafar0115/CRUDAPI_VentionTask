using MediatR;
using OnlineShop.Application.Abstraction;
using OnlineShop.Domain.Models;

namespace OnlineShop.Application.CQRS.Orders.Commands
{
    public class OrderDeleteCommand:IRequest
    {
        public Guid Id { get; set; }
    }

    public class OrderDeleteCommandHandler : IRequestHandler<OrderDeleteCommand>
    {
        private readonly IApplicationDbContext _dbContext;

        public OrderDeleteCommandHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(OrderDeleteCommand request, CancellationToken cancellationToken)
        {
            Order? order = await _dbContext.Orders.FindAsync(request.Id);
            if (order == null) throw new NotFoundException(nameof(Order), request.Id);

            _dbContext.Orders.Remove(order);
            await _dbContext.SaveChangesAsync();
        }
    }


}
