using qr_factorizacion_api_cs.Services;
using Xunit;
using System;

namespace qr_factorizacion_api_cs.Tests.QRFactorizationService
{
    /// <summary>
    /// Pruebas unitarias para el servicio de factorización QR.
    /// Se valida tanto el comportamiento esperado ante entradas inválidas como la correcta ejecución con matrices válidas.
    /// </summary>
    public class QrFactorizationServiceTests
    {
        /// <summary>
        /// Verifica que se lance una excepción cuando la matriz de entrada está vacía.
        /// </summary>
        [Fact]
        public void ValidateMatrix_ThrowsArgumentException_WhenMatrixIsEmpty()
        {
            // Arrange
            var qrService = new QrFactorizationService();
            var emptyMatrix = new double[0][];

            // Act & Assert
            Assert.Throws<ArgumentException>(() => qrService.ValidateMatrix(emptyMatrix));
        }

        /// <summary>
        /// Verifica que la factorización QR retorna resultados válidos para una matriz identidad 2x2.
        /// </summary>
        [Fact]
        public void CalculateQrFactorization_ReturnsValidResult_ForIdentityMatrix()
        {
            // Arrange
            var qrService = new QrFactorizationService();
            var identityMatrix = new double[][]
            {
                new double[] { 1, 0 },
                new double[] { 0, 1 }
            };

            // Act
            var (Q, R) = qrService.CalculateQrFactorization(identityMatrix);

            // Assert
            Assert.NotNull(Q);
            Assert.NotNull(R);
            Assert.Equal(2, Q.Length);
            Assert.Equal(2, R.Length);
        }

    }
}