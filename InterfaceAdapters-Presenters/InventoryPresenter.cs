using ApplicationLayer_UC;
using EnterpriseLayer;

namespace InterfaceAdapters_Presenters
{
    public class InventoryPresenter : IPresenter<Inventory, InventoryViewModel>
    {
        public IEnumerable<InventoryViewModel> Present(IEnumerable<Inventory> inventories)
            => inventories.Select(i => new InventoryViewModel
            {
                BeerId = i.BeerId,
                CurrentQuantity = i.CurrentQuantity,
                MinimumStock = i.MinimumStock,
                MaximumStock = i.MaximumStock,
                Message = i.IsExistence() ? i.IsScarce() ? "El producto esta por agotarse" : "" : "El producto se ha agotado!"
            });

    }
}
