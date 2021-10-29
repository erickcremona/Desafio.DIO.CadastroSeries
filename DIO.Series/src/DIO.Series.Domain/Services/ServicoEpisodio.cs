using DIO.Series.Domain.Contracts.RepositoryInterfaces;
using DIO.Series.Domain.Contracts.ServiceInterfaces;
using DIO.Series.Domain.Entities;
using DIO.Series.Domain.Validations;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DIO.Series.Domain.Services
{
    public class ServicoEpisodio : ServicoBase<Episodio>, IServicoEpisodio
    {
        private readonly IRepositorioEpisodio _repositorioEpisodio;

        public ServicoEpisodio(IRepositorioEpisodio repositorioEpisodio, INotificacao notificacao) : base(repositorioEpisodio, notificacao)
        {
            _repositorioEpisodio = repositorioEpisodio;
        }

        public bool ValidacaoAdicionarEpisodio(Episodio episodio)
        {
            if (!ExecutarValidacao(new ValidacaoEpisodio(), episodio)) return false;

            if (_repositorioEpisodio.ObterPorExpressaoAsync(e => e.NomeEpisodio == episodio.NomeEpisodio && e.SerieId == episodio.SerieId).Result.Any())
            {
                Notificar("Já existe um episódio cadastrado com o nome informado.");
                return false;
            }
            return true;
        }

        public bool ValidacaoAlterarEpisodio(Episodio episodio)
        {
            if (!ExecutarValidacao(new ValidacaoEpisodio(), episodio)) return false;

            if (_repositorioEpisodio.ObterPorExpressaoAsync(e => e.NomeEpisodio == episodio.NomeEpisodio && e.SerieId == episodio.SerieId && e.Id != episodio.Id).Result.Any())
            {
                Notificar("Já existe um episódio cadastrado com o nome informado.");
                return false;
            }
            return true;
        }

        public async Task<bool> ValidacaoExcluirEpisodio(Guid id)
        {
            var excluir = await _repositorioEpisodio.ObterPorIdAsNoTrackingAsync(id);

            if (excluir == null)
            {
                Notificar("Id do episódio não foi localizado nos registros");
                return false;
            }           

            return true;
        }
    }
}
