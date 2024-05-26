using KropkaNetApi.X_Entities.Enum;
using KropkaNetApi.X_Entities.Objects.ClientSide;
using KropkaNetApi.X_Entities.Objects.CompanySide;
using KropkaNetApi.X_Models.ClientSide.Company;
using KropkaNetApi.X_Models.ClientSide.Department;
using KropkaNetApi.X_Models.CompanySide.Product;
using KropkaNetApi.Y_Services.CompanySide;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace KropkaNetApi.Y_Controllers.CompanySide
{
    [Route("api/kropkaNet/product")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }



        // POST: /api/kropkaNet/product/create
        [HttpPost("create")]
        [Authorize(Roles = "Employee, Moderator, Admin")]
        public ActionResult Create([FromBody] CreateProductDto dto)
        {
            var createdProductId = _productService.Create(dto);

            var result = Created($"{createdProductId}", null) as CreatedResult;
            if (result != null)
            {
                Response.Headers.Add("Access-Control-Expose-Headers", "Location");
            }

            return result;
        }

        // GET api/kropkaNet/product/all
        [HttpGet("all")]
        [Authorize(Roles = "Employee, Moderator, Admin")]
        public ActionResult<IEnumerable<CompanyDto>> GetAll(
            [FromQuery] int page,
            [FromQuery] string? filters,
            [FromQuery] string? sortBy,
            [FromQuery] SortDirection sortDireciton
            )
        {
            var companyDtos = _productService.GetAll(page, filters, sortBy, sortDireciton);
            return Ok(companyDtos);
        }

        // GET api/kropkaNet/product/{id}
        [HttpGet("{productId}")]
        [Authorize(Roles = "Employee, Moderator, Admin")]
        public ActionResult<IEnumerable<ProductDto>> GetById(
            [FromRoute] int productId,
            [FromQuery] int page,
            [FromQuery] string? filters,
            [FromQuery] string? sortBy,
            [FromQuery] SortDirection sortDireciton
            )
        {
            var productDtos = _productService.GetById(productId, page, filters, sortBy, sortDireciton);
            return Ok(productDtos);
        }

        // PUT api/kropkaNet/product/update/5
        [HttpPut("update/{id}")]
        [Authorize(Roles = "Employee, Moderator, Admin")]
        public ActionResult Update([FromRoute] int id, [FromBody] CreateProductDto dto)
        {
            var productId = _productService.Update(id, dto);

            return Ok($"{productId}");
        }

        // DELETE: /api/kropkaNet/product/delete/{id}
        [HttpDelete("delete")]
        [Authorize(Roles = "Employee, Moderator, Admin")]
        public ActionResult Delete([FromRoute] int id)
        {
            var result = _productService.Delete(id);

            if (result == -1) return NotFound("Product does not exist");
            return NoContent();
        }
    }
}
