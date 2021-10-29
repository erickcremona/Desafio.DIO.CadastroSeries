using DIO.Series.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DIO.Series.Domain.Contracts.RepositoryInterfaces
{
    public interface IRepositorioBase<TEntity> where TEntity : Entidade
    {
        Task<IEnumerable<TEntity>> ObterTodosAsync();
        Task<TEntity> ObterPorIdAsync(Guid id);
        Task<TEntity> ObterPorIdAsNoTrackingAsync(Guid id);
        Task<IEnumerable<TEntity>> ObterPorExpressaoAsync(Expression<Func<TEntity, bool>> predicate);
        void Adicionar(TEntity entity);
        void Atualizar(TEntity entity);
        void Excluir(TEntity entity);
        Task<int> SaveChangesAsync();
    }
}
