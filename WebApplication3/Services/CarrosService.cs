using CarrosAPI.Interfaces.Repositories;
using CarrosAPI.Interfaces.Services;
using CarrosAPI.Models;


namespace CarrosAPI.Services
{
    public class CarrosService : ICarrosService
    {
        private readonly ICarrosRepository _carrosRepository;

        public CarrosService(ICarrosRepository carrosRepository)
        {
            _carrosRepository = carrosRepository;
        }

        public async Task<CarrosModel> Adicionar(CarrosModel carro)
        {
            var carroExistente = await _carrosRepository.BuscarPorId(carro.Id);
            if (carroExistente != null)
            {
                throw new Exception($"Já existe um carro com o ID {carro.Id}");
            }

            await _carrosRepository.Adicionar(carro);
            return carro;
        }

        public async Task<CarrosModel> Atualizar(CarrosModel carro, int id)
        {
            if (id != carro.Id)
            {
                throw new Exception($"ID do objeto e ID da URL não coincidem");
            }

            var carroExistente = await _carrosRepository.BuscarPorId(id);
            if (carroExistente == null)
            {
                throw new Exception($"Carro com ID {id} não encontrado");
            }

            await _carrosRepository.Atualizar(carro);
            return carro;
        }

        public async Task<CarrosModel> BuscarPorId(int id)
        {
            var carro = await _carrosRepository.BuscarPorId(id);
            if (carro == null)
            {
                throw new Exception($"Carro com ID {id} não encontrado");
            }
            return carro;
        }

        public async Task<List<CarrosModel>> BuscarTodosCarros()
        {
            var carros = await _carrosRepository.BuscarTodosCarros();
            if (carros == null || !carros.Any())
            {
                throw new Exception("Não existem carros cadastrados");
            }
            return carros;
        }

        public async Task<bool> Deletar(int id)
        {
            var carroExistente = await _carrosRepository.BuscarPorId(id);
            if (carroExistente == null)
            {
                throw new Exception($"Carro com ID {id} não encontrado");
            }

            await _carrosRepository.Deletar(id);
            return true;
        }
    }
}
