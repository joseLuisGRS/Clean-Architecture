
using ApplicationLayer_UC;
using EnterpriseLayer;

namespace InterfaceAdapters_Presenters
{
    public class BeerPresenter : IPresenter<Beer, BeerViewModel>
    {
        public IEnumerable<BeerViewModel> Present(IEnumerable<Beer> beers)
        {
            return beers.Select(beer => new BeerViewModel
            {
                Id = beer.Id,
                Name = beer.Name,
                Alcohol = beer.Alcohol + "%"
            });
        }
    }
}
