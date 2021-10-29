using DIO.Series.Domain.Contracts.RepositoryInterfaces;
using DIO.Series.Domain.Entities;
using FluentValidation;
using FluentValidation.Results;

namespace DIO.Series.Domain.Contracts.ServiceInterfaces
{
    public interface IServicoBase<TEntity> : IRepositorioBase<TEntity> where TEntity : Entidade
    {
        void Notificar(ValidationResult validationResult);
        void Notificar(string mensagem);
        bool ExecutarValidacao<TV, TE>(TV validation, TE entity) where TV : AbstractValidator<TE> where TE : Entidade;
        bool TemNotificacao();
    }
}
