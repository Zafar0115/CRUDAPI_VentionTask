using AutoMapper;
using MediatR;
using OnlineShop.Application.Abstraction;
using OnlineShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Application.CQRS.Orders.Commands
{
    public class OrderCreateCommand:IRequest<Guid>
    {
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }
        public double OrderAmount { get; set; }
        public DateTime OrderDate { get; init; } = DateTime.Now;
        public DateTime DeliveryDate { get; set; }
    }

    public class OrderCreateCommandHandler : IRequestHandler<OrderCreateCommand, Guid>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public OrderCreateCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(OrderCreateCommand request, CancellationToken cancellationToken)
        {
            Order? order=_mapper.Map<Order>(request);
            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();
            return order.Id;
        }
    }
}
