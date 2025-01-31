namespace InterfaceAdapters_Models
{
    public class InventoryModel
    {
        public int Id { get; set; }
        public BeerModel Beer { get; set; }
        public int BeerId { get; set; }
        public int CurrentQuantity { get; set; }
        public int MinimumStock { get; set; }
        public int MaximumStock { get; set; }

    }
}
