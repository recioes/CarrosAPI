using CarrosAPI.Models;

namespace CarrosAPI.Interfaces.Repositories

{
    public interface ICarrosRepository
    {
        Task<List<CarrosModel>> BuscarTodosCarros();
        Task<CarrosModel> BuscarPorId(int id);
        Task Adicionar(CarrosModel carro);
        Task Atualizar(CarrosModel carro);
        Task Deletar(int id);



    }
}
