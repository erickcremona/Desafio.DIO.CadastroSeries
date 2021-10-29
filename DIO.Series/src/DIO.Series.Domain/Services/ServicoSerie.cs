using DIO.Series.Domain.Contracts.RepositoryInterfaces;
using DIO.Series.Domain.Contracts.ServiceInterfaces;
using DIO.Series.Domain.Entities;
using DIO.Series.Domain.Validations;
using System.Linq;
using System;
using System.Threading.Tasks;

namespace DIO.Series.Domain.Services
{
    public class ServicoSerie : ServicoBase<Serie>, IServicoSerie
    {
        private readonly IRepositorioSerie _repositorioSerie;

        public ServicoSerie(IRepositorioSerie repositorioSerie, INotificacao notificacao) : base(repositorioSerie, notificacao)
        {
            _repositorioSerie = repositorioSerie;
        }

        public bool ValidacaoAdicionarSerie(Serie serie)
        {
            if (!ExecutarValidacao(new ValidacaoSerie(), serie)) return false;

            if (_repositorioSerie.ObterPorExpressaoAsync(s => s.Nome == serie.Nome && s.Ano == serie.Ano).Result.Any())
            {
                Notificar("Já existe uma série com nome e ano informados.");
                return false;
            }
            return true;
        }

        public bool ValidacaoAlterarSerie(Serie serie)
        {
            if (!ExecutarValidacao(new ValidacaoSerie(), serie)) return false;

            if (_repositorioSerie.ObterPorExpressaoAsync(s => s.Nome == serie.Nome && s.Ano == serie.Ano && s.Id != serie.Id).Result.Any())
            {
                Notificar("Já existe uma série com nome e ano informados.");
                return false;
            }
            return true;
        }

        public async Task<bool> ValidacaoExcluirSerie(Guid id)
        {
            var excluir = await _repositorioSerie.ObterSerieComEpisodios(id);

            if (excluir == null)
            {
                Notificar("Id da série não foi localizado nos registros");
                return false;
            }

            if (excluir.Episodios.Any())
            {
                Notificar("A série possui episódios cadastrados!");
                return false;
            }

            return true;
        }
    }
}
