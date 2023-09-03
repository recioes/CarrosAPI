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

            var carroAdicionado = await _carrosRepository.Adicionar(carro);
            return carroAdicionado;

        }

        public async Task<CarrosModel> Atualizar(CarrosModel carro, int id)
        {
            CarrosModel carrosPorId = await _carrosRepository.BuscarPorId(id);
            if (carrosPorId == null)
            {
                throw new Exception($"Carro do ID {id} não encontrado, tente novamente");
            }
            var carroAtualizado = await _carrosRepository.Atualizar(carro, id);
            return carroAtualizado;

        }

        public async Task<CarrosModel> BuscarPorId(int id)
        {
            var carroExistente = await _carrosRepository.BuscarPorId(id);
            if (carroExistente == null)
            {
                throw new Exception($"Não existe um carro com o ID {id}");
            }

            return carroExistente;
        }

        public async Task<List<CarrosModel>> BuscarTodosCarros()
        {
            var todosCarros = await _carrosRepository.BuscarTodosCarros();
            if (todosCarros == null)
            {
                throw new Exception($"Não existem carros cadastrados");
            }

            return todosCarros;
        }

        public async Task<bool> Deletar(int id)
        {
            CarrosModel carroPorId = await _carrosRepository.BuscarPorId(id);
            if (carroPorId == null)
            {

                throw new Exception($"Carro do ID {id} não encontrado, tente novamente");
            }
            var carroDeletado = await _carrosRepository.Deletar(id);
            return carroDeletado;
        }
    }
}
