using Dio.Series.Application.Interfaces;
using DIO.Series.Domain.Contracts.RepositoryInterfaces;
using DIO.Series.Domain.Contracts.ServiceInterfaces;
using DIO.Series.Domain.Entities;
using DIO.Series.Domain.Notificacoes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dio.Series.Application.Servicos
{
    public class ServicoAppEpisodio : IServicoAppEpisodio
    {
        private readonly IServicoEpisodio _servicoEpisodio;
        private readonly IRepositorioEpisodio _repositorioEpisodio;
        public ServicoAppEpisodio(IServicoEpisodio servicoEpisodio, 
                                  IRepositorioEpisodio repositorioEpisodio)
        {
            _servicoEpisodio = servicoEpisodio;
            _repositorioEpisodio = repositorioEpisodio;
        }
        public async Task Adicionar(Episodio episodio)
        {
            if (!_servicoEpisodio.ValidacaoAdicionarEpisodio(episodio)) return;

            if (!_servicoEpisodio.TemNotificacao())
            {
                _servicoEpisodio.Adicionar(episodio);

                await _servicoEpisodio.SaveChangesAsync();
            }
        }

        public async Task Alterar(Episodio episodio)
        {
            if (!_servicoEpisodio.ValidacaoAlterarEpisodio(episodio)) return;

            if (!_servicoEpisodio.TemNotificacao())
            {
                _servicoEpisodio.Atualizar(episodio);

                await _servicoEpisodio.SaveChangesAsync();
            }
        }

        public async Task Excluir(Guid id)
        {
            if (! await _servicoEpisodio.ValidacaoExcluirEpisodio(id)) return;

            if (!_servicoEpisodio.TemNotificacao())
            {
                var excluir = await _servicoEpisodio.ObterPorIdAsync(id);

                _servicoEpisodio.Excluir(excluir);

                await _servicoEpisodio.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Episodio>> ListarTodosExcluidosPorSerieId(Guid serieId)
        {
            return await _repositorioEpisodio.ObterEpisodioExcluidosPorSerieId(serieId);
        }

        public async Task<IEnumerable<Episodio>> ListarTodosPorSerie(Guid serieId)
        {
            return await _repositorioEpisodio.ObterEpisodioPorSerieId(serieId);
        }

        public async Task<Episodio> ObterEpisodioPorId(Guid id)
        {
            return await _repositorioEpisodio.ObterPorIdAsync(id);
        }
    }
    
}
