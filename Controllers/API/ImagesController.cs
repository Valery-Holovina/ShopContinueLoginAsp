using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shop_P41.Services;


    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : Controller
    {
        
        private readonly IImageService _imageService;
        public ImagesController(IImageService imageService)
        {
            _imageService = imageService;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody]ProductImage image)
        {
            if(ModelState.IsValid)
            {
                var result = await _imageService.InsertImageAsync(image);
                return Ok(Json(result));
            }
            return BadRequest(ModelState);
        }
    }
