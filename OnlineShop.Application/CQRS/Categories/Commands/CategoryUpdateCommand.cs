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

namespace OnlineShop.Application.CQRS.Categories.Commands
{
    public class CategoryUpdateCommand:IRequest
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; }
    }

    public class CategoryUpdateCommandHandler : IRequestHandler<CategoryUpdateCommand>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public CategoryUpdateCommandHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task Handle(CategoryUpdateCommand request, CancellationToken cancellationToken)
        {
            Category? category = await _dbContext.Categories.FindAsync(request.Id);
            if (category == null) throw new NotFoundException(nameof(Category),request.Id);

            Category? mapped = _mapper.Map<Category>(request);
            
            var existingEntity = _dbContext.Categories.Local.FirstOrDefault(c => c.Id == mapped.Id);
            if (existingEntity != null)
            {
                _dbContext.Entry(existingEntity).State = EntityState.Detached;
            }

            _dbContext.Categories.Update(mapped);
            await _dbContext.SaveChangesAsync();
        }
    }

}
