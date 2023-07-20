using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Application.Abstraction;
using OnlineShop.Domain.Models;
using System.Text.Json.Serialization;

namespace OnlineShop.Application.CQRS.Customers.Commands
{
    public class CustomerUpdateCommand:IRequest
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [JsonPropertyName("BirhtDate format: YYYY-MM-DD")]
        public string BirthDate { get; set; }
    }

    public class CustomerUpdateCommandHandler : IRequestHandler<CustomerUpdateCommand>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public CustomerUpdateCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task Handle(CustomerUpdateCommand request, CancellationToken cancellationToken)
        {
            Customer? customer = await _dbContext.Customers.FindAsync(request.Id);
            if (customer == null) throw new NotFoundException(nameof(Customer), request.Id);

            Customer? mapped=_mapper.Map<Customer>(request);

            var existingEntity = _dbContext.Customers.Local.FirstOrDefault(c => c.Id == mapped.Id);
            if (existingEntity != null)
            {
                _dbContext.Entry(existingEntity).State = EntityState.Detached;
            }

            _dbContext.Customers.Update(mapped);
            await _dbContext.SaveChangesAsync();
        }
    }
}
