using CarrosAPI.Core.Interfaces.Repositories;
using CarrosAPI.Core.Models;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;


namespace CarrosAPI.Infra.Repository
{
    public class CSVRepository : ICSVRepository
    {
        public async Task<List<CarrosModel>> LerCsvAsync(string caminho)
        {
            List<CarrosModel> records;

            using (var reader = new StreamReader(caminho))
            using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                csv.Context.RegisterClassMap<CarrosModelClassMap>();
                records = new List<CarrosModel>(await csv.GetRecordsAsync<CarrosModel>().ToListAsync());
            }

            return records;
        }

        public async Task EscreverCsvAsync(List<CarrosModel> carros, string caminho)
        {
            using (var writer = new StreamWriter(caminho))
            using (var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                csv.Context.RegisterClassMap<CarrosModelClassMap>();
                await csv.WriteRecordsAsync(carros);
            }
        }
    }
}







