using Microsoft.AspNetCore.Mvc;

public class ImageController: Controller
{

    private readonly IImageService _imageService;

    public ImageController(IImageService imageService)
    {
        _imageService = imageService;
    }
    public async Task<IActionResult> Index()
    {
        var images = await _imageService.GetImagesAsync();
        return View(images);
    }

    [HttpGet]
    public IActionResult Insert()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Insert(ProductImage productImage)
    {
        if (ModelState.IsValid)
        {
             await _imageService.InsertImageAsync(productImage);
        return RedirectToAction(nameof(Index));
        }
        return View(productImage);
       
    }
}