using Microsoft.AspNetCore.Mvc;
using KropkaNetApi.Y_Services.ClientSide;
using KropkaNetApi.X_Models.ClientSide.User;
using Microsoft.AspNetCore.Authorization;
using KropkaNetApi.X_Entities.Enum;
using KropkaNetApi.X_Entities;

namespace KropkaNetApi.Y_Controllers.ClientSide
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

        // POST: api/kropkaNet/user/create
        [HttpPost("create")]
        [Authorize]  // Zakładając, że każdy może tworzyć nowego użytkownika
        public ActionResult<int> Create([FromBody] CreateUserDto dto)
        {
            try
            {
                var userId = _userService.Create(dto);
                return CreatedAtAction(nameof(GetDetails), new { id = userId }, userId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/kropkaNet/user/getAll
        [HttpGet("getAll")]
        [Authorize(Roles = "Employee, Moderator, Admin")] // Dostępne dla wybranych ról
        public ActionResult<ReturnResult<UserDto>> GetAll([FromQuery] int page = 1, [FromQuery] string filter = "", [FromQuery] string sortBy = "Name", [FromQuery] SortDirection sortDirection = SortDirection.ASC)
        {
            var result = _userService.GetAll(page, filter, sortBy, sortDirection);
            return Ok(result);
        }


        // GET: api/kropkaNet/user/{id}
        [HttpGet("{id}")]
        [Authorize]  // Zakładając, że każdy może uzyskać szczegóły o użytkowniku
        public ActionResult<UserDto> GetDetails(int id)
        {
            try
            {
                var userDto = _userService.GetDetails(id);
                if (userDto == null) return NotFound();
                return Ok(userDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/kropkaNet/user/update/{id}
        [HttpPut("update/{id}")]
        [Authorize]  // Zakładając, że każdy może aktualizować użytkownika
        public IActionResult Update(int id, [FromBody] UserDto dto)
        {
            try
            {
                _userService.Update(id, dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/kropkaNet/user/delete/{id}
        [HttpDelete("delete/{id}")]
        [Authorize]  // Zakładając, że każdy może usunąć użytkownika
        public IActionResult Delete(int id)
        {
            try
            {
                _userService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
