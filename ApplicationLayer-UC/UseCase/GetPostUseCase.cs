using EnterpriseLayer;

namespace ApplicationLayer_UC.UseCase
{
    public class GetPostUseCase
    {
        private readonly IExternalServiceAdapter<Post> _adapter;

        public GetPostUseCase(IExternalServiceAdapter<Post> adapter)
            => _adapter = adapter;

        public async Task<IEnumerable<Post>> ExecuteAsync()
            => await _adapter.GetDataAsync();
    }
}
