using AutoMapper;
using OnlineShop.Application.CQRS.Categories.Commands;
using OnlineShop.Application.CQRS.Categories.Queries;
using OnlineShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Application.Common.Mappings
{
    public class CategoryMapping:Profile
    {
        public CategoryMapping()
        {
            CreateMap<CategoryCreateCommand, Category>();
            CreateMap<CategoryUpdateCommand, Category>();
            CreateMap<CategoryDeleteCommand, Category>();
            CreateMap<Category, CategoryGetAllQueryResponse>();
            CreateMap<Category, CategoryGetByIdQueryResponse>();
        }
    }
}
