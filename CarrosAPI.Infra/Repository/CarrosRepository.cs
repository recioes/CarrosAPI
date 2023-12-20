using CarrosAPI.Core.Interfaces.Repositories;
using CarrosAPI.Core.Models;


namespace CarrosAPI.Infra.Repository
 
{

    public class CarroRepository : ICarrosRepository
    {
        private readonly ICSVRepository _csvRepository;
        private readonly string _caminho = @"C:\\Users\\esther.pereira\\source\\repos\\CarrosAPI\\CarrosAPI.Infra\\DataBase\\Carros.csv";
        


        public CarroRepository(ICSVRepository csvRepository)
        {
            _csvRepository = csvRepository;
          
        
        }
        public async Task<List<CarrosModel>> BuscarTodosCarrosAsync()
        {
            return await _csvRepository.LerCsvAsync(_caminho);
        }

        public async Task<CarrosModel> BuscarPorIdAsync(int id)
        {
            var carros = await BuscarTodosCarrosAsync();
            return carros.FirstOrDefault(c => c.Id == id);
        }

        public async Task AdicionarAsync(CarrosModel carro)
        {
            var carros = await BuscarTodosCarrosAsync();
            carros.Add(carro);
            await _csvRepository.EscreverCsvAsync(carros, _caminho);
        }

        public async Task AtualizarAsync(CarrosModel carro)
        {
            var carros = await BuscarTodosCarrosAsync();
            var carroExistente = carros.FirstOrDefault(c => c.Id == carro.Id);
            if (carroExistente != null)
            {
                carroExistente.Cor = carro.Cor;
                carroExistente.Marca = carro.Marca;
                carroExistente.Modelo = carro.Modelo;
                carroExistente.Ano = carro.Ano;
                await _csvRepository.EscreverCsvAsync(carros, _caminho);
            }
        }

        public async Task DeletarAsync(int id)
        {
            var carros = await BuscarTodosCarrosAsync();
            var carro = carros.FirstOrDefault(c => c.Id == id);
            if (carro != null)
            {
                carros.Remove(carro);
               await _csvRepository.EscreverCsvAsync(carros, _caminho);
            }
        }
    }
}

