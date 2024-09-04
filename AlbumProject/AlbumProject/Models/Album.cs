using System.ComponentModel.DataAnnotations.Schema;

namespace AlbumProject.Models;

public class Album
{
    public int AlbumId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    
    public int UserId { get; set; } 
    [ForeignKey("UserId")]
    public User User { get; set; } = null!;
    
    public ICollection<Photo> Photos { get; set; } = new List<Photo>();
}