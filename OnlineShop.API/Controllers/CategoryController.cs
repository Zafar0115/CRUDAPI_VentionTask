using Microsoft.AspNetCore.Mvc;
using OnlineShop.API.Controllers.BaseController;
using OnlineShop.Application.CQRS.Categories.Commands;
using OnlineShop.Application.CQRS.Categories.Queries;
using OnlineShop.Application.CQRS.Customers.Commands;
using OnlineShop.Application.CQRS.Customers.Queries;
using OnlineShop.Application.CQRS.Products.Commands;

namespace OnlineShop.API.Controllers
{
    public class CategoryController:ApiControllerBase
    {
        [HttpPost]
        [Route("GetById")]
        public async Task<CategoryGetByIdQueryResponse> GetById([FromQuery] Guid id)
        {
            return await _mediator.Send(new CategoryGetByIdQuery() { Id = id });
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IEnumerable<CategoryGetAllQueryResponse>> GetAll()
        {
            return await _mediator.Send(new CategoryGetAllQuery());
        }

        [HttpPost]
        [Route("Create")]
        public async Task<Guid> CreateCategory([FromBody] CategoryCreateCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> DeleteCategory([FromQuery] Guid id)
        {
            await _mediator.Send(new CategoryDeleteCommand() { Id = id });
            return NoContent();
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateCategory([FromBody] CategoryUpdateCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
