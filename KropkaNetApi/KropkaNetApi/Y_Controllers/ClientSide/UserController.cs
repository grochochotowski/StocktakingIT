using KropkaNetApi.X_Models.CompanySide.Employee;
using KropkaNetApi.X_Models.Shared.Account;
using KropkaNetApi.Y_Services.ClientSide;
using KropkaNetApi.Y_Services.CompanySide;
using Microsoft.AspNetCore.Mvc;

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
    }
    
}
