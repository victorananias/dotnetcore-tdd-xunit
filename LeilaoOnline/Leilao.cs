﻿using System;
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
        public IEnumerable<Lance> Lances => _lances;
        public string Peca { get; }
        public Lance Ganhador { get; set; }
        public EstadoLeilao Estado { get; private set; }
        public double ValorDestino { get; }

        public Leilao(string peca, double valorDestino = 0)
        {
            Peca = peca;
            _lances = new List<Lance>();
            Estado = EstadoLeilao.NaoIniciado;
            ValorDestino = valorDestino;
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

            if (ValorDestino <= 0)
            {
                Ganhador = _lances.DefaultIfEmpty(new Lance(null, 0)).OrderBy(lance => lance.Valor).LastOrDefault();
                return;
            }

            Ganhador = _lances.Where(lance => lance.Valor >= ValorDestino)
                .DefaultIfEmpty(new Lance(null, 0))
                .OrderBy(lance => lance.Valor)
                .FirstOrDefault();
        }
    }
}