using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Category
{
     [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id{get;set;}

    [Required]
    [MaxLength(100)]
    public string? Name{get;set;}

    //Navigation Property
    public ICollection<Product>? Products{get;set;}
}