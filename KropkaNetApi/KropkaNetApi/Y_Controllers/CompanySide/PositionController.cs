using KropkaNetApi.X_Models.CompanySide.Position;
using KropkaNetApi.Y_Services.CompanySide;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace KropkaNetApi.Y_Controllers.CompanySide
{
    [Route("api/position")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        private readonly IPositionService _positionService;

        public PositionController(IPositionService positionService)
        {
            _positionService = positionService;
        }



        // GET api/position/all
        [HttpGet("all")]
        public ActionResult<IEnumerable<PositionDto>> GetAll()
        {
            var positionDtos = _positionService.GetAll();
            return Ok(positionDtos);
        }
    }
}
