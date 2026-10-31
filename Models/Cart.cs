using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class Cart
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string UserId { get; set; } = null!;

    [ForeignKey(nameof(UserId))]
    [JsonIgnore]
    public User User { get; set; } = null!;
    public ICollection<CartItem> Items { get; set; } = new List<CartItem>();
}