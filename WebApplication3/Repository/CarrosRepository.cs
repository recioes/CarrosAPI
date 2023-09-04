using CarrosAPI.Interfaces.Repositories;
using CarrosAPI.Interfaces.Services;
using CarrosAPI.Models;



namespace CarrosAPI.Repository
{

    public class CarroRepository : ICarrosRepository
    {
        private readonly ICSVService _csvService;
        private readonly string _caminho = "DataBase/Carros.csv";

        public CarroRepository(ICSVService csvService)
        {
            _csvService = csvService;
        }
        public async Task<List<CarrosModel>> BuscarTodosCarros()
        {
            return await _csvService.LerCsvAsync(_caminho);
        }

        public async Task<CarrosModel> BuscarPorId(int id)
        {
            var carros = await BuscarTodosCarros();
            return carros.FirstOrDefault(c => c.Id == id);
        }

        public async Task Adicionar(CarrosModel carro)
        {
            var carros = await BuscarTodosCarros();
            carros.Add(carro);
            _csvService.EscreverCsvAsync(carros, _caminho);
        }

        public async Task Atualizar(CarrosModel carro)
        {
            var carros = await BuscarTodosCarros();
            var carroExistente = carros.FirstOrDefault(c => c.Id == carro.Id);
            if (carroExistente != null)
            {
                carroExistente.Cor = carro.Cor;
                carroExistente.Marca = carro.Marca;
                carroExistente.Modelo = carro.Modelo;
                carroExistente.Ano = carro.Ano;
                _csvService.EscreverCsvAsync(carros, _caminho);
            }
        }

        public async Task Deletar(int id)
        {
            var carros = await BuscarTodosCarros();
            var carro = carros.FirstOrDefault(c => c.Id == id);
            if (carro != null)
            {
                carros.Remove(carro);
                _csvService.EscreverCsvAsync(carros, _caminho);
            }
        }
    }
}

