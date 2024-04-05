using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

using API.DataAccess;
using API.DataObjects;

namespace API.Controllers;

[Route("authors")]
[ApiController]
public class AuthorController(Context context) : ControllerBase {
    /// <summary>
    /// Returns all authors.
    /// </summary>
    [HttpGet]
    [SwaggerOperation(Tags = new[] { "Author" })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<Author[]> GetAllAuthors() {
        return Ok(context.Authors.OrderBy(x => x.Name)
            .ThenBy(x => x.GivenName).ToArray());
    }

    /// <summary>
    /// Returns the author with a given id.
    /// </summary>
    /// <param name="aid">AuthorId</param>
    [HttpGet("{aid}")]
    [SwaggerOperation(Tags = new[] { "Author" })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<Author> GetAuthor([FromRoute] int aid) {
        var value = context.Authors.Where(v => v.Id == aid).FirstOrDefault();
        if (value == null) return NotFound();
        return Ok(value);
    }

    /// <summary>
    /// Adds an author.
    /// </summary>
    /// <param name="value">new Author</param>
    [HttpPost]
    [SwaggerOperation(Tags = new[] { "Author" })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<Author>> AddAuthor([FromBody] Author value) {
        if (ModelState.IsValid) {
            //test if author already exists
            if (context.Authors.Where(v => v.Id == value.Id).FirstOrDefault() != null) {
                ModelState.AddModelError("validationError", "Author already exists");
                return Conflict(ModelState); //author with id already exists, we return a conflict
            }

            context.Authors.Add(value);
            await context.SaveChangesAsync();

            return Ok(value); //we return the author
        }
        return BadRequest(ModelState); //Model is not valid -> Validation Annotation of Author
    }

    /// <summary>
    /// Updates an author.
    /// </summary>
    /// <param name="aid">AuthorId</param>
    /// <param name="value">new Author</param>
    [HttpPut("{aid}")]
    [SwaggerOperation(Tags = new[] { "Author" })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Author>> UpdateAuthor([FromRoute] int aid, [FromBody] Author value) {
        if (ModelState.IsValid) {
            var toUpdate = context.Authors.Where(v => v.Id == aid).FirstOrDefault();
            if (toUpdate != null) {
                toUpdate.Name = value.Name;
                toUpdate.GivenName = value.GivenName;
                toUpdate.Lifetime = value.Lifetime;
                toUpdate.Description = value.Description;

                await context.SaveChangesAsync();

                return Ok(value);
            } else {
                return NotFound(ModelState);
            }
        }
        return BadRequest(ModelState);
    }

    /// <summary>
    /// Delete an author.
    /// Blocks if referenced by a Quote.
    /// </summary>
    /// <param name="aid">AuthorId</param>
    [HttpDelete("{aid}")]
    [SwaggerOperation(Tags = new[] { "Author" })]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<Author>> DeleteAuthor([FromRoute] int aid) {
        if (context.Quotes.Where(v => v.AuthorId == aid).Any()) {
            //block because of reference
            ModelState.AddModelError("referentialIntegrityViolation", "Author referenced by a Quote");
            return Conflict(ModelState);
        }

        var toDelete = context.Authors.Where(v => v.Id == aid);
        context.Authors.RemoveRange(toDelete);

        await context.SaveChangesAsync();

        return Ok(toDelete);
    }
}