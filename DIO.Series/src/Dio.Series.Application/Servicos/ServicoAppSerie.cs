using Dio.Series.Application.Interfaces;
using DIO.Series.Domain.Contracts.RepositoryInterfaces;
using DIO.Series.Domain.Contracts.ServiceInterfaces;
using DIO.Series.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dio.Series.Application.Servicos
{
    public class ServicoAppSerie : IServicoAppSerie
    {
        private readonly IServicoSerie _servicoSerie;
        private readonly IRepositorioSerie _repositorioSerie;
        public ServicoAppSerie(IServicoSerie servicoSerie, 
                               IRepositorioSerie repositorioSerie)
        {
            _servicoSerie = servicoSerie;
            _repositorioSerie = repositorioSerie;
        }
        
        public async Task Adicionar(Serie serie)
        {
            if (!_servicoSerie.ValidacaoAdicionarSerie(serie)) return;

            if (!_servicoSerie.TemNotificacao())
            {
                _servicoSerie.Adicionar(serie);

                await _servicoSerie.SaveChangesAsync();
            }
        }

        public async Task Alterar(Serie serie)
        {
            if (!_servicoSerie.ValidacaoAlterarSerie(serie)) return;

            if (!_servicoSerie.TemNotificacao())
            {
                _servicoSerie.Atualizar(serie);

                await _servicoSerie.SaveChangesAsync();
            }
        }

        public async Task Excluir(Guid id)
        {
            if (! await _servicoSerie.ValidacaoExcluirSerie(id)) return;

            if (!_servicoSerie.TemNotificacao())
            {
                var excluir = await _servicoSerie.ObterPorIdAsync(id);

                _servicoSerie.Excluir(excluir);

                await _servicoSerie.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Serie>> ListarTodos()
        {
            return await _servicoSerie.ObterTodosAsync();
        }

        public async Task<IEnumerable<Serie>> ListarTodosExcluidos()
        {
            return await _repositorioSerie.ObterSeriesExcluidas();
        }

        public async Task<Serie> ObterSeriePorId(Guid id)
        {
            return await _servicoSerie.ObterPorIdAsync(id);
        }

        public async Task<Serie> ObterSeriePorIdComEpisodios(Guid id)
        {
            return await _repositorioSerie.ObterSerieComEpisodios(id);
        }
    }
}
