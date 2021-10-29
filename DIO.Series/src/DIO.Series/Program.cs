using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace DIO.Series
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var services = new ServiceCollection();
            services.ConfigureServices();
            var serviceProvider = services.BuildServiceProvider();     
            var menu = serviceProvider.GetService<IMenu>();
            await menu.ExibirMenuPrincipal();
        }
    }
}
