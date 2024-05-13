using KropkaNetApi.X_Models.CompanySide.Stocktaking;
using KropkaNetApi.Y_Services.CompanySide;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KropkaNetApi.Y_Controllers.CompanySide
{
    [Route("api/kropkaNet/stocktaking")]
    [ApiController]
    [Authorize]
    public class StocktakingController : ControllerBase
    {
        private readonly IStocktakingService _stocktakingService;

        public StocktakingController(IStocktakingService stocktakingService)
        {
            _stocktakingService = stocktakingService;
        }

        // POST: /api/kropkaNet/stocktaking/create
        [HttpPost("create")]
        public ActionResult Create([FromBody] CreateStocktakingDto dto)
        {
            var createdStocktakingId = _stocktakingService.Create(dto);
            if (createdStocktakingId <= 0) return BadRequest("Failed to create stocktaking");

            var result = CreatedAtAction("GetById", new { id = createdStocktakingId }, dto); // Assuming GetById method exists or needs to be implemented
            return result;
        }

        // GET: /api/kropkaNet/stocktaking/all
        [HttpGet("all")]
        public ActionResult<IEnumerable<StocktakingListDto>> GetAll()
        {
            var stocktakings = _stocktakingService.GetAll();
            return Ok(stocktakings);
        }

        // DELETE: /api/kropkaNet/stocktaking/delete/{id}
        [HttpDelete("delete/{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            var result = _stocktakingService.Delete(id);
            if (result == -1) return NotFound("Stocktaking does not exist");

            return NoContent();
        }
    }
}
