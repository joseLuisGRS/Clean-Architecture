using ApplicationLayer_UC.Exeptions;
using EnterpriseLayer;

namespace ApplicationLayer_UC.UseCase
{
    public class GenerateSaleUseCase<TDTO>
    {
        private IRepository<Sale> _repository;
        private readonly IMapper<TDTO, Sale> _mapper;

        public GenerateSaleUseCase(IRepository<Sale> repository, IMapper<TDTO, Sale> mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task ExecuteAsync(TDTO saleDTO)
        {
            var sale = _mapper.ToEntity(saleDTO);
            if (sale.Concepts.Count == 0)
            {
                throw new ValidationException("Una venta debe de tener conceptos");
            }
            if (sale.Total <= 0)
            {
                throw new ValidationException("Una venta debe de tener más de $0.00 en total.");
            }

            await _repository.AddAsync(sale);
        }

    }
}
