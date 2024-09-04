using System.Linq.Expressions;
using AlbumProject.Models;
using AlbumProject.Persistence.Repositories.Contracts.Common;

namespace AlbumProject.Persistence.Repositories.Contracts;

public interface IAlbumRepository:IRepository<Album>
{
    public IEnumerable<Album> GetAllAlbums(Expression<Func<Album,bool>>? filter = null);
}