using AlbumProject.Models;
using AlbumProject.Persistence.Repositories.Contracts;
using AlbumProject.Persistence.Services.Contracts;

namespace AlbumProject.Persistence.Services.Implementations;

public class PhotoService:IPhotoService
{
    private readonly IPhotoRepository _photoRepository;
    private readonly IAlbumRepository _albumRepository;

    public PhotoService(IPhotoRepository photoRepository,IAlbumRepository albumRepository)
    {
        _photoRepository = photoRepository;
        _albumRepository = albumRepository;
    }
    public async Task<IEnumerable<Photo>> GetPhotosByAlbum(int albumId)
    {
        var photos = await _photoRepository.GetAll(obj => obj.AlbumId == albumId);
        return photos;
    }

    public async Task<Photo> AddPhoto(Photo photo)
    {
        await _photoRepository.InsertAsync(photo);
        await _photoRepository.SaveChangesAsync();
        return photo;
    }

    public async Task LikePhoto(int photoId, User user)
    {
        var photo = await _photoRepository.GetOneAsync(obj => obj.PhotoId == photoId);
        if (photo == null)
            throw new KeyNotFoundException("Photo not found");

        if (user.Role == "Admin" || user.Role == "User")
        {
            photo.Likes++;
            await _photoRepository.SaveChangesAsync();
        }
        else
        {
            throw new UnauthorizedAccessException("Not allowed to like this photo");
        }
    }

    public async Task DislikePhoto(int photoId, User user)
    {
        var photo = await _photoRepository.GetOneAsync(obj => obj.PhotoId == photoId);
        if (photo == null)
            throw new KeyNotFoundException("Photo not found");

        if (user.Role == "Admin" || user.Role == "User")
        {
            photo.Dislikes++;
            await _photoRepository.SaveChangesAsync();
        }
        else
        {
            throw new UnauthorizedAccessException("Not allowed to dislike this photo");
        }
    }

    public async Task DeletePhoto(int photoId, User user)
    {
        var photo = await _photoRepository.GetOneAsync(obj => obj.PhotoId == photoId);

        if (photo == null)
            throw new KeyNotFoundException("Photo not found");

        var album = await _albumRepository.GetOneAsync(obj => obj.AlbumId == photo.AlbumId);
        if (user.Role != "Admin" && album.UserId != user.UserId)
        {
            throw new UnauthorizedAccessException("Not allowed to delete this photo");
        }
        

        _photoRepository.Delete(photo);
        await _photoRepository.SaveChangesAsync();
    }
}