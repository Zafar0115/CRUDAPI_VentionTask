using AutoMapper;
using MediatR;
using OnlineShop.Application.Abstraction;
using OnlineShop.Domain.Models;

namespace OnlineShop.Application.CQRS.Categories.Commands
{
    public class CategoryCreateCommand:IRequest<Guid>
    {
        public string CategoryName { get; set; }
    }

    public class CategoryCreateCommandHandler : IRequestHandler<CategoryCreateCommand, Guid>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public CategoryCreateCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CategoryCreateCommand request, CancellationToken cancellationToken)
        {
            Category? category = _mapper.Map<Category>(request);
            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();
            return category.Id;
        }
    }
}
