using Microsoft.EntityFrameworkCore;
using MyRecipeBook.Application.Common;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Infrastructure.DbContexts;

namespace MyRecipeBook.Infrastructure.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    private readonly MyRecipeBookDbContext _myRecipeBookDbContext;

    public Repository(MyRecipeBookDbContext myRecipeBookDbContext)
    {
        _myRecipeBookDbContext = myRecipeBookDbContext;
    }
    
    public async Task<TEntity?> GetById(Guid id)
    {
        return await _myRecipeBookDbContext.Set<TEntity>()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<TEntity>> GetAll()
    {
        return await _myRecipeBookDbContext.Set<TEntity>()
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task Create(TEntity entity)
    {
        await _myRecipeBookDbContext.Set<TEntity>().AddAsync(entity);
    }

    public void Update(TEntity entity)
    {
        _myRecipeBookDbContext.Set<TEntity>().Update(entity);
    }

    public async Task Delete(Guid id)
    {
        var entity = await GetById(id);
        if (entity != null)
        {
            _myRecipeBookDbContext.Set<TEntity>().Remove(entity);
        }
    }
}