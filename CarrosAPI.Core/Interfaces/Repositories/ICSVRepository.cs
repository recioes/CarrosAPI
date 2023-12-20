using CarrosAPI.Core.Models;

namespace CarrosAPI.Core.Interfaces.Repositories
{
    public interface ICSVRepository
    {
        Task<List<CarrosModel>> LerCsvAsync(string caminho);
        Task EscreverCsvAsync(List<CarrosModel> carros, string caminho);
    }
}
