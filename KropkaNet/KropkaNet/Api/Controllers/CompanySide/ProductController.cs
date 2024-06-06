using KropkaNet.Api.Services.CompanySide;
using KropkaNet.Objects.Dtos.ClientSide.Company;
using KropkaNet.Objects.Dtos.CompanySide.Product;
using KropkaNet.Objects.Entities.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KropkaNet.Api.Controllers.CompanySide
{
    [Route("api/kropkaNet/product")]
    [ApiController]
    [Authorize(Roles = "Employee, Moderator, Admin")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }



        // POST: /api/kropkaNet/product/create
        [HttpPost("create")]
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
        public ActionResult<ProductDto> GetById([FromRoute] int productId)
        {
            var productDto = _productService.GetById(productId);
            return Ok(productDto);
        }

        // PUT api/kropkaNet/product/update/5
        [HttpPut("update/{id}")]
        public ActionResult Update([FromRoute] int id, [FromBody] CreateProductDto dto)
        {
            var productId = _productService.Update(id, dto);

            return Ok($"{productId}");
        }

        // DELETE: /api/kropkaNet/product/delete/{productId}
        [HttpDelete("delete/{productId}")]
        public ActionResult Delete([FromRoute] int productId)
        {
            _productService.Delete(productId);

            return NoContent();
        }
    }
}
