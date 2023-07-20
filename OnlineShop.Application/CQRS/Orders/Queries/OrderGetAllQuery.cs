using AutoMapper;
using MediatR;
using OnlineShop.Application.Abstraction;
using OnlineShop.Domain.Models;

namespace OnlineShop.Application.CQRS.Orders.Queries
{
    public class OrderGetAllQuery:IRequest<IEnumerable<OrderGetAllQueryResponse>>
    {
    }

    public class OrderGetAllQueryHandler : IRequestHandler<OrderGetAllQuery, IEnumerable<OrderGetAllQueryResponse>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public OrderGetAllQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public Task<IEnumerable<OrderGetAllQueryResponse>> Handle(OrderGetAllQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Order> orders= _dbContext.Orders;
            IEnumerable<OrderGetAllQueryResponse> response = _mapper.Map<IEnumerable<OrderGetAllQueryResponse>>(orders);
            return Task.FromResult(response);
        }
    }



    public class OrderGetAllQueryResponse
    {
        public Guid CustomerId { get; set; }
        public Guid ProductId { get; set; }
        public double OrderAmount { get; set; }
        public DateTime OrderDate { get; init; } = DateTime.Now;
        public DateTime DeliveryDate { get; set; }
    }
}
