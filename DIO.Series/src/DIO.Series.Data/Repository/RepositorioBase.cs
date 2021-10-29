using DIO.Series.Data.Context;
using DIO.Series.Domain.Contracts.RepositoryInterfaces;
using DIO.Series.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DIO.Series.Data.Repository
{
    public class RepositorioBase<TEntity> : IRepositorioBase<TEntity> where TEntity : Entidade
    {
        private readonly ContextSeries _contextSeries;
        protected readonly DbSet<TEntity> _dbSet;

        public RepositorioBase(ContextSeries contextSeries)
        {
            _contextSeries = contextSeries;
            _dbSet = _contextSeries.Set<TEntity>();
        }
        public void Adicionar(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public void Atualizar(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public void Excluir(TEntity entity)
        {
            entity.Excluido = true;
            _dbSet.Update(entity);
        }

        public async Task<IEnumerable<TEntity>> ObterPorExpressaoAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task<TEntity> ObterPorIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> ObterTodosAsync()
        {
            return await _dbSet.Where(p => p.Excluido == false).ToListAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _contextSeries.SaveChangesAsync();
        }

        public async Task<TEntity> ObterPorIdAsNoTrackingAsync(Guid id)
        {
            return await _dbSet.AsNoTracking().Where(p => p.Id == id && p.Excluido == false).FirstOrDefaultAsync();
        }
    }
}
