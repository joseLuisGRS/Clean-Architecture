using EnterpriseLayer;
using System.Linq.Expressions;

namespace ApplicationLayer_UC.UseCase
{
    public class GetSaleSearchUseCase<TModel>
    {
        private readonly IRepositorySerch<TModel, Sale> _repository;

        public GetSaleSearchUseCase(IRepositorySerch<TModel, Sale> repository)
            => _repository = repository;

        public async Task<IEnumerable<Sale>> ExecuteAsync(Expression<Func<TModel, bool>> predicate)
            => await _repository.GetAsync(predicate);
    }
}
