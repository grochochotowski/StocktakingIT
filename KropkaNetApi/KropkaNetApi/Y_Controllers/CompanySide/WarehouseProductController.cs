using KropkaNetApi.X_Entities.Enum;
using KropkaNetApi.X_Models.ClientSide.Company;
using KropkaNetApi.Y_Services.CompanySide;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KropkaNetApi.Y_Controllers.CompanySide
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
        public ActionResult<IEnumerable<CompanyDto>> GetFromWarehouse(
            [FromRoute] int warehouseId,
            [FromQuery] int page,
            [FromQuery] string? filters,
            [FromQuery] string? sortBy,
            [FromQuery] SortDirection sortDireciton
            )
        {
            var companyDtos = _warehouseProductService.GetFromWarehouse(warehouseId, page, filters, sortBy, sortDireciton);
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
    }
}
