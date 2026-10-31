using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class ProductImage
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [MaxLength(500)]
    public string ImageUrl { get; set; } = null!;

    [Required]
    public int ProductId { get; set; }

    [ForeignKey(nameof(ProductId))]
    [JsonIgnore]
    public Product? Product { get; set; }
}