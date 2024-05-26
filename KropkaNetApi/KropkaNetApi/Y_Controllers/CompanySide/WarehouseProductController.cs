using KropkaNetApi.Y_Services.ClientSide;
using KropkaNetApi.Y_Services.CompanySide;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KropkaNetApi.Y_Controllers.CompanySide
{
    [Route("api/kropkaNet/company")]
    [ApiController]
    //[Authorize(Roles = "Employee, Moderator, Admin")]
    public class WarehouseProductController : ControllerBase
    {
        private readonly IWarehouseProductService _warehouseProductService;

        public WarehouseProductController(IWarehouseProductService warehouseProductService)
        {
            _warehouseProductService = warehouseProductService;
        }

    }
}
