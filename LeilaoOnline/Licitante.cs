namespace LeilaoOnline
{
    public class Licitante
    {
        public string Nome { get; }
        public Leilao Leilao { get; }

        public Licitante(string nome, Leilao leilao)
        {
            Nome = nome;
            Leilao = leilao;
        }
    }
}