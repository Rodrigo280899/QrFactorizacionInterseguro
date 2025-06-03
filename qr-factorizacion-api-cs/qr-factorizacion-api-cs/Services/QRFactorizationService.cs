using System;
using MathNet.Numerics.LinearAlgebra;

namespace qr_factorizacion_api_cs.Services
{
    /// <summary>
    /// Servicio para realizar la factorizaci�n QR de matrices num�ricas.
    /// Utiliza el m�todo de Householder a trav�s de MathNet.Numerics.
    /// </summary>
    public class QrFactorizationService
    {
        /// <summary>
        /// Calcula la factorizaci�n QR de una matriz usando Householder.
        /// </summary>
        /// <param name="matrix">
        /// Matriz de entrada (arreglo de arreglos de double) con m filas y n columnas, donde m >= n.
        /// </param>
        /// <returns>
        /// Una tupla con las matrices Q y R en formato double[][].
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Se lanza si la matriz no cumple con los requisitos de validaci�n.
        /// </exception>
        public (double[][] Q, double[][] R) CalculateQrFactorization(double[][] matrix)
        {
            return HouseHolderQrFactorization(matrix);
        }

        /// <summary>
        /// Realiza la factorizaci�n QR usando el m�todo de Householder.
        /// </summary>
        /// <param name="matrix">Matriz de entrada validada.</param>
        /// <returns>Tupla con las matrices Q y R.</returns>
        private (double[][] Q, double[][] R) HouseHolderQrFactorization(double[][] matrix)
        {
            ValidateMatrix(matrix);
            var denseMatrix = Matrix<double>.Build.DenseOfRows(matrix);

            var qr = denseMatrix.QR();

            double[][] qArray = qr.Q.ToRowArrays();
            double[][] rArray = qr.R.ToRowArrays();

            return (qArray, rArray);
        }

        /// <summary>
        /// Valida que la matriz cumpla con los requisitos para la factorizaci�n QR.
        /// </summary>
        /// <param name="matrix">Matriz a validar.</param>
        /// <exception cref="ArgumentException">
        /// Se lanza si la matriz es vac�a, tiene filas de distinta longitud, dimensiones mayores a 500,
        /// o si el n�mero de filas es menor que el de columnas.
        /// </exception>
        public void ValidateMatrix(double[][] matrix)
        {
            int rowCount = matrix.Length;
            if (rowCount == 0)
                throw new ArgumentException("La matriz no puede estar vac�a.");

            if (rowCount > 500)
                throw new ArgumentException("La matriz no puede tener m�s de 500 filas.");

            int colCount = matrix[0].Length;
            if (colCount > 500)
                throw new ArgumentException("La matriz no puede tener m�s de 500 columnas.");

            if (rowCount < colCount)
                throw new ArgumentException("La matriz debe tener al menos tantas filas como columnas (m >= n) para la factorizaci�n QR.");

            for (int i = 1; i < rowCount; i++)
            {
                if (matrix[i].Length != colCount)
                    throw new ArgumentException("Todas las filas de la matriz deben tener la misma longitud.");
            }
        }
    }
}