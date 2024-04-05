using System.ComponentModel.DataAnnotations;

namespace API.DataObjects;

public class Vote {
    [Key]
    public int Id { get; set; }
    [Required]
    public int QuoteId { get; set; }
    [Required]
    public string User { get; set; }
    [Required]
    public int VoteValue { get; set; }
    public string Timestamp { get; set; }
}