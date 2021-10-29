using DIO.Series.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DIO.Series.Domain.Contracts.RepositoryInterfaces
{
    public interface IRepositorioEpisodio : IRepositorioBase<Episodio>
    {
        Task<IEnumerable<Episodio>> ObterEpisodioExcluidosPorSerieId(Guid serieId);
        Task<IEnumerable<Episodio>> ObterEpisodioPorSerieId(Guid SerieId);
    }
}
