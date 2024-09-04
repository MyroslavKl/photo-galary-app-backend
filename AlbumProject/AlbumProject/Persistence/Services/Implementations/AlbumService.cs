using AlbumProject.Models;
using AlbumProject.Persistence.Repositories.Contracts;
using AlbumProject.Persistence.Services.Contracts;

namespace AlbumProject.Persistence.Services.Implementations;

public class AlbumService : IAlbumService
{
    private readonly IAlbumRepository _albumRepository;

    public AlbumService(IAlbumRepository albumRepository)
    {
        _albumRepository = albumRepository;
    }

    public IEnumerable<Album> GetAllAlbums()
    {
        var album = _albumRepository.GetAllAlbums();
        return album;
    }

    public IEnumerable<Album> GetAlbumsByUser(int userId)
    {
        var albums = _albumRepository.GetAllAlbums(obj => obj.UserId == userId);
        return albums;
    }

    public async Task<Album> CreateAlbum(Album album)
    {
        await _albumRepository.InsertAsync(album);
        await _albumRepository.SaveChangesAsync();
        return album;
    }

    public async Task DeleteAlbum(int albumId, User user)
    {
        var album = await _albumRepository.GetOneAsync(obj => obj.AlbumId == albumId);

        if (album == null)
            throw new KeyNotFoundException("Album not found");

        if (user.Role != "Admin" && album.UserId != user.UserId)
            throw new UnauthorizedAccessException("Not allowed to delete this album");

        _albumRepository.Delete(album);
        await _albumRepository.SaveChangesAsync();
    }
}