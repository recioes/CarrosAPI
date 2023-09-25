using CarrosAPI.Core.Models;

namespace CarrosAPI.Core.Interfaces.Services
{
    public interface ICarrosService
    {

        //Validações
        Task<List<CarrosModel>> BuscarTodosCarrosAsync();
        Task<CarrosModel> BuscarPorIdAsync(int id);
        Task<CarrosModel> AdicionarAsync(CarrosModel carro);
        Task<CarrosModel> AtualizarAsync(CarrosModel carro, int id);
        Task<bool> DeletarAsync(int id);

    }
}
