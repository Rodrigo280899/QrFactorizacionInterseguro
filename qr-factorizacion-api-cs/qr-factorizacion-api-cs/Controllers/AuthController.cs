using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace qr_factorizacion_api_cs.Controllers
{
    /// <summary>
    /// Controlador responsable de la autenticación y generación de tokens JWT.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Constructor que recibe la configuración de la aplicación.
        /// </summary>
        /// <param name="configuration">Configuración inyectada para acceder a claves y parámetros JWT.</param>
        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Autentica al usuario y genera un token JWT si las credenciales son válidas.
        /// </summary>
        /// <param name="request">Objeto con el nombre de usuario y contraseña.</param>
        /// <returns>
        /// Un token JWT si la autenticación es exitosa, o un error 401 si no lo es.
        /// </returns>
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // Validación básica de credenciales (ejemplo simple, no usar en producción)
            if (request.Username == "admin" && request.Password == "admin")
            {
                // Definición de los claims del usuario autenticado
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, request.Username)
                };

                // Obtención de la clave y parámetros de configuración desde appsettings.json
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                // Creación del token JWT
                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: null,
                    claims: claims,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: creds);

                // Retorna el token generado
                return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            }

            // Si las credenciales no son válidas, retorna 401
            return Unauthorized();
        }
    }

    /// <summary>
    /// Modelo para la petición de login.
    /// </summary>
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}