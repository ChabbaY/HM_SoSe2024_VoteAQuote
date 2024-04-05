using System.ComponentModel.DataAnnotations;

namespace API.DataObjects;

public class Author {
    [Key]
    public int Id { get; set; }
    [Required]
    [MinLength(1)]
    [MaxLength(50)]
    public string Name { get; set; }
    [MaxLength(50)]
    public string GivenName { get; set; }
    [MaxLength(50)]
    public string Lifetime { get; set; }
    public string Description { get; set; }
}