using Microsoft.EntityFrameworkCore;

public interface IReviewService
{
    public Task<IEnumerable<Review>> GetAllReviews();
    public Task<Review> CreateReview(Review review);
    public Task<Review> UpdateReview(int id, Review review);
    public Task<Review> GetReviewById(int id);
    public Task<Review> DeleteReview(int id);
    
}

public class ReviewService: IReviewService
{
    private readonly ShopContext _context;

    public ReviewService(ShopContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Review>>GetAllReviews (){
        return await _context.Reviews.ToListAsync();
    }

    public async Task<Review> CreateReview(Review review)
    {
        if (review != null)
        {
            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();
            return review;
        }
         throw new Exception("Review is null");
        
    }

    public async Task<Review> UpdateReview(int id, Review review)
    {
        var reviewId = await _context.Reviews.FindAsync(id);

         if (reviewId == null){
        throw new Exception("Review not found");}

        reviewId.Comment = review.Comment;
        reviewId.Rating = review.Rating;
        reviewId.ProductId = review.ProductId;
        reviewId.UserId = review.UserId;

        await _context.SaveChangesAsync();
        return reviewId;
    }
    public async Task<Review> GetReviewById(int id)
    {
        var reviewId =  await _context.Reviews.FindAsync(id);
        if(reviewId != null)
        {
            return reviewId;
        }
         throw new Exception("Review not found ...");
    }

     public async Task<Review> DeleteReview(int id)
    {
        var reviewId = await _context.Reviews.FindAsync(id);
        if(reviewId != null)
        {
        _context.Remove(reviewId);
         await _context.SaveChangesAsync();
         return reviewId;
        }
         throw new Exception("Review not found ...");
        

    }
}