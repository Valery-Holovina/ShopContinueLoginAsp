using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

public class Product
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id{get;set;}
    [Required] 
    [StringLength(50)]
    public string? Name{get;set;}
    [Required]
    [Precision(10,2)] 
    public decimal Price{get;set;}
     [Required]
    [Precision(1024)] 
    public string? Description{get;set;}
    [Required]
    [Range(0, int.MaxValue)]
    public int Quantity{get;set;}

    [Required]
    [Range(0, int.MaxValue)]
    public int CategotyId{get;set;}

    //Navigation properties
     public Category? Category{get;set;}
     public ICollection<OrderItem>? OrderItems{get;set;}
     public ICollection<CartItem>? CartItems{get;set;}
     public ICollection<Review>? Reviews{get;set;}
     public ICollection<ProductImage>? ProductImages{get;set;}


}