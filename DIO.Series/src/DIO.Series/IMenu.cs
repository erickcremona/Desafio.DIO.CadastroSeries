using System.Threading.Tasks;

namespace DIO.Series
{
    public interface IMenu
    {
        Task ExibirMenuPrincipal();
        Task IniciarMenu();
        void Sair();
        Task ExibirMenuEpisodio();
        Task ExibirMenuSerie();
    }
}
