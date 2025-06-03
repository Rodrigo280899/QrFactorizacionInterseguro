/**
 * Clase responsable de validar la estructura y contenido de las matrices recibidas.
 * Centraliza todas las reglas de validación para mantener la coherencia y facilitar el mantenimiento.
 * 
 * Decisiones técnicas:
 * - Se utiliza una clase estática para evitar instanciación y permitir acceso directo a las validaciones.
 * - Se documenta cada regla para claridad y futuras extensiones.
 */
class MatrixValidator {
    // Límite máximo permitido para filas y columnas de las matrices.
    static LIMITE = 500;

    /**
     * Valida la estructura y contenido de las matrices Q y R en el cuerpo de la solicitud.
     * @param {Object} body - Cuerpo de la solicitud HTTP.
     * @returns {string|null} Mensaje de error si la validación falla, o null si es válida.
     */
    static validate(body) {
        // Validar presencia de claves Q y R.
        if (body.Q === undefined || body.R === undefined) {
            return 'Las claves Q y R deben estar presentes en el cuerpo de la solicitud.';
        }
        // Validar que Q y R sean arreglos.
        if (!Array.isArray(body.Q) || !Array.isArray(body.R)) {
            return 'Tanto Q como R deben ser arreglos (arrays).';
        }
        // Validar que Q y R sean arreglos bidimensionales.
        if (
            !body.Q.every(row => Array.isArray(row)) ||
            !body.R.every(row => Array.isArray(row))
        ) {
            return 'Tanto Q como R deben ser arreglos bidimensionales (matrices).';
        }
        // Validar límites de filas y columnas.
        if (
            body.Q.length > this.LIMITE || body.R.length > this.LIMITE ||
            body.Q.some(row => row.length > this.LIMITE) ||
            body.R.some(row => row.length > this.LIMITE)
        ) {
            return `Las matrices Q y R no deben tener más de ${this.LIMITE} filas ni columnas.`;
        }
        // Validar que todos los valores sean números válidos.
        if (
            !body.Q.every(row => row.every(val => typeof val === 'number' && !isNaN(val))) ||
            !body.R.every(row => row.every(val => typeof val === 'number' && !isNaN(val)))
        ) {
            return 'Todos los valores de las matrices Q y R deben ser números válidos.';
        }
        // Si todas las validaciones pasan, retornar null.
        return null;
    }
}

module.exports = MatrixValidator;