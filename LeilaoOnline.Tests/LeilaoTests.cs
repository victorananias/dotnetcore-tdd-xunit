using System;
using Xunit;

namespace LeilaoOnline.Tests
{
    public class LeilaoTests
    {
        [Theory]
        [InlineData(1200, new double[] {800, 900, 1000, 1200})]
        [InlineData(1000, new double[] {900, 800, 100, 1000})]
        [InlineData(800, new double[] {800})]
        public void TerminaPregao_RetornaMaiorValor_QuandoLeilaoComPeloMenosUmLance(double valorEsperado, double[] lances)
        {
            // Arrange
            var leilao = new Leilao("Pintura do Van Gogh");
            var licitante = new Licitante("Licitante Um", leilao);

            // Act
            foreach (var lance in lances)
            {
                leilao.ReceberLance(licitante, lance);
            }

            leilao.Terminar();

            // Assert
            var valorRecebido = leilao.Ganhador.Valor;
            Assert.Equal(valorEsperado, valorRecebido);
        }

        [Fact]
        public void TerminaPregao_RetornaZero_QuandoLeilaoSemLances()
        {
            // Arrange
            var leilao = new Leilao("Pintura do Van Gogh");

            // Act
            leilao.Terminar();

            // Assert
            var valorEsperado = 0;
            var valorRecebido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado, valorRecebido);
        }
    }
}