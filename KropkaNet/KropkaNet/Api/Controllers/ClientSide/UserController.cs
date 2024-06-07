using Microsoft.AspNetCore.Mvc;
using KropkaNet.Objects.Entities;
using KropkaNet.Objects.Dtos.ClientSide.User;
using KropkaNet.Objects.Entities.Enum;
using KropkaNet.Api.Services.ClientSide;
using KropkaNet.Objects.Entities.Models.ClientSide;
using System.Globalization;

namespace KropkaNet.Api.Controllers.ClientSide
{
    [Route("api/kropkaNet/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }



        // GET: api/kropkaNet/user/getAll
        [HttpGet("getAll")]
        //[Authorize(Roles = "Employee, Moderator, Admin")]
        public ActionResult<ReturnResult<UserDto>> GetAll(
            [FromQuery] int page,
            [FromQuery] string? filter,
            [FromQuery] string? sortBy,
            [FromQuery] SortDirection sortDirection)
        {
            var result = _userService.GetAll(page, filter, sortBy, sortDirection);
            return Ok(result);
        }

        // GET: api/kropkaNet/user/{orderId}/GetFromOrder
        [HttpGet("{orderId}/GetFromOrder")]
        //[Authorize]
        public ActionResult<ReturnResult<UserDto>> GetFromOrder([FromRoute] int orderId)
        {
            var result = _userService.GetFromOrder(orderId);
            return Ok(result);
        }

        // GET: api/kropkaNet/user/getFromCompany/{companyId}
        [HttpGet("getFromCompany/{companyId}")]
        //[Authorize]
        public ActionResult GetFromCompany([FromRoute] int companyId, [FromQuery] string? sortBy, [FromQuery] SortDirection sortDirection)
        {
            var result = _userService.GetFromCompany(companyId, sortBy, sortDirection);
            return Ok(result);
        }


        // GET: api/kropkaNet/user/{id}
        [HttpGet("{id}")]
        //[Authorize]
        public ActionResult<UserDto> GetDetails(int id)
        {
            var userDto = _userService.GetDetails(id);

            return Ok(userDto);
        }

        // PUT: api/kropkaNet/user/update/{id}
        [HttpPut("update/{id}")]
        //[Authorize]
        public IActionResult Update(int id, [FromBody] UpdateUserDto dto)
        {
            _userService.Update(id, dto);
            return Ok();
        }

        // DELETE: api/kropkaNet/user/delete/{id}
        [HttpDelete("delete/{id}")]
        //[Authorize]
        public IActionResult Delete(int id)
        {
            _userService.Delete(id);
            return NoContent();
        }
    }
}
