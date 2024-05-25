using Azure;
using KropkaNetApi.X_Entities.Enum;
using KropkaNetApi.X_Models.ClientSide.Company;
using KropkaNetApi.X_Models.ClientSide.Order;
using KropkaNetApi.X_Models.ClientSide.User;
using KropkaNetApi.Y_Services.ClientSide;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KropkaNetApi.Y_Controllers.ClientSide
{
    [Route("api/kropkaNet/order")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // POST api/kropkaNet/company/create
        [HttpPost("create")]
        //[Authorize]
        public ActionResult Create([FromQuery] int? userId, [FromBody] CreateOrderDto dto)
        {
            var createdOrderId = _orderService.Create(userId, dto);

            var result = Created($"{createdOrderId}", null) as CreatedResult;
            if (result != null)
            {
                Response.Headers.Add("Access-Control-Expose-Headers", "Location");
            }

            return result;
        }

        
    }
}

