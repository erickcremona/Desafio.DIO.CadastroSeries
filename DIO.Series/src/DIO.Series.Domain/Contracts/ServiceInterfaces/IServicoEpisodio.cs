using DIO.Series.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace DIO.Series.Domain.Contracts.ServiceInterfaces
{
    public interface IServicoEpisodio : IServicoBase<Episodio>
    {
        bool ValidacaoAdicionarEpisodio(Episodio episodio);
        bool ValidacaoAlterarEpisodio(Episodio episodio);
        Task<bool> ValidacaoExcluirEpisodio(Guid id);
    }
}
