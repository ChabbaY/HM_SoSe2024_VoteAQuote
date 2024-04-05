using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

using API.DataAccess;
using API.DataObjects;

namespace API.Controllers;

[Route("quotes/{qid}/votes")]
[ApiController]
public class VoteController(Context context) : ControllerBase {
    /// <summary>
    /// Returns all votes of one quote.
    /// </summary>
    /// <param name="qid">QuoteId</param>
    [HttpGet]
    [SwaggerOperation(Tags = new[] { "Vote" })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<Vote[]> GetAllVotes([FromRoute] int qid) {
        return Ok(context.Votes.Where(v => v.QuoteId == qid));
    }

    /// <summary>
    /// Returns the sum of all votes of one quote.
    /// </summary>
    /// <param name="qid">QuoteId</param>
    [HttpGet("aggregate")]
    [SwaggerOperation(Tags = new[] { "Vote" })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<int> GetVoteAggregation([FromRoute] int qid) {
        return Ok(context.Votes.Where(v => v.QuoteId == qid).Sum(x => x.VoteValue));
    }

    /// <summary>
    /// Returns the vote of one quote. Associated to current user.
    /// </summary>
    /// <param name="qid">QuoteId</param>
    /// <param name="user">user string</param>
    [HttpGet("current/{user}")]
    [SwaggerOperation(Tags = new[] { "Vote" })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Vote>> GetVote([FromRoute] int qid, [FromRoute] string user) {
        var value = context.Votes.Where(v => (v.QuoteId == qid) && (v.User == user)).FirstOrDefault();
        if (value == null)
            return NotFound();
        return Ok(value);
    }

    /// <summary>
    /// Adds a vote to one quote. Associated to current user.
    /// </summary>
    /// <param name="qid">QuoteId</param>
    /// <param name="value">new Vote</param>
    /// <param name="user">user string</param>
    [HttpPost("{user}")]
    [SwaggerOperation(Tags = new[] { "Vote" })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<Vote>> AddVote([FromRoute] int qid, [FromBody] Vote value, [FromRoute] string user) {
        if (ModelState.IsValid) {
            //test if vote already exists
            if (context.Votes.Where(v => (v.QuoteId == qid) && (v.User == user)).FirstOrDefault() != null) {
                ModelState.AddModelError("validationError", "Vote already exists");
                return Conflict(ModelState); //vote with qid & uid already exists, we return a conflict
            }

            //test if referenced quote exists
            if (context.Quotes.Any(q => q.Id == qid) is false) {
                ModelState.AddModelError("validationError", "Quote not found");
                return Conflict(ModelState);
            }

            value.VoteValue = (value.VoteValue > 0) ? 1 : -1;
            value.Timestamp = DateTime.UtcNow.ToString();

            value.QuoteId = qid;
            context.Votes.Add(value);
            await context.SaveChangesAsync();

            return Ok(value); //we return the vote
        }
        return BadRequest(ModelState); //Model is not valid -> Validation Annotation of Vote
    }

    /// <summary>
    /// Updates a vote of one quote. Associated to current user.
    /// </summary>
    /// <param name="qid">QuoteId</param>
    /// <param name="value">new Vote</param>
    /// <param name="user">user string</param>
    [HttpPut("{user}")]
    [SwaggerOperation(Tags = new[] { "Vote" })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Vote>> UpdateVote([FromRoute] int qid, [FromBody] Vote value, [FromRoute] string user) {
        if (ModelState.IsValid) {
            var toUpdate = context.Votes.Where(v => (v.QuoteId == qid) && (v.User == user)).FirstOrDefault();
            if (toUpdate != null) {
                toUpdate.VoteValue = (value.VoteValue > 0) ? 1 : -1;
                toUpdate.Timestamp = DateTime.UtcNow.ToString();

                await context.SaveChangesAsync();

                return Ok(toUpdate);
            } else {
                return NotFound(ModelState);
            }
        }
        return BadRequest(ModelState);
    }

    /// <summary>
    /// Delete a vote of one quote. Associated to current user.
    /// </summary>
    /// <param name="qid">QuoteId</param>
    /// <param name="user">user string</param>
    [HttpDelete("{user}")]
    [SwaggerOperation(Tags = new[] { "Vote" })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<Vote>> DeleteVote([FromRoute] int qid, [FromRoute] string user) {
        var toDelete = context.Votes.Where(v => (v.QuoteId == qid) && (v.User == user));
        context.Votes.RemoveRange(toDelete);

        await context.SaveChangesAsync();

        return Ok(toDelete);
    }
}