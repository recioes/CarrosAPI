using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CarrosAPI.Core.Models;
using CarrosAPI.Core.Interfaces.Services;
using Swashbuckle.AspNetCore.Annotations;
using CarrosAPI.Core.Services;



namespace CarrosAPI.Controllers
{
    [Route("API/[controller]")]
    [ApiController]
    public class CarrosController : APIController
    {
        private readonly ICarrosService _carrosService;

        public CarrosController(ICarrosService carrosService, IAuthService authService, TokenService tokenService)
: base(authService, tokenService)  
        {
            _carrosService = carrosService;
        }

        [HttpGet]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Obtem todos os carros")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<CarrosModel>>> BuscarTodosCarrosAsync()
        {
            return Ok(await _carrosService.BuscarTodosCarrosAsync());
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        [SwaggerOperation(Summary = "Obtem carro pelo id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CarrosModel>> BuscarPorIdAsync(int id)
        {
            var carro = await _carrosService.BuscarPorIdAsync(id);
            if (carro == null)
            {
                return NotFound("Carro não encontrado");
            }
            return Ok(carro);
        }

        [HttpPost]
        [Authorize]
        [SwaggerOperation(Summary = "Adiciona um carro novo")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AdicionarAsync([FromBody] CarrosModel carroModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var novoCarro = await _carrosService.AdicionarAsync(carroModel);
            return CreatedAtAction(nameof(BuscarPorIdAsync), new { id = novoCarro.Id }, novoCarro);
        }

        [HttpPut("{id}")]
        [Authorize]
        [SwaggerOperation(Summary = "Atualiza um carro existente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AtualizarAsync(int id, [FromBody] CarrosModel carroModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var carroAtualizado = await _carrosService.AtualizarAsync(carroModel, id);
            if (carroAtualizado == null)
            {
                return NotFound("Carro não encontrado");
            }
            return Ok(carroAtualizado);
        }

        [HttpDelete("{id}")]
        [Authorize]
        [SwaggerOperation(Summary = "Deleta um carro existente")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletarAsync(int id)
        {
            var sucesso = await _carrosService.DeletarAsync(id);
            if (!sucesso)
            {
                return NotFound("Carro não encontrado");
            }
            return Ok("Carro deletado com sucesso");
        }
    }
}