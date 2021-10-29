using DIO.Series.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dio.Series.Application.Interfaces
{
    public interface IServicoAppSerie
    {
        Task Adicionar(Serie serie);
        Task Alterar(Serie serie);
        Task Excluir(Guid id);
        Task<IEnumerable<Serie>> ListarTodos();
        Task<Serie> ObterSeriePorIdComEpisodios(Guid id);
        Task<Serie> ObterSeriePorId(Guid id);
        Task<IEnumerable<Serie>> ListarTodosExcluidos();
    }
}
