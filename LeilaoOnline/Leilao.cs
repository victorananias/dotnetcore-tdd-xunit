using System;
using System.Collections.Generic;
using System.Linq;

namespace LeilaoOnline
{
    public enum EstadoLeilao
    {
        NaoIniciado,
        EmAndamento,
        Finalizado
    }

    public class Leilao
    {
        private IList<Lance> _lances;
        private Licitante _ultimoLicitante;
        private IModalidadeAvaliacao _modalidade;
        public IEnumerable<Lance> Lances => _lances;
        public string Peca { get; }
        public Lance Ganhador { get; set; }
        public EstadoLeilao Estado { get; private set; }

        public Leilao(string peca, IModalidadeAvaliacao modalidade)
        {
            Peca = peca;
            _lances = new List<Lance>();
            Estado = EstadoLeilao.NaoIniciado;
            _modalidade = modalidade;
        }

        public void ReceberLance(Licitante licitante, double valor)
        {
            if (!LanceAceito(licitante))
            {
                return;
            }

            _lances.Add(new Lance(licitante, valor));
            _ultimoLicitante = licitante;
        }

        private bool LanceAceito(Licitante licitante)
        {
            return Estado == EstadoLeilao.EmAndamento && _ultimoLicitante != licitante;
        }

        public void Iniciar()
        {
            Estado = EstadoLeilao.EmAndamento;
        }

        public void Terminar()
        {
            if (Estado != EstadoLeilao.EmAndamento)
            {
                throw new InvalidOperationException("Não é possível terminar o leilão sem antes iniciá-lo.");
            }

            Estado = EstadoLeilao.Finalizado;

            _modalidade.Avaliar(this);
        }
    }
}