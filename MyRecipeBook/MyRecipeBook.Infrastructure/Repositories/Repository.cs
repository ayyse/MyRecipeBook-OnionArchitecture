using Microsoft.EntityFrameworkCore;
using MyRecipeBook.Application.Common;
using MyRecipeBook.Domain.Entities;
using MyRecipeBook.Infrastructure.DbContexts;

namespace MyRecipeBook.Infrastructure.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    private readonly MyRecipeBookDbContext _myRecipeBookDbContext;
    private readonly DbSet<TEntity> _dbSet;

    public Repository(MyRecipeBookDbContext myRecipeBookDbContext)
    {
        _myRecipeBookDbContext = myRecipeBookDbContext;
        _dbSet = _myRecipeBookDbContext.Set<TEntity>();
    }
    
    public async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<TEntity>> GetAllAsync()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }

    public async Task AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public Task UpdateAsync(TEntity entity)
    {
        _dbSet.Update(entity);
        return Task.CompletedTask;
    }

    public Task RemoveAsync(TEntity entity)
    {
        _dbSet.Remove(entity);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await _myRecipeBookDbContext.SaveChangesAsync();
    }
}