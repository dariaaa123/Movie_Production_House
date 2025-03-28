namespace Tema1PS.Presenter;

public class MovieDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int Year { get; set; }
    public string Category { get; set; } 
    public string Type { get; set; } 
    public int DirectorId { get; set; }
    public string DirectorName { get; set; } 

    public int ScreenWriterId { get; set; }
    public string ScreenWriterName { get; set; }

    public List<int> ActorIds { get; set; } 
    public List<string> ActorNames { get; set; }  
    
  
    public string Photo1 { get; set; }
    public string Photo2 { get; set; }
    public string Photo3 { get; set; }

    public List<string> GetImageList()
    {
        var images = new List<string>();
        if (!string.IsNullOrEmpty(Photo1)) images.Add(Photo1);
        if (!string.IsNullOrEmpty(Photo2)) images.Add(Photo2);
        if (!string.IsNullOrEmpty(Photo3)) images.Add(Photo3);
        return images;
    }

 
}