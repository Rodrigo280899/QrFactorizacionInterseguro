using Microsoft.AspNetCore.Mvc;
using qr_factorizacion_api_cs.Services;
using qr_factorizacion_api_cs.Models;
using System.Text.Json;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace qr_factorizacion_api_cs.Controllers
{
    /// <summary>
    /// Controlador para operaciones de factorización QR de matrices.
    /// Orquesta la validación, cálculo y envío de resultados a un API externo.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class QRFactorizationController : ControllerBase
    {
        private readonly QrFactorizationService _qrFactorizationService;
        private readonly ExternalApiService _externalApiService;

        /// <summary>
        /// Constructor con inyección de dependencias.
        /// </summary>
        /// <param name="qrFactorizationService">Servicio de factorización QR.</param>
        /// <param name="externalApiService">Servicio para comunicación con API externo.</param>
        public QRFactorizationController(
            QrFactorizationService qrFactorizationService,
            ExternalApiService externalApiService)
        {
            _qrFactorizationService = qrFactorizationService;
            _externalApiService = externalApiService;
        }

        /// <summary>
        /// Recibe una matriz, calcula su factorización QR y reenvía el resultado a un API externo.
        /// </summary>
        /// <param name="request">Objeto con la matriz a factorizar.</param>
        /// <returns>
        /// Respuesta del API externo si la operación es exitosa, o un mensaje de error en caso contrario.
        /// </returns>
        [HttpPost("matrix")]
        public async Task<IActionResult> PostMatrix([FromBody] MatrixRequest request)
        {
            try
            {
                // Validación básica de la petición
                if (request == null || request.Matrix == null)
                {
                    return BadRequest("Por favor completa todas las celdas con números válidos.");
                }

                // Cálculo de la factorización QR
                var (Q, R) = _qrFactorizationService.CalculateQrFactorization(request.Matrix);

                // Serialización del resultado
                var result = new { Q, R };
                var resultJson = JsonSerializer.Serialize(result);

                // Envío del resultado al API externo
                var jwtToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");
                var externalResponse = await _externalApiService.SendMatrixResultAsync(resultJson, jwtToken);

                // Manejo de la respuesta del API externo
                if (!externalResponse.success)
                {
                    return StatusCode(
                        externalResponse.statusCode,
                        $"Error al enviar los resultados al API externo: {externalResponse.body}"
                    );
                }

                // Retorna la respuesta del API externo como resultado
                return Content(externalResponse.body, "application/json");
            }
            catch (Exception ex)
            {
                // Manejo de errores inesperados
                return BadRequest(ex.Message);
            }
        }
    }
}