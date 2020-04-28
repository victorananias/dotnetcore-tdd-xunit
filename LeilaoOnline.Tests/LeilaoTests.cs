using System;
using System.Linq;
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
            
            leilao.Iniciar();

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
        
        [Theory]
        [InlineData(2, new double[] {800, 900})]
        [InlineData(4, new double[] {900, 800, 100, 1000})]
        public void ReceberLance_NaoPermiteNovosLances_QuandoLeilaoFinalizado(double qtdEsperada, double[] lances)
        {
            // Arrange
            var leilao = new Leilao("Pintura do Van Gogh");
            var licitante = new Licitante("Licitante Um", leilao);
            
            leilao.Iniciar();
            
            foreach (var lance in lances)
            {
                leilao.ReceberLance(licitante, lance);
            }

            leilao.Terminar();

            // Act
            leilao.ReceberLance(licitante, 5000);

            // Assert
            var qtdRecebida = leilao.Lances.Count();
            Assert.Equal(qtdEsperada, qtdRecebida);
        }
    }
}