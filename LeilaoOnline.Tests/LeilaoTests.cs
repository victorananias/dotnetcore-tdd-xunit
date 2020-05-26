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
        public void TerminaPregao_RetornaMaiorValor_QuandoLeilaoComPeloMenosUmLance(double valorEsperado,
            double[] lances)
        {
            // Arrange
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Pintura do Van Gogh", modalidade);
            var licitante1 = new Licitante("Licitante Um", leilao);
            var licitante2 = new Licitante("Licitante Dois", leilao);

            leilao.Iniciar();
            for (var i = 0; i < lances.Length; i++)
            {
                if (i % 2 == 0)
                {
                    leilao.ReceberLance(licitante1, lances[i]);
                    continue;
                }

                leilao.ReceberLance(licitante2, lances[i]);
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
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Pintura do Van Gogh", modalidade);
            leilao.Iniciar();

            // Act
            leilao.Terminar();

            // Assert
            var valorEsperado = 0;
            var valorRecebido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado, valorRecebido);
        }

        [Fact]
        public void TerminaPregao_LancaInvalidOperationException_QuandoLeilaoNaoIniciado()
        {
            // Arrange
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Pintura do Van Gogh", modalidade);

            // Assert
            var exception = Assert.Throws<InvalidOperationException>(() => { leilao.Terminar(); });

            const string valorEsperado = "Não é possível terminar o leilão sem antes iniciá-lo.";
            Assert.Equal(valorEsperado, exception.Message);
        }

        [Theory]
        [InlineData(2, new double[] {800, 900})]
        [InlineData(4, new double[] {900, 800, 100, 1000})]
        public void ReceberLance_NaoPermiteNovosLances_QuandoLeilaoFinalizado(double qtdEsperada, double[] lances)
        {
            // Arrange
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Pintura do Van Gogh", modalidade);
            var licitante1 = new Licitante("Licitante Um", leilao);
            var licitante2 = new Licitante("Licitante Dois", leilao);

            leilao.Iniciar();
            for (var i = 0; i < lances.Length; i++)
            {
                if (i % 2 == 0)
                {
                    leilao.ReceberLance(licitante1, lances[i]);
                    continue;
                }

                leilao.ReceberLance(licitante2, lances[i]);
            }

            leilao.Terminar();

            // Act

            if (lances.Length % 2 == 0)
            {
                leilao.ReceberLance(licitante1, 5000);
            }
            else
            {
                leilao.ReceberLance(licitante2, 5000);
            }

            // Assert
            var qtdRecebida = leilao.Lances.Count();
            Assert.Equal(qtdEsperada, qtdRecebida);
        }

        [Fact]
        public void ReceberLance_NaoPermiteNovoLance_QuandoOLicitanteTentaDarLancesConsecutivos()
        {
            // Arrange
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Pintura do Van Gogh", modalidade);
            var licitante = new Licitante("Licitante Um", leilao);

            leilao.Iniciar();
            leilao.ReceberLance(licitante, 100);

            // Act
            leilao.ReceberLance(licitante, 5000);

            // Assert
            const int qtdEsperada = 1;
            var qtdRecebida = leilao.Lances.Count();
            Assert.Equal(qtdEsperada, qtdRecebida);
        }

        [Fact]
        public void Constructor_LancaArgumentEexception_QuandoLanceForNegativo()
        {
            // Arrange
            var valorNegativo = -100;

            // Assert
            Assert.Throws<ArgumentException>(() => new Lance(null, -100));
        }

        [Fact]
        public void Terminar_Retorna_ValorSuperiorMaisProximo_DadoLeilaoNessaModalidade()
        {
            // Arrange
            var modalidade = new OfertaSuperiorMaisProxima(150);
            var leilao = new Leilao("Pintura do Van Gogh", modalidade);
            var licitante = new Licitante("Licitante Um", leilao);
            var licitante2 = new Licitante("Licitante Dois", leilao);

            leilao.Iniciar();

            // Act
            leilao.ReceberLance(licitante, 100);
            leilao.ReceberLance(licitante2, 200);
            leilao.ReceberLance(licitante, 5000);
            leilao.Terminar();

            // Assert
            const double valorEsperado = 200;
            var valorRecebido = leilao.Ganhador.Valor;
            Assert.Equal(valorEsperado, valorRecebido);
        }
    }
}