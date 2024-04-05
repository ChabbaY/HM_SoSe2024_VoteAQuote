using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

using API.DataAccess;
using API.DataObjects;

namespace API.Controllers;

[Route("quotes")]
[ApiController]
public class QuoteController(Context context) : ControllerBase {
    /// <summary>
    /// Returns all quotes of one author.
    /// </summary>
    /// <param name="aid">AuthorId</param>
    [HttpGet]
    [SwaggerOperation(Tags = new[] { "Quote" })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<Quote[]> GetAllQuotes([FromRoute] int aid) {
        return Ok(context.Quotes.Where(v => v.AuthorId == aid)
            .OrderBy(x => x.Content));
    }

    /// <summary>
    /// Returns the quote with a given id of one author.
    /// </summary>
    /// <param name="aid">AuthorId</param>
    /// <param name="qid">QuoteId</param>
    [HttpGet("{qid}")]
    [SwaggerOperation(Tags = new[] { "Quote" })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Quote> GetQuote([FromRoute] int aid, [FromRoute] int qid) {
        var value = context.Quotes.Where(v => (v.AuthorId == aid) && (v.Id == qid)).FirstOrDefault();
        if (value == null) return NotFound();
        return Ok(value);
    }

    /// <summary>
    /// Adds a quote to one author.
    /// </summary>
    /// <param name="aid">AuthorId</param>
    /// <param name="value">new Quote</param>
    [HttpPost]
    [SwaggerOperation(Tags = new[] { "Quote" })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<Quote>> AddQuote([FromRoute] int aid, [FromBody] Quote value) {
        if (ModelState.IsValid) {
            //test if quote already exists
            if (context.Quotes.Where(v => v.Id == value.Id).FirstOrDefault() != null) {
                ModelState.AddModelError("validationError", "Quote already exists");
                return Conflict(ModelState); //quote with id already exists, we return a conflict
            }

            //test if referenced author exists
            if (context.Authors.Any(a => a.Id == aid) is false) {
                ModelState.AddModelError("validationError", "Author not found");
                return Conflict(ModelState);
            }

            value.AuthorId = aid;
            context.Quotes.Add(value);
            await context.SaveChangesAsync();

            return Ok(value); //we return the quote
        }
        return BadRequest(ModelState); //Model is not valid -> Validation Annotation of Quote
    }

    /// <summary>
    /// Updates a quote of one author.
    /// </summary>
    /// <param name="aid">AuthorId</param>
    /// <param name="qid">QuoteId</param>
    /// <param name="value">new Quote</param>
    [HttpPut("{qid}")]
    [SwaggerOperation(Tags = new[] { "Quote" })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Quote>> UpdateQuote([FromRoute] int aid, [FromRoute] int qid, [FromBody] Quote value) {
        if (ModelState.IsValid) {
            var toUpdate = context.Quotes.Where(v => (v.AuthorId == aid) && (v.Id == qid)).FirstOrDefault();
            if (toUpdate != null) {
                toUpdate.Content = value.Content;

                await context.SaveChangesAsync();

                return Ok(value);
            } else {
                return NotFound(ModelState);
            }
        }
        return BadRequest(ModelState);
    }

    /// <summary>
    /// Delete a quote of one author.
    /// Blocks if referenced by a Vote.
    /// </summary>
    /// <param name="aid">AuthorId</param>
    /// <param name="qid">QuoteId</param>
    [HttpDelete("{qid}")]
    [SwaggerOperation(Tags = new[] { "Quote" })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<Quote>> DeleteQuote([FromRoute] int aid, [FromRoute] int qid) {
        if (context.Votes.Where(v => v.QuoteId == qid).Any()) {
            //block because of reference
            ModelState.AddModelError("referentialIntegrityViolation", "Quote referenced by a Vote");
            return Conflict(ModelState);
        }
        
        var toDelete = context.Quotes.Where(v => (v.AuthorId == aid) && (v.Id == qid));
        context.Quotes.RemoveRange(toDelete);

        await context.SaveChangesAsync();

        return Ok(toDelete);
    }
}