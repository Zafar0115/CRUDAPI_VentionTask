using Microsoft.AspNetCore.Mvc;
using OnlineShop.API.Controllers.BaseController;
using OnlineShop.Application.CQRS.Categories.Commands;
using OnlineShop.Application.CQRS.Customers.Commands;
using OnlineShop.Application.CQRS.Customers.Queries;
using OnlineShop.Application.CQRS.Orders.Commands;
using OnlineShop.Application.CQRS.Orders.Queries;

namespace OnlineShop.API.Controllers
{
    public class CustomerController:ApiControllerBase
    {
        [HttpPost]
        [Route("GetById")]
        public async Task<CustomerGetByIdQueryResponse> GetById([FromQuery] Guid id)
        {
            return await _mediator.Send(new CustomerGetByIdQuery() { Id = id });
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IEnumerable<CustomerGetAllQueryResponse>> GetAll()
        {
            return await _mediator.Send(new CustomerGetAllQuery());
        }

        [HttpPost]
        [Route("Create")]
        public async Task<Guid> CreateCustomer([FromBody] CustomerCreateCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> DeleteCustomer([FromQuery] Guid id)
        {
            await _mediator.Send(new CustomerDeleteCommand() { Id = id });
            return NoContent();
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateCustomer([FromBody] CustomerUpdateCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
