using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Application.Abstraction;
using OnlineShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Application.CQRS.Orders.Commands
{
    public class OrderUpdateCommand:IRequest
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }
        public double OrderAmount { get; set; }
        public DateTime OrderDate { get; init; } = DateTime.Now;
        public DateTime DeliveryDate { get; set; }
    }

    public class OrderUpdateCommandHandler : IRequestHandler<OrderUpdateCommand>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public OrderUpdateCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task Handle(OrderUpdateCommand request, CancellationToken cancellationToken)
        {
            Order? order = await _dbContext.Orders.FindAsync(request.Id);
            if (order == null) throw new NotFoundException(nameof(Order),request.Id);

            Customer? customer = await _dbContext.Customers.FindAsync(request.CustomerId);
            if (customer == null) throw new NotFoundException(nameof(Customer), request.CustomerId);

            Product? product = await _dbContext.Products.FindAsync(request.ProductId);
            if (product == null) throw new NotFoundException(nameof(Product), request.ProductId);

            Order? mapped = _mapper.Map<Order>(request);

            var existingEntity = _dbContext.Orders.Local.FirstOrDefault(c => c.Id == mapped.Id);
            if (existingEntity != null)
            {
                _dbContext.Entry(existingEntity).State = EntityState.Detached;
            }

            _dbContext.Orders.Update(mapped);
            await _dbContext.SaveChangesAsync();
        }
    }


}
