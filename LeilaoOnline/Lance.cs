namespace LeilaoOnline
{
    public class Lance
    {
        public Licitante Cliente { get; }
        public double Valor { get; }

        public Lance(Licitante cliente, double valor)
        {
            Cliente = cliente;
            Valor = valor;
        }
    }
}