using Microsoft.AspNetCore.Mvc;
using OnlineShop.API.Controllers.BaseController;
using OnlineShop.Application.CQRS.Categories.Commands;
using OnlineShop.Application.CQRS.Orders.Commands;
using OnlineShop.Application.CQRS.Orders.Queries;

namespace OnlineShop.API.Controllers
{
    public class OrderController:ApiControllerBase
    {
        [HttpPost]
        [Route("GetById")]
        public async Task<OrderGetByIdQueryResponse> GetById([FromQuery] Guid id)
        {
            return await _mediator.Send(new OrderGetByIdQuery() { Id = id });
        }

         [HttpGet]
        [Route("GetAll")]
        public async Task<IEnumerable<OrderGetAllQueryResponse>> GetAll()
        {
            return await _mediator.Send(new OrderGetAllQuery());
        }

        [HttpPost]
        [Route("Create")]
        public async Task<Guid> CreateOrder([FromBody]OrderCreateCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> DeleteOrder([FromQuery] Guid id)
        {
            await _mediator.Send(new OrderDeleteCommand() { Id = id });
            return NoContent();
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateOrder([FromBody]OrderUpdateCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

    }
}
