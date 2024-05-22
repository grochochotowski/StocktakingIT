using KropkaNetApi.X_Entities;
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
        public ActionResult LogIn([FromBody] LoginDto dto)
        {
            LoginResponse response = _accountService.LogIn(dto);

            if (response.IsLoggedIn)
            {
                return Ok(response);
            }

            return Unauthorized("Login or password is incorrect");
        }

        [HttpPost("refresh")]
        public ActionResult Refresh([FromBody] RefreshTokenModel model)
        {
            LoginResponse response = _accountService.Refresh(model);

            if (response.IsLoggedIn)
            {
                return Ok(response);
            }

            return Unauthorized("Login or password is incorrect");
        }

    }
}
    