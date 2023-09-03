using CarrosAPI.Interfaces.Repositories;
using CarrosAPI.Interfaces.Services;
using CarrosAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq;

namespace CarrosAPI.Repository
{
    public class CarrosRepository : ICarrosRepository

    {
        private readonly string filePath = "DataBase/Carros.csv";
        private readonly ICSVService _csvFileService;

        public CarrosRepository(ICSVService csvFileService)
        {
            this._csvFileService = csvFileService;
        }


        public async Task<CarrosModel> BuscarPorId(int id)
        {
            List<CarrosModel> carros = await _csvFileService.ReadCSVFileAsync(filePath);
            return carros.FirstOrDefault(x => x.Id == id);
        }

        public async Task<List<CarrosModel>> BuscarTodosCarros()
        {
            List<CarrosModel> carros = await _csvFileService.ReadCSVFileAsync(filePath);
            return carros;
        }
        public async Task<CarrosModel> Adicionar(CarrosModel carro)
        {
            await _csvFileService.WriteToCSVFileAsync(filePath, carro);

            return carro;
        }

        public async Task<CarrosModel> Atualizar(CarrosModel carro, int id)
        {
            List<CarrosModel> carros = await _csvFileService.ReadCSVFileAsync(filePath);
            CarrosModel carroPorId = await BuscarPorId(id);

            // Atualizar as propriedades
            carroPorId.Cor = carro.Cor;
            carroPorId.Marca = carro.Marca;
            carroPorId.Modelo = carro.Modelo;
            carroPorId.Ano = carro.Ano;

            await _csvFileService.WriteAllToCSVFileAsync(filePath, carros);

            return carroPorId;
        }

        public async Task<bool> Deletar(int id)
        {
            List<CarrosModel> carros = await _csvFileService.ReadCSVFileAsync(filePath);
            CarrosModel carroPorId = await BuscarPorId(id);
            carros.Remove(carroPorId);

            await _csvFileService.WriteAllToCSVFileAsync(filePath, carros);
           
            return true;

        }
    }
}

