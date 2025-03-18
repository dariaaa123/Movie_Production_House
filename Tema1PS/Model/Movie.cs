namespace Tema1PS.Model;

public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int Year { get; set; }
    public string Category { get; set; } // Ex: Drama, Comedy, Action
    public string Type { get; set; } // Ex: Artistic, Serial
    public Director Director { get; set; }
    public ScreenWriter Screenwriter { get; set; }
    //public List<string> Images { get; set; }
}