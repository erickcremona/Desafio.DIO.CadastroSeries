using DIO.Series.Domain.Contracts.RepositoryInterfaces;
using DIO.Series.Domain.Contracts.ServiceInterfaces;
using DIO.Series.Domain.Entities;
using DIO.Series.Domain.Notificacoes;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DIO.Series.Domain.Services
{
    public class ServicoBase<TEntity> : IServicoBase<TEntity> where TEntity : Entidade
    {
        private readonly IRepositorioBase<TEntity> _repositorioBase;

        private readonly INotificacao _notificacao;

        public ServicoBase(IRepositorioBase<TEntity> repositorioBase, INotificacao notificacao)
        {
            _repositorioBase = repositorioBase;
            _notificacao = notificacao;
        }

        public void Adicionar(TEntity entity)
        {
            _repositorioBase.Adicionar(entity);
        }

        public void Atualizar(TEntity entity)
        {
            _repositorioBase.Atualizar(entity);
        }

        public void Excluir(TEntity entity)
        {
            _repositorioBase.Excluir(entity);
        }

        public bool ExecutarValidacao<TV, TE>(TV validation, TE entity)
            where TV : AbstractValidator<TE>
            where TE : Entidade
        {
            var validator = validation.Validate(entity);
            Notificar(validator);
            return validator.IsValid;
        }

        public void Notificar(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
                Notificar(error.ErrorMessage);
        }

        public void Notificar(string mensagem)
        {
            _notificacao.Handle(new Notificacao(mensagem));
        }

        public async Task<IEnumerable<TEntity>> ObterPorExpressaoAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _repositorioBase.ObterPorExpressaoAsync(predicate);
        }

        public async Task<TEntity> ObterPorIdAsNoTrackingAsync(Guid id)
        {
            return await _repositorioBase.ObterPorIdAsNoTrackingAsync(id);
        }

        public async Task<TEntity> ObterPorIdAsync(Guid id)
        {
            return await _repositorioBase.ObterPorIdAsync(id);
        }

        public async Task<IEnumerable<TEntity>> ObterTodosAsync()
        {
            return await _repositorioBase.ObterTodosAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _repositorioBase.SaveChangesAsync();
        }

        public bool TemNotificacao()
        {
            return _notificacao.TemNotificacao();
        }
    }
}
