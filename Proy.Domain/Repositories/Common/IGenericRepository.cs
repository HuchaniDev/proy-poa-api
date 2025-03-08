namespace Proy.Domain.Repositories.Common;

public interface IGenericRepository<TEntity> where TEntity : class
{
    public Task<TEntity> SaveAsync(TEntity entity);
    public Task<TEntity?> GetByIdAsync(int id); 
    public Task<bool>DeleteHardAsync(int id); 
    public Task<bool>ExistsByIdAsync(int id); 
}