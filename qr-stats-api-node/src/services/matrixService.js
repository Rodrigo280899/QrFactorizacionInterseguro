// Importa funciones utilitarias para operaciones con matrices.
// Se mantiene la coherencia en el uso de minúsculas en los nombres de carpetas.
const { calculateStatistics, isDiagonal, isSquare } = require('../utils/matrixUtils');

/**
 * Servicio para operaciones de matrices.
 * Centraliza la lógica de obtención de información relevante sobre una matriz.
 * 
 * Decisiones técnicas:
 * - Se utiliza una clase estática para evitar instanciación innecesaria y facilitar el acceso desde el controlador.
 * - Se documenta el método principal para claridad y mantenibilidad.
 */
class MatrixService {
    /**
     * Obtiene información relevante de una matriz, incluyendo estadísticas y propiedades.
     * @param {number[][]} matrix - Matriz numérica bidimensional.
     * @returns {Object} Objeto con estadísticas y propiedades de la matriz.
     */
    static getMatrixInfo(matrix) {
        return {
            statistics: calculateStatistics(matrix),
            isSquare: isSquare(matrix),
            isDiagonal: isDiagonal(matrix)
        };
    }
}

module.exports = MatrixService;