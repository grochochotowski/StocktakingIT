using KropkaNet.Api.Services.CompanySide;
using KropkaNet.Objects.Dtos.ClientSide.Company;
using KropkaNet.Objects.Entities.Enum;
using Microsoft.AspNetCore.Mvc;

namespace KropkaNet.Api.Controllers.CompanySide
{
    [Route("api/kropkaNet/warehouse/{warehouseId}")]
    [ApiController]
    //[Authorize(Roles = "Employee, Moderator, Admin")]
    public class WarehouseProductController : ControllerBase
    {
        private readonly IWarehouseProductService _warehouseProductService;

        public WarehouseProductController(IWarehouseProductService warehouseProductService)
        {
            _warehouseProductService = warehouseProductService;
        }

        // GET api/kropkaNet/warehouse/{warehouseId}/products
        [HttpGet("products")]
        public ActionResult<IEnumerable<CompanyDto>> GetFromWarehouse([FromRoute] int warehouseId)
        {
            var companyDtos = _warehouseProductService.GetFromWarehouse(warehouseId);
            return Ok(companyDtos);
        }

        // PATCH api/kropkaNet/warehouse/{warehouseId}/addProduct/{productId}
        [HttpPatch("addProduct/{productId}")]
        public ActionResult AddProduct([FromRoute] int warehouseId, [FromRoute] int productId, [FromQuery] int quantity)
        {
            _warehouseProductService.AddProduct(warehouseId, productId, quantity);

            return Ok();
        }

        // PATCH api/kropkaNet/warehouse/{warehouseId}/removeProduct/{productId}
        [HttpPatch("removeProduct/{productId}")]
        public ActionResult RemoveProduct([FromRoute] int warehouseId, [FromRoute] int productId, [FromQuery] int quantity)
        {
            _warehouseProductService.RemoveProduct(warehouseId, productId, quantity);

            return Ok();
        }

        // GET api/kropkaNet/warehouse/{warehouseId}/export
        [HttpGet("export")]
        public async Task<IActionResult> Export([FromRoute] int warehouseId)
        {
            var fileContent = _warehouseProductService.Export(warehouseId);
            var fileName = $"Products_{System.DateTime.Now:yyyyMMddHHmmss}.xlsx";
            var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            return File(fileContent, contentType, fileName);
        }
    }
}
