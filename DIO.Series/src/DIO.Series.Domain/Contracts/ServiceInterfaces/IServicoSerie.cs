using DIO.Series.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace DIO.Series.Domain.Contracts.ServiceInterfaces
{
    public interface IServicoSerie: IServicoBase<Serie>
    {
        bool ValidacaoAdicionarSerie(Serie serie);
        bool ValidacaoAlterarSerie(Serie serie);
        Task<bool> ValidacaoExcluirSerie(Guid id);
    }
}
