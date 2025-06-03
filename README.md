# Coding Challenge - Division TI

Este proyecto resuelve el desafio tecnico solicitado, implementando una solucion distribuida con dos APIs comunicadas entre si, cada una en un lenguaje distinto, utilizando buenas practicas de desarrollo, contenerizacion y seguridad.

---

## Tecnologias utilizadas

-  API 1: C# (.NET 9 - ASP.NET Core Web API)
-  API 2: Node.js (Express)
-  Comunicacion entre APIs via HTTP
-  Contenerizacion con Docker
-  Orquestacion con Docker Compose
-  Autenticacion con JWT
-  Factorizacion QR y rotacion de matrices
-  Calculo de estadisticas avanzadas

---

## Como ejecutar el proyecto

### 1. Clonar repositorio

codigo fuente en github
gtihub: https://github.com/Rodrigo280899/QrFactorizacionInterseguro

en la carpeta raiz donde esta el archivo docker-compose ejecutar lo siguiente:
1° Para descargar las imagenes publicadas en docker hub:
- docker-compose pull
2° Para activar el docker con las imagenes publicadas:
- docker-compose up -d

El link de la vista es el siguiente (con el docker levantado):
- http://localhost:8080/


## EndPoints

### 1. Autenticacion
POST /auth/token

{
  "username": "admin",
  "password": "admin"
}

### 2. Rotacion y factorizacion
POST /api/matrix/rotate

Authorization: Bearer <jwt_token>
Content-Type: application/json

{
  "matrix": [
    [1, 2],
    [3, 4],
    [5, 6]
  ]
}

### 3. Estadisticas
POST /stats

Authorization: Bearer <jwt_token>
Content-Type: application/json
{
    "Q": [
        [-1.0000000000000004,0,0],
        [0,-1.0000000000000004,0],
        [0,0,-1.0000000000000004]
    ],
    "R": [
        [-5,0,0],
        [0,3.5,0],
        [0,0,-2]
    ]
}

## Seguridad con JWT
Ambas APIs estan protegidas por autenticacion JWT.

Agrega el token JWT en el encabezado Authorization: Bearer <token> en cada solicitud.

El token se valida en ambas APIs. Si es invalido o esta ausente, se retorna 401 Unauthorized.

## Seguridad con JWT
Pruebas unitarias en:

xUnit para C#

Jest para Node.js
