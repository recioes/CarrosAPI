using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CarrosAPI.Core.Interfaces.Services;
using CarrosAPI.Core.Models.Auth;


namespace CarrosAPI.Controllers
{
    [Route("API/Conta")]
    public class APIController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ITokenService _tokenService;

        public APIController(IAuthService authService, ITokenService tokenService)
        {
            _authService = authService;
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(IList<string>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<dynamic>> AuthenticateAsync([FromBody] UserInfo userInfo)
        {
            var estaAutenticado = await _authService.AuthenticateAsync(userInfo);
            if (!estaAutenticado)
            {
                return NotFound(new { message = "Usuário ou senha inválidos" });
            }

            var token = _tokenService.GerarToken(userInfo);
            userInfo.Password = "";
            return new
            {
                user = userInfo.Username,
                token = token
            };
        }
    }
}  






    

