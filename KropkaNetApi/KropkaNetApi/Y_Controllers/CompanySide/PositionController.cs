using KropkaNetApi.X_Models.CompanySide.Position;
using KropkaNetApi.Y_Services.CompanySide;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KropkaNetApi.Y_Controllers.CompanySide
{
    [Route("api/kropkaNet/position")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        private readonly IPositionService _positionService;

        public PositionController(IPositionService positionService)
        {
            _positionService = positionService;
        }



        // GET: /api/kropkaNet/position/all
        [HttpGet("all")]
        //[Authorize(Roles = "Employee, Moderator, Admin")]
        public ActionResult<IEnumerable<PositionDto>> GetAll()
        {
            var positionDtos = _positionService.GetAll();
            return Ok(positionDtos);
        }
    }
}
