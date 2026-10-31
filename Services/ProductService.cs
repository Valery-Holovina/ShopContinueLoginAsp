using Microsoft.EntityFrameworkCore;

public interface IProductService
{
     Task<IEnumerable<Product>> GetProducts();

     Task<Product?> GetProductById(int id);
     Task<Product?> CreateProduct(Product product);
     Task<Product?> UpdateProduct(int id, Product product);
     Task<Product?> DeleteProduct(int id);
}

public class ProductService : IProductService
{
    private readonly ShopContext _context;

    public ProductService(ShopContext context)
    {
        _context = context;
    }




    
public async Task<IEnumerable<Product>> GetProducts()
{
    return await _context.Products
        .Include(p => p.ProductImages)
        .ToListAsync();
}


    public async Task<Product?> GetProductById(int id)   
    {
     var product = await  _context.Products.FindAsync(id);
     if (product == null)
        {
            return null;
        }
    return product;
  }
    public async Task<Product?> CreateProduct(Product product)
    {
        if (product != null)
        {
        _context.Products.Add(product);
          await _context.SaveChangesAsync();
        return product;
            
        }
        throw new Exception("Error create product");
       
    }

        public async Task<Product?> UpdateProduct(int id, Product product)
    {
        var productToUpdate = await _context.Products.FindAsync(id);
        if(productToUpdate == null)
        {
            return null;
        }
        productToUpdate.Name = product.Name;
        productToUpdate.Price = product.Price;
        productToUpdate.Quantity = product.Quantity;
        productToUpdate.CategotyId = product.CategotyId;
        productToUpdate.Description = product.Description;
        _context.Products.Update(productToUpdate);

         await _context.SaveChangesAsync();
        return productToUpdate;

    }

     public async Task<Product?> DeleteProduct(int id)
    {
        var productToDelete = await _context.Products.FindAsync(id);
        if (productToDelete == null)
        {
             return null;
        }
               
        _context.Products.Remove(productToDelete);
        await _context.SaveChangesAsync();
        return productToDelete;
    }



}

// -- Categories
// INSERT INTO "Categories" ("Name") VALUES
// ('Гаджети'),
// ('Настільні ігри'),
// ('Книги'),
// ('Космос'),
// ('Ретро техніка'),
// ('Розумний дім'),
// ('Спорт'),
// ('Колекційні речі');

// -- Products
// INSERT INTO "Products"
// ("Name", "Price", "Description", "Quantity", "CategotyId")
// VALUES
// (
//     'Розумний кубик Рубіка',
//     2499.99,
//     'Кубик Рубіка з Bluetooth та мобільним додатком для аналізу швидкості складання.',
//     15,
//     1
// ),
// (
//     'Міні-дрон Explorer X',
//     7999.99,
//     'Компактний дрон з камерою 4K та автоматичним поверненням додому.',
//     8,
//     1
// ),
// (
//     'Catan',
//     1799.99,
//     'Популярна стратегічна настільна гра про колонізацію острова.',
//     25,
//     2
// ),
// (
//     'Ticket to Ride Europe',
//     1999.99,
//     'Настільна гра про будівництво залізничних маршрутів по Європі.',
//     18,
//     2
// ),
// (
//     'Марсіанин',
//     499.99,
//     'Науково-фантастичний роман про виживання астронавта на Марсі.',
//     40,
//     3
// ),
// (
//     'Коротка історія часу',
//     599.99,
//     'Відома книга Стівена Гокінга про Всесвіт та фізику.',
//     22,
//     3
// ),
// (
//     'Модель ракети Saturn V',
//     3499.99,
//     'Деталізована колекційна модель легендарної ракети NASA.',
//     10,
//     4
// ),
// (
//     'Метеоритний уламок',
//     8999.99,
//     'Сертифікований невеликий фрагмент справжнього метеорита.',
//     3,
//     4
// ),
// (
//     'Касетний плеєр RetroSound',
//     2999.99,
//     'Сучасний плеєр у стилі 80-х років.',
//     12,
//     5
// ),
// (
//     'Ігрова приставка Retro Game 620',
//     1599.99,
//     '620 класичних ретро-ігор в одному пристрої.',
//     20,
//     5
// ),
// (
//     'Розумна лампа SmartLight Pro',
//     1299.99,
//     'Керування освітленням через смартфон та голосових помічників.',
//     30,
//     6
// ),
// (
//     'Розумна розетка WiFi',
//     699.99,
//     'Дистанційне керування електроприладами.',
//     45,
//     6
// ),
// (
//     'Фітнес-браслет ActiveFit',
//     1899.99,
//     'Моніторинг активності, сну та серцевого ритму.',
//     35,
//     7
// ),
// (
//     'Електрична скакалка',
//     899.99,
//     'Підрахунок стрибків та спалених калорій.',
//     28,
//     7
// ),
// (
//     'Колекційна монета Bitcoin',
//     1499.99,
//     'Сувенірна монета із золотистим покриттям.',
//     16,
//     8
// ),
// (
//     'Фігурка астронавта Apollo',
//     2199.99,
//     'Колекційна фігурка астронавта місії Apollo.',
//     14,
//     8
// );