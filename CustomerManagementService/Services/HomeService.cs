using AutoMapper;
using Database.Models;
using Repository;

namespace Services
{
    public class HomeService : IHomeService
    {
        private readonly IHomeRepository _homescreenRepository;
        private readonly IMapper _mapper;

        public HomeService(IHomeRepository homescreenRepository, IMapper mapper)
        {
            _homescreenRepository = homescreenRepository;
            _mapper = mapper;
        }
        public async Task<List<BreachCostDTO>> GetBreachCosts()
        {
            List<CostData> breachCost = await _homescreenRepository.GetBreachCostData();
            List<BreachCostDTO> breachCostDtOs = _mapper.Map<List<BreachCostDTO>>(breachCost);

            return breachCostDtOs;
        }

        public async Task<List<GlobalBreachDTO>> GetGlobalBreaches()
        {
            List<GlobalDataBreach> dataBreaches = await _homescreenRepository.GetGlobalBreachData();
            List<GlobalBreachDTO> globalBreachDtOs = _mapper.Map<List<GlobalBreachDTO>>(dataBreaches);

            return globalBreachDtOs;
        }
    }
}
