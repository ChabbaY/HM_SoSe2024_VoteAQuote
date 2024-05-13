using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

using API.DataAccess;
using API.DataObjects;

namespace API.Controllers;

[Route("ranking")]
[ApiController]
public class RankingController(Context context) : ControllerBase {
    /// <summary>
    /// Returns the ranking (all quotes with votes, sorted).
    /// </summary>
    [HttpGet]
    [SwaggerOperation(Tags = new[] { "Ranking" })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<VotedQuote[]> GetVoteRanking() {
        List<VotedQuote> result = [];
        var quotes = context.Quotes.ToList();
        foreach (Quote quote in quotes) {
            Author author = context.Authors.Where(a => a.Id == quote.AuthorId).First();
            int vote = context.Votes.Where(v => v.QuoteId == quote.Id).Sum(v => v.VoteValue);
            result.Add(new VotedQuote() {
                Author = author,
                Quote = quote,
                Vote = vote,
                Color = ""
            });
        }
        result.Sort((a, b) => b.Vote - a.Vote);

        return Ok(result.ToArray());
    }
}