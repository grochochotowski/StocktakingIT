using KropkaNetApi.X_Entities.Enum;
using KropkaNetApi.X_Models.ClientSide.Company;
using KropkaNetApi.Y_Services.ClientSide;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace KropkaNetApi.Y_Controllers.ClientSide
{
    [Route("api/kropkaNet/company")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        // PSOT api/kropkaNet/company/create/{id}
        [HttpPost("create/{userId}")]
        [Authorize(Roles = "User")]
        public ActionResult Create([FromRoute] int userId, [FromBody] CreateCompanyDto dto)
        {
            var createdCompanyId = _companyService.Create(userId, dto);

            var result = Created($"{createdCompanyId}", null) as CreatedResult;
            if (result != null)
            {
                Response.Headers.Add("Access-Control-Expose-Headers", "Location");
            }

            return result;
        }

        // GET api/kropkaNet/company/user/{id}
        [HttpGet("user/{userId}")]
        [Authorize]
        public ActionResult<IEnumerable<CompanyDto>> GetListUser(
            [FromRoute] int userId,
            [FromQuery] int page,
            [FromQuery] string? filters,
            [FromQuery] string? sortBy,
            [FromQuery] SortDirection sortDireciton
            )
        {
            var companyDtos = _companyService.GetListUser(userId, page, filters, sortBy, sortDireciton);
            return Ok(companyDtos);
        }

        // GET api/kropkaNet/company/all
        [HttpGet("all")]
        [Authorize(Roles = "Employee, Moderator, Admin")]
        public ActionResult<IEnumerable<CompanyDto>> GetList(
            [FromQuery] int page,
            [FromQuery] string? filters,
            [FromQuery] string? sortBy,
            [FromQuery] SortDirection sortDireciton
            )
        {
            var companyDtos = _companyService.GetList(page, filters, sortBy, sortDireciton);
            return Ok(companyDtos);
        }
    }
}
