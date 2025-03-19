using Microsoft.EntityFrameworkCore;
using Tema1PS.Model;

namespace Tema1PS.DataBase;

public class ProdHouseContext : DbContext
{
    public DbSet<Actor> Actors { get; set; }
    public DbSet<ScreenWriter> Screenwriters { get; set; }
    public DbSet<Director> Directors { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<MoviesActors> MoviesActors { get; set; }

    public ProdHouseContext(DbContextOptions<ProdHouseContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Define Composite Primary Key for MoviesActors
        modelBuilder.Entity<MoviesActors>()
            .HasKey(ma => new { ma.MovieId, ma.ActorId });

        base.OnModelCreating(modelBuilder);
    }
}