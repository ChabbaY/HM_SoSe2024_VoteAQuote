namespace API.DataObjects;

public class VotedQuote {
    public Author Author { get; set; }
    public Quote Quote { get; set; }
    public int Vote { get; set; }
    public string Color { get; set; }
}