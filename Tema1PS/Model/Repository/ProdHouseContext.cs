using Microsoft.EntityFrameworkCore;
using Tema1PS.Model;

namespace Tema1PS.DataBase;

public class ProdHouseContext:DbContext
{
public DbSet<Actor> Actors { get; set; }
public DbSet<ScreenWriter> Screenwriters { get; set; }
public DbSet<Director> Directors { get; set; }
public DbSet<Movie> Movies { get; set; }

public ProdHouseContext(DbContextOptions options) : base(options)
{
    
}

}