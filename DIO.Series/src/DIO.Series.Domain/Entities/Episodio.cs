using System;

namespace DIO.Series.Domain.Entities
{
    public class Episodio : Entidade
    {
        public int NumeroEpisodio { get; set; }
        public int Temporada { get; set; }
        public string NomeEpisodio { get; set; }
        public string Descricao { get; set; }
        public int MinutosEpisodio { get; set; }
        public Guid SerieId { get; set; }
        public Serie Serie { get; set; }
    }
}
