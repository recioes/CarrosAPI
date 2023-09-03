using CarrosAPI.Interfaces.Services;
using CarrosAPI.Models;

namespace CarrosAPI.Services
{
    public class CSVService : ICSVService
    {

        public async Task<List<CarrosModel>> ReadCSVFileAsync(string filePath)
        {
            List<CarrosModel> carros = new List<CarrosModel>();

            using (StreamReader reader = new StreamReader(filePath))
            {
                string linha;
                while ((linha = await reader.ReadLineAsync()) != null)
                {
                    string[] campos = linha.Split(',');
                    if (campos.Length == 5)
                    {
                        carros.Add(new CarrosModel
                        {
                            Id = int.Parse(campos[0]),
                            Cor = campos[1],
                            Marca = campos[2],
                            Modelo = campos[3],
                            Ano = int.Parse(campos[4])
                        });
                    }
                }
            }

            return carros;
        }

        public async Task WriteAllToCSVFileAsync(string filePath, List<CarrosModel> carros)
        {
            using (StreamWriter writer = new StreamWriter(filePath, append: false))
            {
                foreach (var carro in carros)
                {
                    await writer.WriteLineAsync($"{carro.Id},{carro.Cor},{carro.Marca},{carro.Modelo},{carro.Ano}");
                }
            }
        }

        public async Task WriteToCSVFileAsync(string filePath, CarrosModel carro)
        {
            using (StreamWriter writer = new StreamWriter(filePath, append: true))
            {
                await writer.WriteLineAsync($"{carro.Id},{carro.Cor},{carro.Marca},{carro.Modelo},{carro.Ano}");
            }
        }
    }
}
    






