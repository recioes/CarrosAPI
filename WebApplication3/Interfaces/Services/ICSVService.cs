using CarrosAPI.Models;
using System.Threading.Tasks;

namespace CarrosAPI.Interfaces.Services
{
    public interface ICSVService
    {
        Task<List<CarrosModel>> ReadCSVFileAsync(string filePath);
        Task WriteAllToCSVFileAsync(string filePath, List<CarrosModel> carros);
        Task WriteToCSVFileAsync(string filePath, CarrosModel carro);
    }
}
