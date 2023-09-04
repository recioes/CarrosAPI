using Microsoft.AspNetCore.Mvc;
using CarrosAPI.Models;
using CarrosAPI.Interfaces.Services;


namespace CarrosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarrosController : ControllerBase
    {
        private readonly ICarrosService _carrosService;  

        public CarrosController(ICarrosService carrosService)  
        {
            _carrosService = carrosService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CarrosModel>>> BuscarTodosCarros()
        {
            return Ok(await _carrosService.BuscarTodosCarros());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CarrosModel>> BuscarPorId(int id)
        {
            return Ok(await _carrosService.BuscarPorId(id));
        }

        [HttpPost]
        public async Task<ActionResult<CarrosModel>> Adicionar([FromBody] CarrosModel carroModel)
        {
            return Ok(await _carrosService.Adicionar(carroModel));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CarrosModel>> Atualizar([FromBody] CarrosModel carroModel, int id)
        {
            return Ok(await _carrosService.Atualizar(carroModel, id));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Deletar(int id)
        {
            return Ok(await _carrosService.Deletar(id));
        }
    }
}




