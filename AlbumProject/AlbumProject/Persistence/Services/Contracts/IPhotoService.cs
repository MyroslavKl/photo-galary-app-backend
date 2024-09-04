using AlbumProject.Models;

namespace AlbumProject.Persistence.Services.Contracts;

public interface IPhotoService
{
    Task<IEnumerable<Photo>> GetPhotosByAlbum(int albumId);
    Task<Photo> AddPhoto(Photo photo);
    Task LikePhoto(int photoId, User user);
    Task DislikePhoto(int photoId, User user);
    Task DeletePhoto(int photoId, User user);
}