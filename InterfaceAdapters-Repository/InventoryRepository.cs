using ApplicationLayer_UC;
using EnterpriseLayer;
using InterfaceAdapters_Data;
using InterfaceAdapters_Models;
using Microsoft.EntityFrameworkCore;

namespace InterfaceAdapters_Repository
{
    public class InventoryRepository: IRepository<Inventory>
    {
        private readonly AppDbContext _dbContext;

        public InventoryRepository(AppDbContext dbContext)
            => _dbContext = dbContext;

        public async Task AddAsync(Inventory entity)
        {
            var inventoriModel = new InventoryModel()
            {
                BeerId = entity.BeerId,
                CurrentQuantity = entity.CurrentQuantity,
                MinimumStock = entity.MinimumStock,
                MaximumStock = entity.MaximumStock,
            };
            await _dbContext.Inventories.AddAsync(inventoriModel);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Inventory>> GetAllAsync()
        {
            return await _dbContext.Inventories
                .Select(i => new Inventory
                {
                    BeerId = i.BeerId,
                    CurrentQuantity = i.CurrentQuantity,
                    MinimumStock = i.MinimumStock,
                    MaximumStock = i.MaximumStock,
                })                
                .ToListAsync();    
        }

        public async Task<Inventory> GetByIdAsync(int id)
        {
            var inventoryModel = await _dbContext.Inventories.FindAsync(id);
            return new Inventory
            {
                BeerId = inventoryModel.BeerId,
                CurrentQuantity = inventoryModel.CurrentQuantity,
                MinimumStock = inventoryModel.MinimumStock,
                MaximumStock = inventoryModel.MaximumStock,
            };
        }

    }
}
