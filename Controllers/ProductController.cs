using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

public class ProductController: Controller
{

    private readonly IProductService _productService;

    public ProductController (IProductService productService)
    {
        _productService = productService;
    }


   
    public async Task<IActionResult> Index()
    {
        var products = await _productService.GetProducts();
        return View(products);
    }

[HttpGet]
[Authorize(Roles = "admin, Valery, new")]
public IActionResult Image()
{
    Console.WriteLine("GET Image action reached!");
    return View(new ProductImage());
}
   

    [Authorize(Roles = "admin, Valery, new")]
     [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            System.Console.WriteLine($"Id: {id}");
            var product = await _productService.GetProductById(id);
            return View(product);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            if(ModelState.IsValid)
            {
                await _productService.CreateProduct(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

[HttpGet]
        public async Task<IActionResult> Edit(int id)
    {
        var product = await _productService.GetProductById(id);
            return View(product);
    }

    [HttpPost]
        public async Task<IActionResult> Edit(Product product)
    {
        if (ModelState.IsValid)
        {
            var result = await _productService.UpdateProduct(product.Id, product);
            return RedirectToAction(nameof(Index));
        }
        return View(product);
    }

[HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _productService.GetProductById(id);
            return View(product);
    }
    [HttpPost]
    public async Task<IActionResult> DeletePost(int id)
    {
        var result = await _productService.DeleteProduct(id);
        return RedirectToAction(nameof(Index));
    }
}