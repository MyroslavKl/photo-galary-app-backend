using System.Linq.Expressions;
using AlbumProject.Models;
using AlbumProject.Persistence.Repositories.Contracts.Common;

namespace AlbumProject.Persistence.Repositories.Contracts;

public interface IPhotoRepository:IRepository<Photo>
{
    public Task<IEnumerable<Photo>> GetAll(Expression<Func<Photo,bool>>? filter = null);
}