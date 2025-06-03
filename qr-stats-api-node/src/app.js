/**
 * Punto de entrada principal de la aplicación.
 * Configura y levanta el servidor Express para exponer la API de matrices.
 * 
 * Decisiones técnicas:
 * - Se utiliza express.json() para parsear automáticamente cuerpos JSON.
 * - Se centralizan las rutas de matrices bajo el prefijo /api/matrix para mantener una estructura REST clara.
 * - El puerto es configurable mediante variable de entorno para facilitar despliegues en diferentes entornos.
 */

const express = require('express');
const matrixRoutes = require('./routes/matrix');

// Carga de variables de entorno desde un archivo .env, si existe.
require('dotenv').config();


const app = express();

// Middleware para parsear cuerpos JSON en las solicitudes.
app.use(express.json());

// Rutas para operaciones de matrices, agrupadas bajo /api/matrix.
app.use('/api/matrix', matrixRoutes);

// Definición del puerto; por defecto 3000 si no se especifica en variables de entorno.
const PORT = process.env.PORT || 3000;

// Inicio del servidor y notificación en consola.
app.listen(PORT, () => {
    console.log(`Server is running on port ${PORT}`);
});