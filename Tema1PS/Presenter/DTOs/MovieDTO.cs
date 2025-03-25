namespace Tema1PS.Presenter;

public class MovieDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int Year { get; set; }
    public string Category { get; set; } // Ex: Drama, Comedy, Action
    public string Type { get; set; } // Ex: Artistic, Serial
    public int DirectorId { get; set; }
    public string DirectorName { get; set; } // ✅ New field

    public int ScreenWriterId { get; set; }
    public string ScreenWriterName { get; set; } // ✅ New field

    public List<int> ActorIds { get; set; } = new List<int>();
    public List<string> ActorNames { get; set; } = new List<string>(); 
    
    

 
}