using System;

namespace LeilaoOnline
{
    public class Lance
    {
        public Licitante Cliente { get; }
        public double Valor { get; }

        public Lance(Licitante cliente, double valor)
        {
            if (valor < 0)
            {
                throw new ArgumentException("Valor do lance deve ser igual ou maior a zero.");
            }
            Cliente = cliente;
            Valor = valor;
        }
    }
}