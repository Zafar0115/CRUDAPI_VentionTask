
using Microsoft.AspNetCore.Mvc;
using OnlineShop.API.Controllers.BaseController;
using OnlineShop.Application.CQRS.Products.Commands;
using OnlineShop.Application.CQRS.Products.Queries;

namespace OnlineShop.API.Controllers
{

    public class ProductController : ApiControllerBase
    {
        [HttpPost]
        [Route("Create")]
        public async Task<Guid> CreateProduct([FromBody] ProductCreateCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpGet]
        [Route("GetById")]
        public async Task<ProductGetByIdQueryResponse> GetById([FromQuery] Guid id)
        {
            return await _mediator.Send(new ProductGetByIdQuery() { Id = id });
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IEnumerable<ProductGetAllQueryResponse>> GetAll()
        {
            return await _mediator.Send(new ProductGetAllQuery());
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete([FromQuery] Guid id)
        {
            await _mediator.Send(new ProductDeleteCommand() { Id=id});
            return NoContent();
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] ProductUpdateCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }


    }
}
