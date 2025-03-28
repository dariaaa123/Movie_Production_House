namespace Tema1PS.Model;

public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int Year { get; set; }
    public string Category { get; set; } // Ex: Drama, Comedy, Action
    public string Type { get; set; } // Ex: Artistic, Serial
    public int DirectorId { get; set; }
    public int ScreenWriterId { get; set; }
    
    public string Photo1 { get; set; } // New column
    public string Photo2 { get; set; } // New column
    public string Photo3 { get; set; } // New column
}