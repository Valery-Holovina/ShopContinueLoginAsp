using Microsoft.AspNetCore.Mvc;

public class ReviewController : Controller
{
    private readonly IReviewService _reviewService;

    public ReviewController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

   

      [HttpGet]
    public async Task<IActionResult> GetReviews()
    {
        var reviews = await _reviewService.GetAllReviews();
        return View(reviews);
    }

   
    [HttpGet]
    public async Task<IActionResult> GetReviewById(int id)
    {
        var review = await _reviewService.GetReviewById(id);

        if (review == null)
        {
            return NotFound();
        }

        return View(review);
    }


    [HttpGet]
    public async Task<IActionResult> UpdateReview(int id)
    {
        var review = await _reviewService.GetReviewById(id);

        if (review == null)
        {
            return NotFound();
        }

        return View(review);
    }

  
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateReview(int id, Review review)
    {
        if (id != review.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return View(review);
        }

        try
        {
            await _reviewService.UpdateReview(id, review);
            return RedirectToAction(nameof(GetReviews));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return View(review);
        }
    }

[HttpGet]
public async Task<IActionResult> DeleteReviewConfirm(int id)
{
    var review = await _reviewService.GetReviewById(id);

    if (review == null)
    {
        return NotFound();
    }

    return View(review);
}

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> DeleteReview(int id)
{
    await _reviewService.DeleteReview(id);
    return RedirectToAction(nameof(GetReviews));
}

    
    [HttpGet]
    public IActionResult CreateReview()
    {
        return View();
    }

   
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateReview(Review review)
    {
        if (!ModelState.IsValid)
        {
            return View(review);
        }

        await _reviewService.CreateReview(review);
        return RedirectToAction("GetReviews");
    }
}

