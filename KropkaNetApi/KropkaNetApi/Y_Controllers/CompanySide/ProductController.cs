using KropkaNetApi.X_Entities.Objects.ClientSide;
using KropkaNetApi.X_Models.CompanySide.Product;
using KropkaNetApi.Y_Services.CompanySide;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;

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

        // GET: /api/kropkaNet/product/list
        [HttpGet("list")]
        public ActionResult<IEnumerable<ProductListDto>> GetList([FromQuery] string? filter)
        {
            var productList = _productService.GetList(filter);
            return Ok(productList);
        }

        // DELETE: /api/kropkaNet/product/delete/{id}
        [HttpDelete("delete")]
        public ActionResult Delete([FromRoute] int id)
        {
            var result = _productService.Delete(id);

            if (result == -1) return NotFound("Product does not exist");
            return NoContent();
        }
    }
}
