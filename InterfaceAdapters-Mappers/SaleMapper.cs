using ApplicationLayer_UC;
using EnterpriseLayer;
using InterfaceAdapters_Mappers.Dtos.Requests;

namespace InterfaceAdapters_Mappers
{
    public class SaleMapper : IMapper<SaleRequestDTO, Sale>
    {
        public Sale ToEntity(SaleRequestDTO dto)
        {
            var concepts = new List<Concept>();
            foreach (var conceptDTO in dto.Concepts) 
            {
                concepts.Add(new Concept(conceptDTO.IdBeer, conceptDTO.Quantity, conceptDTO.UnitPrice));
            }
            return new Sale(DateTime.Now, concepts);
        }
    }
}
