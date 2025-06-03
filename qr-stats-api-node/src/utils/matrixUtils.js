/**
 * Calcula estadísticas básicas de una matriz numérica bidimensional.
 * @param {number[][]} matrix - Matriz de números.
 * @returns {Object} Objeto con suma total, máximo, mínimo y promedio.
 */
const calculateStatistics = (matrix) => {
    // Aplana la matriz para facilitar los cálculos.
    const flatMatrix = matrix.flat();
    const totalSum = flatMatrix.reduce((acc, val) => acc + val, 0);
    const max = Math.max(...flatMatrix);
    const min = Math.min(...flatMatrix);
    const average = totalSum / flatMatrix.length;
    return { totalSum, max, min, average };
};

/**
 * Verifica si una matriz es cuadrada (mismo número de filas y columnas).
 * @param {any[][]} matrix - Matriz a verificar.
 * @returns {boolean} True si es cuadrada, false en caso contrario.
 */
const isSquare = (matrix) =>
    Array.isArray(matrix) &&
    matrix.length > 0 &&
    matrix.every(row => Array.isArray(row) && row.length === matrix.length);

/**
 * Verifica si una matriz cuadrada es diagonal.
 * Una matriz diagonal tiene todos los elementos fuera de la diagonal principal en cero.
 * @param {number[][]} matrix - Matriz a verificar.
 * @returns {boolean} True si es diagonal, false en caso contrario.
 */
const isDiagonal = (matrix) => {
    if (!isSquare(matrix)) return false;
    for (let i = 0; i < matrix.length; i++) {
        for (let j = 0; j < matrix[i].length; j++) {
            if (i !== j && matrix[i][j] !== 0) return false;
        }
    }
    return true;
};

// Exporta las funciones utilitarias para su uso en otros módulos.
module.exports = { calculateStatistics, isDiagonal, isSquare };