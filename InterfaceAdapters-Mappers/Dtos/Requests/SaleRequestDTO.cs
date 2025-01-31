
namespace InterfaceAdapters_Mappers.Dtos.Requests
{
    public class SaleRequestDTO
    {
        public List<ConceptRequestDTO> Concepts { get; set; }
    }

    public class ConceptRequestDTO
    {
        public int IdBeer { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }

}
