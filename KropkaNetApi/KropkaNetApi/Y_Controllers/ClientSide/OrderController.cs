using Azure;
using KropkaNetApi.X_Models.ClientSide.Order;
using KropkaNetApi.Y_Services.ClientSide;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KropkaNetApi.Y_Controllers.ClientSide
{
    public class OrderController
    {
        [Route("api/kropkaNet/order")]
        [ApiController]
        [Authorize]
        public class CompanyController : ControllerBase
        {
            private readonly IOrderService _orderService;

            public CompanyController(IOrderService orderService)
            {
                _orderService = orderService;
            }

            [HttpPost("create")]
            public ActionResult Create([FromBody] CreateOrderDto dto)
            {
                var createdOrderId = _orderService.Create(dto);

                var result = Created($"{createdOrderId}", null) as CreatedResult;
                if (result != null)
                {
                    Response.Headers.Add("Access-Control-Expose-Headers", "Location");
                }

                return result;
            }
        }
}
