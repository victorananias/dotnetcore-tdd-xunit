using System.Linq;

namespace LeilaoOnline
{
    public class MaiorValor : IModalidadeAvaliacao
    {
        public void Avaliar(Leilao leilao)
        {
            leilao.Ganhador = leilao.Lances.DefaultIfEmpty(new Lance(null, 0)).OrderBy(lance => lance.Valor)
                .LastOrDefault();
        }
    }
}