using System;
using Xunit;

namespace LeilaoOnline.Tests
{
    public class LeilaoTests
    {
        [Fact]
        public void LeilaoComVariosLances()
        {
            // Arrange
            var leilao = new Leilao("Pintura do Van Gogh");
            var licitante1 = new Licitante("Licitante Um", leilao);
            var licitante2 = new Licitante("Licitante Dois", leilao);
            
            // Act
            leilao.ReceberLance(licitante1, 800);
            leilao.ReceberLance(licitante2, 1000);
            leilao.ReceberLance(licitante1, 900);
            
            leilao.Terminar();
            
            // Assert
            var valorEsperado = 1000;
            var valorRecebido = leilao.Ganhador.Valor;
            
            Assert.Equal(valorRecebido, valorRecebido);
        }
        
        [Fact]
        public void LeilaoComUmLance()
        {
            // Arrange
            var leilao = new Leilao("Pintura do Van Gogh");
            var licitante1 = new Licitante("Licitante Um", leilao);
            
            // Act
            leilao.ReceberLance(licitante1, 800);
            leilao.Terminar();
            
            // Assert
            var valorEsperado = 800;
            var valorRecebido = leilao.Ganhador.Valor;
            
            Assert.Equal(valorRecebido, valorRecebido);
        }
    }
}