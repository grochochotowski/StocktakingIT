using KropkaNetApi.X_Models.ClientSide.Company;
using KropkaNetApi.Y_Services.CompanySide;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KropkaNetApi.Y_Controllers.ClientSide
{
    [Route("api/kropkaNet/position")]
    [ApiController]
    [Authorize]
    public class CompanyController : ControllerBase
    {
        /*private readonly ICompanyService _companyService;

        public PositionController(IPositionService positionService)
        {
            _positionService = positionService;
        }*/
    }
}
