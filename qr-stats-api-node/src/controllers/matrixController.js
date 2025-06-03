// Importa el validador y el servicio de matrices.
// Nota: Se recomienda mantener la coherencia en el uso de mayúsculas/minúsculas en los nombres de carpetas.
const MatrixValidator = require('../validators/matrixValidator');
const MatrixService = require('../services/matrixService');

/**
 * Controlador para operaciones relacionadas con matrices.
 * Se encarga de validar la entrada y delegar la lógica de negocio al servicio correspondiente.
 */
class MatrixController {
    /**
     * Calcula estadísticas y propiedades de las matrices Q y R recibidas en el cuerpo de la solicitud.
     * @param {import('express').Request} req - Objeto de solicitud HTTP.
     * @param {import('express').Response} res - Objeto de respuesta HTTP.
     * @returns {void}
     */
    calculateStatistics(req, res) {
        // Validación de la estructura y contenido del body.
        const validationError = MatrixValidator.validate(req.body);
        if (validationError) {
            // Si hay error de validación, responde con estado 400 y el mensaje correspondiente.
            return res.status(400).json({ error: validationError });
        }

        // Extrae las matrices Q y R del cuerpo de la solicitud.
        const { Q, R } = req.body;

        // Calcula la información de ambas matrices utilizando el servicio.
        const result = {
            Q: MatrixService.getMatrixInfo(Q),
            R: MatrixService.getMatrixInfo(R)
        };

        // Devuelve la respuesta en formato JSON.
        return res.json(result);
    }
}

module.exports = MatrixController;