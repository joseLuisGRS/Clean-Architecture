namespace EnterpriseLayer
{
    public class Inventory
    {
        public int BeerId { get; set; }
        public int CurrentQuantity { get; set; }
        public int MinimumStock { get; set; }
        public int MaximumStock { get; set; }
        public bool IsScarce() => CurrentQuantity <= MinimumStock;
        public bool IsExistence () => CurrentQuantity > 0;

    }
}
