/**
 * Rutas para operaciones relacionadas con matrices.
 * Se utiliza un controlador dedicado para manejar la lógica de negocio.
 * 
 * Decisiones técnicas:
 * - Se instancia el controlador fuera de la ruta para mantener una única instancia y facilitar el testeo.
 * - Se utiliza `bind` para asegurar el contexto correcto de `this` en el método del controlador.
 * - Los nombres de los objetos y rutas siguen una convención clara y coherente.
 */

const express = require('express');
const router = express.Router();
const MatrixController = require('../controllers/matrixController');
const authenticateToken = require('../middleware/jwtValidator');

// Instancia única del controlador de matrices
const matrixController = new MatrixController();

// Aplica el middleware JWT a todas las rutas de este router
router.use(authenticateToken);

/**
 * Ruta POST /calculate
 * Recibe matrices en el cuerpo de la solicitud y retorna estadísticas y propiedades.
 */
router.post('/calculate', matrixController.calculateStatistics.bind(matrixController));

module.exports = router;