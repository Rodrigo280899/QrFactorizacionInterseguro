# Diseño de Arquitectura

## 1. Componentes de la Solución

- **Frontend HTML(html + css + javascript):** Pantalla de visualizacion para ingresar matrices y leer las respuestas esperadas.
- **API en C# (.NET 9 - ASP.NET Core):** Recibe una matriz rectangular, realiza rotación y factorización QR.
- **API en Node.js (Express):** Recibe la matriz rotada y devuelve estadísticas (mínimo, máximo, promedio, suma, si es diagonal).
- **Docker:** Utilizado para contenerizar ambas APIs y pantalla de usuario.
- **docker-compose:** Orquesta los servicios y la pantalla de usuario para facilitar el despliegue conjunto y la comunicación entre ellos.

## 2. Flujo de la Aplicación

1. Se ingresa la matriz indicada por el usuario.
1. El sistema se valida mediante JWT (`/api/Auth/login`).
2. El usuario envía una matriz en formato JSON a la API en C# (`/api/QRFactorization/matrix`)[Protegido].
3. La API en C# realiza la rotación de la matriz y la factorización QR.
4. Luego, envía el resultado como JSON vía HTTP a la API en Node.js (`/api/matrix/calculate`)[Protegido].
5. La API en Node.js calcula estadísticas sobre la matriz.
6. El resultado se devuelve al usuario.

## 3. Comunicación entre APIs

- Comunicación basada en HTTP (`HttpClient` en C#).
- Ambas APIs están en la misma red de Docker para facilitar llamadas internas.
- Se manejan excepciones y errores de red con bloques try-catch y respuestas estandarizadas a RESTful.

## 4. Seguridad con JWT

Ambas APIs implementan autenticación basada en JWT (JSON Web Token) para proteger los endpoints.

### Flujo de autenticación:

1. El cliente realiza login (o simula un login) y recibe un token JWT firmado.
2. Las solicitudes a los endpoints protegidos deben incluir el token en el encabezado: Authorization: Bearer <token>
3. Si el token es válido y no ha expirado, se permite el acceso al recurso.