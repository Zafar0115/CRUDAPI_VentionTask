using AutoMapper;
using MediatR;
using OnlineShop.Application.Abstraction;
using OnlineShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Application.CQRS.Categories.Queries
{
    public class CategoryGetByIdQuery:IRequest<CategoryGetByIdQueryResponse>
    {
        public Guid Id { get; set; }
    }

    public class CategoryGetByIdQueryHandler : IRequestHandler<CategoryGetByIdQuery, CategoryGetByIdQueryResponse>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public CategoryGetByIdQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<CategoryGetByIdQueryResponse> Handle(CategoryGetByIdQuery request, CancellationToken cancellationToken)
        {
            Category? category = await _dbContext.Categories.FindAsync(request.Id);
            if (category == null) throw new NotFoundException(nameof(Category), request.Id);

            CategoryGetByIdQueryResponse? mapped=_mapper.Map<CategoryGetByIdQueryResponse>(category);
            return mapped;
        }
    }


    public class CategoryGetByIdQueryResponse
    {
        public string CategoryName { get; set; }
    }
}
