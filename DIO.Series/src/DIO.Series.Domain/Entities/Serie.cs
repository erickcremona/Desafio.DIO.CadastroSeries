using System.Collections.Generic;

namespace DIO.Series.Domain.Entities
{
    public class Serie : Entidade
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Temporadas { get; set; }
        public int Classificacao { get; set; }
        public Genero Genero { get; set; }
        public string Elenco { get; set; }
        public int Ano { get; set; }
        public IEnumerable<Episodio> Episodios { get; set; }
    }
}
