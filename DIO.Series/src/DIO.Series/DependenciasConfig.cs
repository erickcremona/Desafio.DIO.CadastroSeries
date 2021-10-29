using Dio.Series.Application.Interfaces;
using Dio.Series.Application.Servicos;
using DIO.Series.Data.Context;
using DIO.Series.Data.Repository;
using DIO.Series.Domain.Contracts.RepositoryInterfaces;
using DIO.Series.Domain.Contracts.ServiceInterfaces;
using DIO.Series.Domain.Notificacoes;
using DIO.Series.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DIO.Series
{
    public static class DependenciasConfig
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services)
        {
            services.AddSingleton<ContextSeries>();
            services.AddSingleton<INotificacao, Notificador>();
            services.AddTransient(typeof(IRepositorioBase<>), typeof(RepositorioBase<>));
            services.AddTransient(typeof(IServicoBase<>), typeof(ServicoBase<>));
            services.AddTransient<IServicoAppEpisodio, ServicoAppEpisodio>();
            services.AddTransient<IServicoAppSerie, ServicoAppSerie>();
            services.AddTransient<IServicoEpisodio, ServicoEpisodio>();
            services.AddTransient<IServicoSerie, ServicoSerie>();
            services.AddTransient<IRepositorioEpisodio, RepositorioEpisodio>();
            services.AddTransient<IRepositorioSerie, RepositorioSerie>();
            services.AddTransient<IMenu, Menu>();

            return services;
        }
    }
}
