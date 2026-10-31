using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class Review
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Range(1, 5)]
    public int Rating { get; set; }

    [Required]
    [MaxLength(100)]
    public string? Comment { get; set; }

    [Required]
    public int ProductId { get; set; }

    [Required]
    public string UserId { get; set; } = string.Empty;

    [ForeignKey(nameof(ProductId))]
    public Product? Product { get; set; }

    [ForeignKey(nameof(UserId))]
    public User? User { get; set; }
}