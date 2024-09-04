using System.Linq.Expressions;

namespace AlbumProject.Persistence.Repositories.Contracts.Common;

public interface IRepository<TEntity> where TEntity: class
{
    Task<TEntity?> GetOneAsync(Expression<Func<TEntity,bool>>? filter = null);
    Task InsertAsync(TEntity obj);
    void Delete(TEntity obj);
    Task SaveChangesAsync();
}