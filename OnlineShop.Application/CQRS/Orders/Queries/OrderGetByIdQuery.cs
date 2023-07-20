using AutoMapper;
using MediatR;
using OnlineShop.Application.Abstraction;
using OnlineShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Application.CQRS.Orders.Queries
{
    public class OrderGetByIdQuery:IRequest<OrderGetByIdQueryResponse>
    {
        public Guid Id { get; set; }
    }

    public class OrderGetByIdQueryHandler : IRequestHandler<OrderGetByIdQuery, OrderGetByIdQueryResponse>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public OrderGetByIdQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<OrderGetByIdQueryResponse> Handle(OrderGetByIdQuery request, CancellationToken cancellationToken)
        {
            Order? order = await _dbContext.Orders.FindAsync(request.Id);
            if (order == null) throw new NotFoundException(nameof(Order),request.Id);

            OrderGetByIdQueryResponse? response = _mapper.Map<OrderGetByIdQueryResponse>(order);
            return response;
        }
    }

    public class OrderGetByIdQueryResponse
    {
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }
        public double OrderAmount { get; set; }
        public DateTime OrderDate { get; init; } = DateTime.Now;
        public DateTime DeliveryDate { get; set; }
    }


}
