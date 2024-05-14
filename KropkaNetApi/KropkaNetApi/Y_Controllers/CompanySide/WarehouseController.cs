using KropkaNetApi.X_Models.CompanySide.Warehouse;
using KropkaNetApi.Y_Services.CompanySide;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KropkaNetApi.Y_Controllers.CompanySide
{
    [Route("api/kropkaNet/warehouse")]
    [ApiController]
    [Authorize]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseService _warehouseService;

        public WarehouseController(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }

        // POST: /api/kropkaNet/warehouse/create
        [HttpPost("create")]
        public ActionResult Create([FromBody] CreateWarehouseDto dto)
        {
            var createdWarehouseId = _warehouseService.Create(dto);
            if (createdWarehouseId <= 0) return BadRequest("Failed to create warehouse");

            return CreatedAtAction(nameof(GetById), new { id = createdWarehouseId }, dto);
        }

        // GET: /api/kropkaNet/warehouse/all
        [HttpGet("all")]
        public ActionResult<IEnumerable<WarehouseListDto>> GetAll()
        {
            var warehouses = _warehouseService.GetAll();
            return Ok(warehouses);
        }

        // GET: /api/kropkaNet/warehouse/{id}
        [HttpGet("{id}")]
        public ActionResult<WarehouseDto> GetById([FromRoute] int id)
        {
            var warehouse = _warehouseService.GetById(id);
            if (warehouse == null) return NotFound("Warehouse not found");

            return Ok(warehouse);
        }

        // DELETE: /api/kropkaNet/warehouse/delete/{id}
        [HttpDelete("delete/{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            var result = _warehouseService.Delete(id);
            if (result == -1) return NotFound("Warehouse does not exist");

            return NoContent();
        }
    }
}
