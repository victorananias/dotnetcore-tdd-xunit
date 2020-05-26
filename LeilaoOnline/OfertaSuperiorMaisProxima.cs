using System.Linq;

namespace LeilaoOnline
{
    public class OfertaSuperiorMaisProxima : IModalidadeAvaliacao
    {
        public OfertaSuperiorMaisProxima(double valorDestino)
        {
            ValorDestino = valorDestino;
        }

        public double ValorDestino { get; set; }

        public void Avaliar(Leilao leilao)
        {
            leilao.Ganhador = leilao.Lances.Where(lance => lance.Valor >= ValorDestino)
                .DefaultIfEmpty(new Lance(null, 0))
                .OrderBy(lance => lance.Valor)
                .FirstOrDefault();
        }
    }
}