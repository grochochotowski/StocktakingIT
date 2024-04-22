using KropkaNetApi.X_Models.CompanySide.Product;
using KropkaNetApi.Y_Services.CompanySide;
using Microsoft.AspNetCore.Mvc;

namespace KropkaNetApi.Y_Controllers.CompanySide
{
    [Route("api/kropkaNet/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }



        // GET /api/kropkaNet/product/list
        [HttpGet("list")]
        public ActionResult<IEnumerable<ProductListDto>> GetList([FromQuery] string? filter)
        {
            var productList = _productService.GetList(filter);
            return Ok(productList);
        }
    }
}
