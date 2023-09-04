
using CarrosAPI.Models;


namespace CarrosAPI.Interfaces.Services
{
    public interface ICSVService
    {
        Task<List<CarrosModel>> LerCsvAsync(string caminho);
        Task EscreverCsvAsync(List<CarrosModel> carros, string caminho);
    }
}
