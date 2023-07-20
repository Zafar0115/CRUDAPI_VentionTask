using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace OnlineShop.API.Controllers.BaseController
{
    [Route("[controller]")]
    [ApiController]
    public class ApiControllerBase:ControllerBase
    {
        protected IMediator _mediator => HttpContext.RequestServices.GetRequiredService<IMediator>();
    }
}
