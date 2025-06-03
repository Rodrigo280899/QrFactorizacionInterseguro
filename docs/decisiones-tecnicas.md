# Decisiones Técnicas

## 1. Lenguajes y Frameworks

- **C# (.NET 9 - ASP.NET Core):** Escogido por su robustez, facilidad para trabajar con APIs REST y soporte matemático a través de librerías como MathNet.
- **Node.js + Express:** Sencillo, flexible y adecuado para procesamiento rápido de datos.
- **Docker:** Asegura portabilidad entre entornos de desarrollo, testing y producción.

## 2. Factorización QR

Utilicé la librería **MathNet.Numerics** en C# para realizar la descomposición QR. Permite manipular matrices y realizar operaciones algebraicas de forma eficiente.

## 3. Comunicación entre APIs

- En la API de C#, usé `HttpClient` para enviar el resultado de la rotación y factorización a la API de Node.
- La URL de la API Node se configura vía variables de entorno para facilitar despliegue en Docker.

## 4. Validación de Datos

- Se valida que:
  - La matriz no esté vacía
  - Todos los sub-arrays tengan la misma longitud
  - Todos los elementos sean numéricos
- En C#, usé `DataAnnotations` y validación manual para estructuras complejas.

## 5. Manejo de Errores

> Ambos servicios manejan errores y devuelven respuestas con códigos HTTP significativos (`400`, `401`, `500`).

## 6. Pruebas (si las hiciste)

- **C# API**: Usé `xUnit` para pruebas unitarias y de integración.
- **Node.js API**: Usé `Jest` para validar cálculo estadístico.

## 7. Seguridad con JWT

Decidí proteger los endpoints mediante autenticación JWT por las siguientes razones:

- JWT es un estándar ampliamente usado para APIs REST.
- No requiere almacenamiento de sesión en el servidor.
- Fácil de integrar con middleware en ASP.NET Core y Express.

### Implementación técnica:

- **C# API:**
  - Uso del paquete `Microsoft.AspNetCore.Authentication.JwtBearer`.
  - Clave secreta en `appsettings.json` o como variable de entorno.
  - Middleware de autenticación y autorización configurado en `Startup.cs` o `Program.cs`.

- **Node.js API:**
  - Uso de `jsonwebtoken` y middleware de verificación manual.
  - Validación de expiración y firma del token.
  - En caso de token inválido, la API responde con `401 Unauthorized`.

Ambas APIs devuelven errores claros (`401`, `403`) cuando el token es inválido o falta.