using DIO.Series.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Dio.Series.Application.Interfaces
{
    public interface IServicoAppEpisodio
    {
        Task Adicionar(Episodio episodio);
        Task Alterar(Episodio episodio);
        Task Excluir(Guid id);
        Task<IEnumerable<Episodio>> ListarTodosPorSerie(Guid serieId);
        Task<Episodio> ObterEpisodioPorId(Guid id);
        Task<IEnumerable<Episodio>> ListarTodosExcluidosPorSerieId(Guid serieId);
    }
}
