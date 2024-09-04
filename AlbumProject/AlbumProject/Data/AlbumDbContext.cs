using AlbumProject.Models;
using Microsoft.EntityFrameworkCore;

namespace AlbumProject.Data;

public class AlbumDbContext:DbContext
{
    public AlbumDbContext(DbContextOptions<AlbumDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Album> Albums { get; set; }
    public DbSet<Photo> Photos { get; set; }
}