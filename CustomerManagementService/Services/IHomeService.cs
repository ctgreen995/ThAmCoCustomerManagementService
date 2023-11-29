using DTO;

namespace Services
{
    public interface IHomeService
    {
        Task<List<CostDTO>> GetData();
    }
}
