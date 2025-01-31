
namespace ApplicationLayer_UC
{
    public interface IPresenter<TModel, TOutput>
    {
        public IEnumerable<TOutput> Present(IEnumerable<TModel> data);
    }
}
