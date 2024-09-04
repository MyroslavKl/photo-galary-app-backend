using System.Linq.Expressions;
using AlbumProject.Data;
using AlbumProject.Models;
using AlbumProject.Persistence.Repositories.Contracts;
using AlbumProject.Persistence.Repositories.Contracts.Common;
using AlbumProject.Persistence.Repositories.Implementations.Common;
using Microsoft.EntityFrameworkCore;

namespace AlbumProject.Persistence.Repositories.Implementations;

public class AlbumRepository:Repository<Album>,IAlbumRepository
{
    private readonly AlbumDbContext _dbContext;
    public AlbumRepository(AlbumDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public IEnumerable<Album> GetAllAlbums(Expression<Func<Album, bool>>? filter = null)
    {
        IQueryable<Album> query = _dbContext.Albums.Include(a =>a.User);
        if (filter is not null)
        {
            query = query.Where(filter);
        }

        return query;
    }
}