using System.Text;
using Microsoft.Extensions.Options;
using qr_factorizacion_api_cs.Models;

namespace qr_factorizacion_api_cs.Services
{
    /// <summary>
    /// Servicio responsable de la comunicación con APIs externas relacionadas con matrices.
    /// Permite enviar resultados de factorización QR a un endpoint configurable.
    /// </summary>
    public class ExternalApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ExternalApiOptions _options;

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="ExternalApiService"/>.
        /// </summary>
        /// <param name="httpClient">Cliente HTTP inyectado para realizar peticiones.</param>
        /// <param name="options">Opciones de configuración para la API externa.</param>
        public ExternalApiService(HttpClient httpClient, IOptions<ExternalApiOptions> options)
        {
            _httpClient = httpClient;
            _options = options.Value;
        }

        /// <summary>
        /// Envía un resultado de matriz en formato JSON al endpoint externo configurado.
        /// </summary>
        /// <param name="json">Cadena JSON con los datos de la matriz a enviar.</param>
        /// <returns>
        /// Una tupla indicando si la petición fue exitosa, el código de estado HTTP y el cuerpo de la respuesta.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Se lanza si la configuración de la URL base o la ruta es inválida.
        /// </exception>
        public async Task<(bool success, int statusCode, string body)> SendMatrixResultAsync(string json, string jwtToken)
        {
            if (string.IsNullOrWhiteSpace(_options.BaseUrl) || string.IsNullOrWhiteSpace(_options.MatrixCalculatePath))
                throw new InvalidOperationException("La configuración de la URL del API externo es inválida.");

            var url = $"{_options.BaseUrl.TrimEnd('/')}{_options.MatrixCalculatePath}";
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Agrega el token JWT en el header Authorization
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwtToken);

            var response = await _httpClient.PostAsync(url, content);
            var responseBody = await response.Content.ReadAsStringAsync();

            return (response.IsSuccessStatusCode, (int)response.StatusCode, responseBody);
        }
    }
}