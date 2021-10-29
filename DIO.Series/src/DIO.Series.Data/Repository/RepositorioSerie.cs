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
    public class RepositorioSerie: RepositorioBase<Serie>, IRepositorioSerie
    {

        public RepositorioSerie(ContextSeries contextSeries): base(contextSeries){ }

        public async Task<Serie> ObterSerieComEpisodios(Guid id)
        {
            return await _dbSet.AsNoTracking().Include(p => p.Episodios)
                .FirstOrDefaultAsync(p => p.Excluido == false && p.Id == id);
        }

        public async Task<IEnumerable<Serie>> ObterSeriesExcluidas()
        {
            return await _dbSet.AsNoTracking()
                .Where(p => p.Excluido == true).ToListAsync();
        }
    }
}
