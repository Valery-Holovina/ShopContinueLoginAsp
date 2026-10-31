
using Microsoft.EntityFrameworkCore;

public interface IImageService
{
    Task<IEnumerable<ProductImage>> GetImagesAsync();
    Task<ProductImage> InsertImageAsync(ProductImage productImage);
    Task<ProductImage?> UpdateImageAsync(int id, ProductImage productImage);
    Task<ProductImage?> DeleteImageAsync(int id);
}

public class ImageService : IImageService
{
    private readonly ShopContext _context;

    public ImageService(ShopContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProductImage>> GetImagesAsync()
    {
        return await _context.ProductImages.ToListAsync();
    }

    public async Task<ProductImage> InsertImageAsync(ProductImage productImage)
    {
        await _context.ProductImages.AddAsync(productImage);
        await _context.SaveChangesAsync();

        return productImage;
    }

    public async Task<ProductImage?> UpdateImageAsync(
        int id,
        ProductImage productImage)
    {
        var productImageUpdate =
            await _context.ProductImages.FindAsync(id);

        if (productImageUpdate == null)
        {
            return null;
        }

        productImageUpdate.ImageUrl = productImage.ImageUrl;
        productImageUpdate.ProductId = productImage.ProductId;

        await _context.SaveChangesAsync();

        return productImageUpdate;
    }

    public async Task<ProductImage?> DeleteImageAsync(int id)
    {
        var productImage =
            await _context.ProductImages.FindAsync(id);

        if (productImage == null)
        {
            return null;
        }

        _context.ProductImages.Remove(productImage);
        await _context.SaveChangesAsync();

        return productImage;
    }
}
