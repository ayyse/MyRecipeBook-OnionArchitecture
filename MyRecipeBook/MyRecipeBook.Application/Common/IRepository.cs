using MyRecipeBook.Domain.Entities;

namespace MyRecipeBook.Application.Common;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity?> GetById(Guid id);
    Task<List<TEntity>> GetAll();
    Task Create(TEntity entity);
    void Update(TEntity entity);
    Task Delete(Guid id);
}