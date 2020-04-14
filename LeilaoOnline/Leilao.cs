using System.Collections.Generic;
using System.Linq;

namespace LeilaoOnline
{
    public class Leilao
    {
        private IList<Lance> _lances;
        public IEnumerable<Lance> Lances => _lances;
        public string Peca { get; }
        public Lance Ganhador { get; set; }

        public Leilao(string peca)
        {
            Peca = peca;
            _lances = new List<Lance>();
        }

        public void ReceberLance(Licitante cliente, double valor)
        {
            _lances.Add(new Lance(cliente, valor));
        }

        public void Iniciar()
        {

        }

        public void Terminar()
        {
            Ganhador = _lances.DefaultIfEmpty(new Lance(null, 0)).OrderBy(lance => lance.Valor).LastOrDefault();
        }
    }
}