namespace InterfaceAdapters_Presenters
{
    public class InventoryViewModel
    {
        public int BeerId { get; set; }
        public int CurrentQuantity { get; set; }
        public int MinimumStock { get; set; }
        public int MaximumStock { get; set; }
        public string Message { get; set; }
    }
}
