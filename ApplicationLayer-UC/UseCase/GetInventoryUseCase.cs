using EnterpriseLayer;

namespace ApplicationLayer_UC.UseCase
{
    public class GetInventoryUseCase<TEntity, TOutput>
    {
        private readonly IRepository<TEntity> _repository;
        private readonly IPresenter<TEntity, TOutput> _presenter;

        public GetInventoryUseCase(IRepository<TEntity> repository, IPresenter<TEntity, TOutput> presenter)
        {
            _repository = repository;
            _presenter = presenter;
        }

        public async Task<IEnumerable<TOutput>> ExecuteAsync()
        {
            var inventories = await _repository.GetAllAsync();
            return _presenter.Present(inventories);
        }
    }
}
