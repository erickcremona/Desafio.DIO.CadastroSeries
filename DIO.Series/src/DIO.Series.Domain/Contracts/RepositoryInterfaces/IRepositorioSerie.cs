using DIO.Series.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DIO.Series.Domain.Contracts.RepositoryInterfaces
{
    public interface IRepositorioSerie : IRepositorioBase<Serie>
    {
        Task<Serie> ObterSerieComEpisodios(Guid id);
        Task<IEnumerable<Serie>> ObterSeriesExcluidas();       
    }
}
