using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

using API.DataAccess;
using API.DataObjects;

namespace API.Controllers;

[Route("ranking")]
[ApiController]
public class RankingController(Context context) : ControllerBase {
    /// <summary>
    /// Returns the ranking (all quotes with votes, sorted). Contains a user's vote.
    /// <param name="user">user string</param>
    /// </summary>
    [HttpGet("{user}")]
    [SwaggerOperation(Tags = new[] { "Ranking" })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<VotedQuote[]> GetVoteRanking([FromRoute] string user) {
        List<VotedQuote> result = [];
        var quotes = context.Quotes.ToList();
        foreach (Quote quote in quotes) {
            Author author = context.Authors.Where(a => a.Id == quote.AuthorId).First();
            int vote = context.Votes.Where(v => v.QuoteId == quote.Id).Sum(v => v.VoteValue);
            var userVote = context.Votes.Where(v => (v.QuoteId == quote.Id) && (v.User == user)).FirstOrDefault();
            int uservote = (userVote != null) ? userVote.VoteValue : 0;
            result.Add(new VotedQuote() {
                Author = author,
                Quote = quote,
                Vote = vote,
                Color = "",
                Uservote = uservote
            });
        }
        result.Sort((a, b) => b.Vote - a.Vote);

        return Ok(result.ToArray());
    }
}