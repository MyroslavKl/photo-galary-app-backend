using System.Linq.Expressions;
using AlbumProject.Data;
using AlbumProject.Persistence.Repositories.Contracts.Common;
using Microsoft.EntityFrameworkCore;

namespace AlbumProject.Persistence.Repositories.Implementations.Common;

public class Repository<TEntity>:IRepository<TEntity> where TEntity: class
{
    private readonly AlbumDbContext _dbContext;
    private readonly DbSet<TEntity> _dbSet;

    public Repository(AlbumDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<TEntity>();
    }

    public async Task<TEntity?> GetOneAsync(Expression<Func<TEntity, bool>>? filter = null)
    {
        IQueryable<TEntity> query = _dbSet;
        if (filter is not null)
        {
            query = query.Where(filter);
        }

        return await query.FirstOrDefaultAsync();
    }

    public async Task InsertAsync(TEntity obj)
    { 
        await _dbSet.AddAsync(obj);
    }
    

    public void Delete(TEntity obj)
    {
        _dbSet.Remove(obj);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}