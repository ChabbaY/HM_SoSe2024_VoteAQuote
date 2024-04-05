using Microsoft.EntityFrameworkCore;

using API.DataObjects;

namespace API.DataAccess;

/// <summary>
/// Database abstraction for the Base API
/// </summary>
/// <param name="options"></param>
public class Context(DbContextOptions<Context> options) : DbContext(options) {
    /// <summary>
    /// Authors
    /// </summary>
    public DbSet<Author> Authors { get; set; }

    /// <summary>
    /// Quotes
    /// </summary>
    public DbSet<Quote> Quotes { get; set; }

    /// <summary>
    /// Votes
    /// </summary>
    public DbSet<Vote> Votes { get; set; }
}