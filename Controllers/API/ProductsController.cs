using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;



    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

       [Authorize(
    AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,
    Roles = "admin,Valery,new"
)]
[HttpPost]
[HttpPost]
        public async Task<IActionResult> Create([FromBody] Product product)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.CreateProduct(product);
                return Ok(Json(result));
            }
            return BadRequest(new { error = "Parsing model ..." });
        }
        [HttpGet]
        public async Task<IActionResult> Read()
        {
            var products = await _productService.GetProducts();
            return Ok(Json(products));
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] Product product)
        {
            if (ModelState.IsValid)
            {
                var result = await _productService.UpdateProduct(id, product);
                return Ok(Json(result));
            }
            return BadRequest("Error ModelState!");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var result = await _productService.DeleteProduct(id);
            return Ok(Json(result));
        }
    }

