using AlbumProject.Models;

namespace AlbumProject.Persistence.Services.Contracts;

public interface IAlbumService
{
    IEnumerable<Album> GetAllAlbums();
    IEnumerable<Album> GetAlbumsByUser(int userId);
    Task<Album> CreateAlbum(Album album);
    Task DeleteAlbum(int albumId, User user);
}