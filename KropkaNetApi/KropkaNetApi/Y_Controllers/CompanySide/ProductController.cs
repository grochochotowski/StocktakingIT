using KropkaNetApi.X_Entities.Objects.ClientSide;
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



        // POST: /api/kropkaNet/product/create
        [HttpGet("create")]
        public ActionResult<int> Create([FromBody] CreateProductDto dto)
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
    }
}
