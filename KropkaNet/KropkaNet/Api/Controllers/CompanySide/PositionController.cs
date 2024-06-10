using KropkaNet.Api.Services.CompanySide;
using KropkaNet.Objects.Dtos.CompanySide.Position;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KropkaNet.Api.Controllers.CompanySide
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
        [Authorize(Roles = "Employee, Moderator, Admin")]
        public ActionResult<IEnumerable<PositionDto>> GetAll()
        {
            var positionDtos = _positionService.GetAll();
            return Ok(positionDtos);
        }
    }
}
