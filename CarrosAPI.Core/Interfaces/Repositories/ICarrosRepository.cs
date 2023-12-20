using CarrosAPI.Core.Models;

namespace CarrosAPI.Core.Interfaces.Repositories

{
    public interface ICarrosRepository
    {
        Task<List<CarrosModel>> BuscarTodosCarrosAsync();
        Task<CarrosModel> BuscarPorIdAsync(int id);
        Task AdicionarAsync(CarrosModel carro);
        Task AtualizarAsync(CarrosModel carro);
        Task DeletarAsync(int id);



    }
}
