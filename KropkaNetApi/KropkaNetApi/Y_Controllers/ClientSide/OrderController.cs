using Azure;
using KropkaNetApi.X_Entities.Enum;
using KropkaNetApi.X_Entities.Objects.ClientSide;
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

        // POST api/kropkaNet/order/create
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

        // GET api/kropkaNet/order/user/{id}
        [HttpGet("user/{userId}")]
        //[Authorize]
        public ActionResult<IEnumerable<OrderListDto>> GetListUser(
            [FromRoute] int userId,
            [FromQuery] int page,
            [FromQuery] string? filters,
            [FromQuery] string? sortBy,
            [FromQuery] SortDirection sortDireciton
            )
        {
            var companyDtos = _orderService.GetListUser(userId, page, filters, sortBy, sortDireciton);
            return Ok(companyDtos);
        }

        // GET api/kropkaNet/order/all
        [HttpGet("all")]
        //[Authorize(Roles = "Employee, Moderator, Admin")]
        public ActionResult<IEnumerable<OrderListDto>> GetList(
            [FromQuery] int page,
            [FromQuery] string? filters,
            [FromQuery] string? sortBy,
            [FromQuery] SortDirection sortDireciton
            )
        {
            var companyDtos = _orderService.GetList(page, filters, sortBy, sortDireciton);
            return Ok(companyDtos);
        }

        // GET api/kropkaNet/order/{id}
        [HttpGet("{id}")]
        //[Authorize]
        public ActionResult<OrderDto> GetDetails([FromRoute] int id)
        {
            var companyDto = _orderService.GetDetails(id);
            return Ok(companyDto);
        }

        // PUT api/kropkaNet/order/update/5
        [HttpPut("update/{id}")]
        //[Authorize]
        public ActionResult Update([FromRoute] int id, [FromBody] UpdateOrderDto dto)
        {
            var orderId = _orderService.Update(id, dto);

            return Ok($"{orderId}");
        }

        // PATCH api/kropkaNet/order/addUser
        [HttpPatch("addUser")]
        //[Authorize]
        public ActionResult AddUser([FromQuery] int userId, [FromQuery] int orderId)
        {
            _orderService.AddUser(userId, orderId);

            return Ok();
        }

        // PATCH api/kropkaNet/order/removeUser
        [HttpPatch("removeUser")]
        //[Authorize]
        public ActionResult RemoveUser([FromQuery] int userId, [FromQuery] int orderId)
        {
            _orderService.RemoveUser(userId, orderId);

            return Ok();
        }

        // PATCH api/kropkaNet/order/state
        [HttpPatch("state")]
        //[Authorize(Roles="Employee, Moderator, Admin")]
        public ActionResult ChangeState([FromQuery] int id, [FromQuery] int state)
        {
            _orderService.ChangeState(id, state);

            return Ok();
        }

        // DELETE api/kropkaNet/company/delete{id}
        [HttpDelete("delete/{id}")]
        //[Authorize]
        public ActionResult Delete([FromRoute] int id)
        {
            _orderService.Delete(id);

            return NoContent();
        }
    }
}

