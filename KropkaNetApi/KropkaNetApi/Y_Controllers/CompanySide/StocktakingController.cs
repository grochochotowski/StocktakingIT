using KropkaNetApi.Y_Services.CompanySide;
using KropkaNetApi.X_Models.CompanySide.Stocktaking;
using KropkaNetApi.X_Entities.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KropkaNetApi.X_Entities;

namespace KropkaNetApi.Y_Controllers.CompanySide
{
    [Route("api/kropkaNet/stocktaking")]
    [ApiController]
    //[Authorize(Roles = "Employee, Moderator, Admin")]
    public class StocktakingController : ControllerBase
    {
        private readonly IStocktakingService _stocktakingService;

        public StocktakingController(IStocktakingService stocktakingService)
        {
            _stocktakingService = stocktakingService;
        }

        // POST: /api/kropkaNet/stocktaking/create
        [HttpPost("create")]
        public ActionResult<int> Create([FromBody] CreateStocktakingDto dto)
        {
            var createdStocktakingId = _stocktakingService.Create(dto);
            return createdStocktakingId > 0
                ? CreatedAtAction(nameof(GetById), new { id = createdStocktakingId }, dto)
                : BadRequest("Failed to create stocktaking");
        }

        // GET: /api/kropkaNet/stocktaking/all
        [HttpGet("all")]
        public ActionResult<ReturnResult<StocktakingListDto>> GetAll(
            [FromQuery] int page,
            [FromQuery] string? filter,
            [FromQuery] string? sortBy,
            [FromQuery] SortDirection sortDirection
            )
        {
            var result = _stocktakingService.GetAll(page, filter, sortBy, sortDirection);
            return Ok(result);
        }

        // GET: /api/kropkaNet/stocktaking/{id}
        [HttpGet("{id}")]
        public ActionResult<StocktakingDto> GetById(int id)
        {
            var stocktaking = _stocktakingService.GetDetails(id);
            return stocktaking != null ? Ok(stocktaking) : NotFound("Stocktaking not found");
        }

        // PUT: /api/kropkaNet/stocktaking/update/{id}
        [HttpPut("update/{id}")]
        public IActionResult Update(int id, [FromBody] UpdateStocktakingDto dto)
        {
                var stocktakingId = _stocktakingService.Update(id, dto);
                return Ok(stocktakingId);
        }

        // PATCH: /api/kropkaNet/stocktaking/{stocktakingId}/addEmployee/{employeeId}
        [HttpPatch("{stocktakingId}/addEmployee/{employeeId}")]
        public IActionResult AddEmployee(int stocktakingId, int employeeId)
        {
            try
            {
                _stocktakingService.AddEmployee(stocktakingId, employeeId);
                return Ok("Employee added successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PATCH: /api/kropkaNet/stocktaking/{stocktakingId}/removeEmployee/{employeeId}
        [HttpPatch("{stocktakingId}/removeEmployee/{employeeId}")]
        public IActionResult RemoveEmployee(int stocktakingId, int employeeId)
        {
            try
            {
                _stocktakingService.RemoveEmployee(stocktakingId, employeeId);
                return Ok("Employee removed successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: /api/kropkaNet/stocktaking/delete/{id}
        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            int result = _stocktakingService.Delete(id);
            return result != -1 ? NoContent() : NotFound("Stocktaking not found");
        }
    }
}
