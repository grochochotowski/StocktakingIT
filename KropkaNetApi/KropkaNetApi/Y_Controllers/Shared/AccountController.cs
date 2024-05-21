using KropkaNetApi.X_Models.Shared.Account;
using KropkaNetApi.Y_Services.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KropkaNetApi.Y_Controllers.Shared
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }



        [HttpPost("register")]
        [Authorize]
        public ActionResult Register([FromBody] RegisterDto dto)
        {
            _accountService.Register(dto);
            return Ok();
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginDto dto)
        {
            string token = _accountService.GenerateToken(dto);

            return Ok(new { token = token });
        }

    }
}
    