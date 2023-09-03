using CarrosAPI.Models;

namespace CarrosAPI.Interfaces.Services
{
    public interface ICarrosService
    {

        //Validações
        Task<List<CarrosModel>> BuscarTodosCarros();
        Task<CarrosModel> BuscarPorId(int id);
        Task<CarrosModel> Adicionar(CarrosModel carro);
        Task<CarrosModel> Atualizar(CarrosModel carro, int id);
        Task<bool> Deletar(int id);

    }
}
