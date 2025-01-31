
namespace ApplicationLayer_UC
{
    public interface IExternalServiceAdapter<T>
    {
        Task<IEnumerable<T>> GetDataAsync();
    }
}
