using Azure;
using KropkaNetApi.X_Models.ClientSide.Order;
using KropkaNetApi.Y_Controllers.CompanySide;
using KropkaNetApi.Y_Services.ClientSide;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KropkaNetApi.Y_Controllers.ClientSide
{
    [Route("api/kropkaNet/department")]
    [ApiController]
    [Authorize]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpPost("create")]
        public ActionResult Create([FromBody] CreateDepartmentDto dto)
        {
            var createdOrderId = _departmentService.Create(dto);

            var result = Created($"{createdDepartmentId}", null) as CreatedResult;
            if (result != null)
            {
                Response.Headers.Add("Access-Control-Expose-Headers", "Location");
            }

            return result;
        }
    }
}
