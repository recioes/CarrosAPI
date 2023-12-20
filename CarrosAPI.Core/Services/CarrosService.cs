using CarrosAPI.Core.Interfaces.Repositories;
using CarrosAPI.Core.Interfaces.Services;
using CarrosAPI.Core.Models;

namespace CarrosAPI.Core.Services
{
    public class CarrosService : ICarrosService
    {
        private readonly ICarrosRepository _carrosRepository;

        public CarrosService(ICarrosRepository carrosRepository)
        {
            _carrosRepository = carrosRepository;
        }

        public async Task<CarrosModel> AdicionarAsync(CarrosModel carro)
        {
            var carroExistente = await _carrosRepository.BuscarPorIdAsync(carro.Id);
            if (carroExistente != null)
            {
                throw new Exception($"Já existe um carro com o ID {carro.Id}");
            }

            await _carrosRepository.AdicionarAsync(carro);
            return carro;
        }

        public async Task<CarrosModel> AtualizarAsync(CarrosModel carro, int id)
        {
            if (id != carro.Id)
            {
                throw new Exception($"ID do objeto e ID da URL não coincidem");
            }

            var carroExistente = await _carrosRepository.BuscarPorIdAsync(id);
            if (carroExistente == null)
            {
                throw new Exception($"Carro com ID {id} não encontrado");
            }

            await _carrosRepository.AtualizarAsync(carro);
            return carro;
        }

        public async Task<CarrosModel> BuscarPorIdAsync(int id)
        {
            var carro = await _carrosRepository.BuscarPorIdAsync(id);
            if (carro == null)
            {
                throw new Exception($"Carro com ID {id} não encontrado");
            }
            return carro;
        }

        public async Task<List<CarrosModel>> BuscarTodosCarrosAsync()
        {
            var carros = await _carrosRepository.BuscarTodosCarrosAsync();
            if (carros == null || !carros.Any())
            {
                throw new Exception("Não existem carros cadastrados");
            }
            return carros;
        }

        public async Task<bool> DeletarAsync(int id)
        {
            var carroExistente = await _carrosRepository.BuscarPorIdAsync(id);
            if (carroExistente == null)
            {
                throw new Exception($"Carro com ID {id} não encontrado");
            }

            await _carrosRepository.DeletarAsync(id);
            return true;
        }
    }
}
