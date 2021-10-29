using DIO.Series.Domain.Notificacoes;
using System.Collections.Generic;

namespace DIO.Series.Domain.Contracts.ServiceInterfaces
{
    public interface INotificacao
    {
        bool TemNotificacao();
        List<Notificacao> ObterNotificaoes();
        void Handle(Notificacao notificacao);
        void LimparNotificacoes();
    }
}
