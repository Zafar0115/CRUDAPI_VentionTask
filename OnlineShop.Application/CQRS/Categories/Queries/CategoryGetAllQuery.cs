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
    public class CategoryGetAllQuery:IRequest<IEnumerable<CategoryGetAllQueryResponse>>
    {
    }
    public class CategoryGetAllQueryHandler : IRequestHandler<CategoryGetAllQuery, IEnumerable<CategoryGetAllQueryResponse>>
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public CategoryGetAllQueryHandler(IApplicationDbContext dbContext,IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public Task<IEnumerable<CategoryGetAllQueryResponse>> Handle(CategoryGetAllQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Category> categories=_dbContext.Categories;
            IEnumerable<CategoryGetAllQueryResponse> mapped=_mapper.Map<IEnumerable<CategoryGetAllQueryResponse>>(categories);
            return Task.FromResult(mapped);
        }
    }
    public class CategoryGetAllQueryResponse
    {
        public string CategoryName { get; set; }
    }

}
