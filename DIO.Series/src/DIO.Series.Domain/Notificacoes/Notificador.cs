using DIO.Series.Domain.Contracts.ServiceInterfaces;
using System.Collections.Generic;
using System.Linq;

namespace DIO.Series.Domain.Notificacoes
{
    public class Notificador : INotificacao
    {
        private List<Notificacao> _notificacoes;

        public Notificador()
        {
            _notificacoes = new List<Notificacao>();
        }

        public void Handle(Notificacao notificacao)
            => _notificacoes.Add(notificacao);

        public void LimparNotificacoes()
        {
            _notificacoes.Clear();
        }

        public List<Notificacao> ObterNotificaoes()
            => _notificacoes;

        public bool TemNotificacao()
            => _notificacoes.Any();
    }
}
