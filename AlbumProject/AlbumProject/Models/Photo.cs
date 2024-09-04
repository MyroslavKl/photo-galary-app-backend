using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace AlbumProject.Models;

public class Photo
{
    public int PhotoId { get; set; }
    public string FilePath { get; set; } = string.Empty;
    public string ThumbnailPath { get; set; } = string.Empty;
    public int Likes { get; set; }
    public int Dislikes { get; set; }
    
    public int AlbumId { get; set; }
    [ForeignKey("AlbumId")] 
    public Album Album { get; set; } = null!;
}