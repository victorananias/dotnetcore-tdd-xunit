using System.Collections.Generic;
using System.Linq;

namespace LeilaoOnline
{
    public enum EstadoLeilao
    {
        EmAndamento,
        Finalizado
    }
    
    public class Leilao
    {
        private IList<Lance> _lances;
        public IEnumerable<Lance> Lances => _lances;
        public string Peca { get; }
        public Lance Ganhador { get; set; }
        public EstadoLeilao Estado { get; private set; }

        public Leilao(string peca)
        {
            Peca = peca;
            _lances = new List<Lance>();
            Estado = EstadoLeilao.EmAndamento;
        }

        public void ReceberLance(Licitante cliente, double valor)
        {
            if (Estado == EstadoLeilao.Finalizado)
            {
                return;
            }
            
            _lances.Add(new Lance(cliente, valor));
        }

        public void Iniciar()
        {

        }

        public void Terminar()
        {
            Estado = EstadoLeilao.Finalizado;
            Ganhador = _lances.DefaultIfEmpty(new Lance(null, 0)).OrderBy(lance => lance.Valor).LastOrDefault();
        }
    }
}