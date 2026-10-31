using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class CartItem
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public int CartId { get; set; }

    [ForeignKey(nameof(CartId))]
    [JsonIgnore]
    public Cart Cart { get; set; } = null!;

    [Required]
    public int ProductId { get; set; }

    [ForeignKey(nameof(ProductId))]
    [JsonIgnore]
    public Product Product { get; set; } = null!;

    [Range(1, 1000)]
    public int Quantity { get; set; }
}