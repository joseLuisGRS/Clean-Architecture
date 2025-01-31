using System.Linq.Expressions;

namespace ApplicationLayer_UC
{
    public interface IRepositorySerch<TModel, TEntity>
    {
        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TModel, bool>> predicate);
    }
}
