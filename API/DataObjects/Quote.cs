using System.ComponentModel.DataAnnotations;

namespace API.DataObjects;

public class Quote {
    [Key]
    public int Id { get; set; }
    [Required]
    public int AuthorId { get; set; }
    [Required]
    public string Content { get; set; }
}