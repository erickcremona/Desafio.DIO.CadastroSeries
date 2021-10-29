using DIO.Series.Data.Context;
using DIO.Series.Domain.Contracts.RepositoryInterfaces;
using DIO.Series.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DIO.Series.Data.Repository
{
    public class RepositorioEpisodio : RepositorioBase<Episodio>, IRepositorioEpisodio
    {
        public RepositorioEpisodio(ContextSeries contextSeries) : base(contextSeries){ }

        public async Task<IEnumerable<Episodio>> ObterEpisodioExcluidos()
        {
            return await _dbSet.AsNoTracking()
                .Where(p => p.Excluido == true).ToListAsync();
        }

        public async Task<IEnumerable<Episodio>> ObterEpisodioExcluidosPorSerieId(Guid serieId)
        {
            return await _dbSet.AsNoTracking()
               .Where(p => p.Excluido == true && p.SerieId == serieId).ToListAsync();
        }

        public async Task<IEnumerable<Episodio>> ObterEpisodioPorSerieId(Guid serieId)
        {
            return await _dbSet.AsNoTracking()
                .Where(p => p.Excluido == false && p.SerieId == serieId).ToListAsync();
        }


    }
}
