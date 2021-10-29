using Dio.Series.Application.Interfaces;
using DIO.Series.Domain.Contracts.ServiceInterfaces;
using DIO.Series.Domain.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DIO.Series
{
    public class Menu : IMenu
    {
        private readonly IServicoAppSerie _servicoAppSerie;
        private readonly IServicoAppEpisodio _servicoAppEpisodio;
        private readonly INotificacao _notificacao;
        public Menu(IServicoAppSerie servicoAppSerie, IServicoAppEpisodio servicoAppEpisodio, INotificacao notificacao)
        {
            _servicoAppSerie = servicoAppSerie;
            _servicoAppEpisodio = servicoAppEpisodio;
            _notificacao = notificacao;
        }

        public async Task IniciarMenu()
        {
            Console.WriteLine(" 1 - Entrar no menu de Cadastro de séries");
            Console.WriteLine(" 2 - Entrar no menu de Cadastro dos episódios");
            Console.WriteLine(" 3 - Sair da Aplicação");

            Console.Write("\n Digite a opção desejada: ");

            var selecao = Console.ReadLine();


            if (int.TryParse(selecao, out int numeroSelecionado))
            {
                switch (numeroSelecionado)
                {
                    case 1:
                        Console.WriteLine($"\n ### Ok. Você escolheu a opção '{numeroSelecionado}'. ###\n");
                        Console.WriteLine(" *****************************************************************************");
                        Console.WriteLine(" AGORA VAMOS ENTRAR NO MENU DE CADASTRO DAS SÉRIES.");
                        Console.WriteLine(" *****************************************************************************\n\n");
                        Console.WriteLine(" POR FAVOR DIGITE UMA DAS OPÇÕES ABAIXO: \n");

                        await ExibirMenuSerie();
                        break;
                    case 2:
                        Console.WriteLine($"\n ### Ok. Você escolheu a opção '{numeroSelecionado}'. ###\n");
                        Console.WriteLine(" *****************************************************************************");
                        Console.WriteLine(" AGORA VAMOS ENTRAR NO MENU DE CADASTRO DOS EPISÓDIOS DAS SÉRIES.");
                        Console.WriteLine(" *****************************************************************************\n\n");

                        await ExibirMenuEpisodio();
                        break;
                    case 3:
                        Sair();
                        break;
                    default:
                        await SelecaoInvalidaMenuInicial(numeroSelecionado);
                        break;
                }
            }
            else
                await SelecaoInvalidaMenuInicial(selecao);
        }

        public async Task ExibirMenuSerie()
        {
            Console.WriteLine(" 1 - Listar todas as séries");
            Console.WriteLine(" 2 - Obter uma série");
            Console.WriteLine(" 3 - Listar séries excluídas");
            Console.WriteLine(" 4 - Adicionar nova série");
            Console.WriteLine(" 5 - Atualizar série");
            Console.WriteLine(" 6 - Excluir série");
            Console.WriteLine(" 7 - Voltar para menu principal");
            Console.WriteLine(" 8 - Sair da aplicação");
            Console.Write("\n Digite a opção desejada: ");

            var selecao = Console.ReadLine();

            if (int.TryParse(selecao, out int numeroSelecionado))
            {
                Console.WriteLine($"\n ### Ok. Você escolheu a opção '{numeroSelecionado}'. ###\n");

                switch (numeroSelecionado)
                {
                    case 1:
                        await ListarTodasSeries();
                        break;
                    case 2:
                        await ExibirExcluirUmaSeriePorId(false);
                        break;
                    case 3:
                        await ListarTodasSeriesExcluidas();
                        break;
                    case 4:
                        await AdicionarAlterarSerie(true);
                        break;
                    case 5:
                        await AdicionarAlterarSerie(false);
                        break;
                    case 6:
                        await ExibirExcluirUmaSeriePorId(true);
                        break;
                    case 7:
                        await ExibirMenuPrincipal();
                        break;
                    case 8:
                        Sair();
                        break;
                    default:
                        await SelecaoInvalidaSerie(numeroSelecionado);
                        break;
                }
            }
            else
                await SelecaoInvalidaSerie(selecao);
        }

        private async Task ListarTodasSeries()
        {
            var series = await _servicoAppSerie.ListarTodos();

            if (series.Any())
            {
                Console.WriteLine(" *****************************************************************************");
                Console.WriteLine(" TODAS AS SÉRIES ENCONTRADAS ESTÃO LISTADAS ABAIXO.");
                Console.WriteLine(" *****************************************************************************\n\n");

                foreach (var serie in series)
                {
                    Console.WriteLine("\n *****************************************************************************");
                    Console.WriteLine($" Id: {serie.Id}");
                    Console.WriteLine($" Nome da Série: {serie.Nome}");
                    Console.WriteLine($" Descrição: {serie.Descricao}");
                    Console.WriteLine($" Data de Cadastro: {serie.DataCadastro.ToShortDateString()}");
                    Console.WriteLine($" Ano da Série: {serie.Ano}");
                    Console.WriteLine($" Elenco: {serie.Elenco}");
                    Console.WriteLine($" Qtd Temporadas: {serie.Temporadas}");
                    Console.WriteLine($" Classificação: {serie.Classificacao} anos");
                    Console.WriteLine($" Gênero: {(Genero)serie.Genero}");
                    Console.WriteLine(" *****************************************************************************\n");
                }

                Console.WriteLine("\n *****************************************************************************");
                Console.WriteLine(" FIM DA LISTA DE SÉRIES CADASTRADAS NO SISTEMA.");
                Console.WriteLine(" *****************************************************************************\n\n");
            }
            else
            {
                Console.WriteLine(" *****************************************************************************");
                Console.WriteLine(" AINDA NÃO EXISTE SÉRIES CADASTRADAS, DIGITE 4 PARA CADASTRAR.");
                Console.WriteLine(" *****************************************************************************\n\n");
            }
            Console.WriteLine("\n POR FAVOR DIGITE UMA DAS OPÇÕES ABAIXO: \n");
            await ExibirMenuSerie();
        }


        private async Task ExibirExcluirUmaSeriePorId(bool excluir)
        {
            Console.Write("\n Digite agora o ID da serie: ");

            var id = Console.ReadLine();

            if (Guid.TryParse(id, out Guid idConsulta))
            {
                var serie = await _servicoAppSerie.ObterSeriePorId(idConsulta);

                if (serie != null)
                {
                    Console.WriteLine(" *****************************************************************************");
                    Console.WriteLine(" A SÉRIE CONSULTADA ESTÁ SENDO EXIBIDA ABAIXO.");
                    Console.WriteLine(" *****************************************************************************\n\n");

                    Console.WriteLine("\n *****************************************************************************");
                    Console.WriteLine($" Id: {serie.Id}");
                    Console.WriteLine($" Nome da Série: {serie.Nome}");
                    Console.WriteLine($" Descrição: {serie.Descricao}");
                    Console.WriteLine($" Data de Cadastro: {serie.DataCadastro.ToShortDateString()}");
                    Console.WriteLine($" Ano da Série: {serie.Ano}");
                    Console.WriteLine($" Elenco: {serie.Elenco}");
                    Console.WriteLine($" Qtd Temporadas: {serie.Temporadas}");
                    Console.WriteLine($" Classificação: {serie.Classificacao} anos");
                    Console.WriteLine($" Gênero: {(Genero)serie.Genero}");
                    Console.WriteLine(" *****************************************************************************\n");

                    if (excluir)
                    {
                        Console.WriteLine(" *****************************************************************************");
                        Console.WriteLine(" DESEJA REALMENTE EXCLUIR ESTA SÉRIE DO SISTEMA?");
                        Console.WriteLine(" *****************************************************************************\n\n");

                        Console.Write("\n DIGITE 'S' PARA SIM: ");
                        var sim = Console.ReadLine();

                        if (sim.Trim().ToLower() == "s")
                        {
                            try
                            {
                                serie.Excluido = true;
                                await _servicoAppSerie.Alterar(serie);

                                if (_notificacao.TemNotificacao())
                                {
                                    var notificacoes = _notificacao.ObterNotificaoes();

                                    Console.WriteLine($"\n *** :( Atenção! Não foi possível excluir a série. :( ***\n");
                                    Console.WriteLine(" *****************************************************************************");
                                    Console.WriteLine("\n LISTAMOS ABAIXO O(S) MOTIVO(S): \n");

                                    foreach (var item in notificacoes)
                                    {
                                        Console.WriteLine($" ########## {item.Mensagem}. ##########\n\n");
                                    }

                                    _notificacao.LimparNotificacoes();
                                }
                                else
                                {
                                    Console.WriteLine("\n\n *****************************************************************************");
                                    Console.WriteLine(" ************** :) SÉRIE EXCLUÍDA COM SUCESSO! :) **************");
                                    Console.WriteLine(" *****************************************************************************");
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"\n *** :( Atenção! Não foi excluir a série. :( ***\n");
                                Console.WriteLine(" *****************************************************************************");
                                Console.WriteLine("\n LISTAMOS ABAIXO O(S) MOTIVO(S): \n");

                                Console.WriteLine($"{ex.Message}.");
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine(" *****************************************************************************");
                    Console.WriteLine(" ID DA SÉRIE NÃO FOI ENCONTRADA NO SISTEMA.");
                    Console.WriteLine(" *****************************************************************************\n\n");
                }
                Console.WriteLine("\n POR FAVOR DIGITE UMA DAS OPÇÕES ABAIXO: \n");
                await ExibirMenuSerie();
            }
            else
            {
                Console.WriteLine(" *****************************************************************************");
                Console.WriteLine(" ID DIGITADO NÃO É VÁLIDO.");
                Console.WriteLine(" *****************************************************************************\n\n");
            }


            Console.WriteLine("\n POR FAVOR DIGITE UMA DAS OPÇÕES ABAIXO: \n");
            await ExibirMenuSerie();
        }


        private async Task ListarTodasSeriesExcluidas()
        {
            var series = await _servicoAppSerie.ListarTodosExcluidos();

            if (series.Any())
            {
                Console.WriteLine(" *****************************************************************************");
                Console.WriteLine(" TODAS AS SÉRIES EXCLUÍDAS ENCONTRADAS ESTÃO LISTADAS ABAIXO.");
                Console.WriteLine(" *****************************************************************************\n\n");

                foreach (var serie in series)
                {
                    Console.WriteLine("\n *****************************************************************************");
                    Console.WriteLine($" Id: {serie.Id}");
                    Console.WriteLine($" Nome da Série: {serie.Nome}");
                    Console.WriteLine($" Descrição: {serie.Descricao}");
                    Console.WriteLine($" Data de Cadastro: {serie.DataCadastro.ToShortDateString()}");
                    Console.WriteLine($" Ano da Série: {serie.Ano}");
                    Console.WriteLine($" Elenco: {serie.Elenco}");
                    Console.WriteLine($" Qtd Temporadas: {serie.Temporadas}");
                    Console.WriteLine($" Classificação: {serie.Classificacao} anos");
                    Console.WriteLine($" Gênero: {(Genero)serie.Genero}");
                    Console.WriteLine(" *****************************************************************************\n");
                }

                Console.WriteLine("\n *****************************************************************************");
                Console.WriteLine(" FIM DA LISTA DE SÉRIES EXCLUÍDAS.");
                Console.WriteLine(" *****************************************************************************\n\n");
            }
            else
            {
                Console.WriteLine(" *****************************************************************************");
                Console.WriteLine(" AINDA NÃO EXISTE SÉRIES EXCLUÍDAS ATÉ ESTE MOMENTO.");
                Console.WriteLine(" *****************************************************************************\n\n");
            }
            Console.WriteLine("\n POR FAVOR DIGITE UMA DAS OPÇÕES ABAIXO: \n");
            await ExibirMenuSerie();
        }

        private async Task AdicionarAlterarSerie(bool add)
        {
            Serie serie = null;

            if (add)
            {
                Console.WriteLine(" *****************************************************************************");
                Console.WriteLine(" VAMOS COMEÇAR AGORA O CADASTRO DA NOVA SÉRIE.");
                Console.WriteLine(" *****************************************************************************\n\n");

                serie = new Serie();
            }
            else
            {
                Console.WriteLine(" *****************************************************************************");
                Console.WriteLine(" VAMOS COMEÇAR AGORA A ALTERAÇÃO DA SÉRIE.");
                Console.WriteLine(" *****************************************************************************\n\n");

                Console.Write("\n Digite agora o ID da serie: ");

                var id = Console.ReadLine();

                if (Guid.TryParse(id, out Guid idConsulta))
                {
                    serie = await _servicoAppSerie.ObterSeriePorId(idConsulta);

                    if (serie != null)
                    {
                        Console.WriteLine(" *****************************************************************************");
                        Console.WriteLine(" A SÉRIE PARA ALTERAÇÃO ESTÁ SENDO EXIBIDA ABAIXO.");
                        Console.WriteLine(" *****************************************************************************\n\n");

                        Console.WriteLine("\n *****************************************************************************");
                        Console.WriteLine($" Id: {serie.Id}");
                        Console.WriteLine($" Nome da Série: {serie.Nome}");
                        Console.WriteLine($" Descrição: {serie.Descricao}");
                        Console.WriteLine($" Data de Cadastro: {serie.DataCadastro.ToShortDateString()}");
                        Console.WriteLine($" Ano da Série: {serie.Ano}");
                        Console.WriteLine($" Elenco: {serie.Elenco}");
                        Console.WriteLine($" Qtd Temporadas: {serie.Temporadas}");
                        Console.WriteLine($" Classificação: {serie.Classificacao} anos");
                        Console.WriteLine($" Gênero: {(Genero)serie.Genero}");
                        Console.WriteLine(" *****************************************************************************\n");
                    }

                }
                else
                {
                    Console.WriteLine(" *****************************************************************************");
                    Console.WriteLine(" ID DA SÉRIE NÃO FOI ENCONTRADO NO SISTEMA.");
                    Console.WriteLine(" *****************************************************************************\n\n");
                }

            }

            if (serie == null)
            {
                Console.WriteLine("\n POR FAVOR DIGITE UMA DAS OPÇÕES ABAIXO: \n");
                await ExibirMenuSerie();
            }

            var cadastroConcluido = false;
            var sair = false;


            void ExibirMenu()
            {
                Console.WriteLine(" 1 - Para alterar");
                Console.WriteLine(" 2 - Sair do cadastro");
                Console.WriteLine(" 3 - Continuar");
                Console.Write("\n Digite a opção desejada: ");
            }

            do
            {
                Console.Write("\n Digite agora o Nome da série: ");
                serie.Nome = Console.ReadLine();
                Console.WriteLine($"\n ### Ok. Você digitou: '{serie.Nome}'. ###\n");

                ExibirMenu();

                var selecao = Console.ReadLine();

                if (int.TryParse(selecao, out int numeroSelecionado))
                {
                    switch (numeroSelecionado)
                    {
                        case 1:
                            cadastroConcluido = false;
                            break;
                        case 2:
                            sair = true;
                            cadastroConcluido = true;
                            break;
                        default:
                            cadastroConcluido = true;
                            break;
                    }
                }
            } while (cadastroConcluido == false);

            if (sair == false)
            {
                cadastroConcluido = false;

                do
                {
                    Console.Write("\n Digite agora a Descrição da série: ");
                    serie.Descricao = Console.ReadLine();
                    Console.WriteLine($"\n ### Ok. Você digitou: '{serie.Descricao}'. ###\n");

                    ExibirMenu();

                    var selecao = Console.ReadLine();

                    if (int.TryParse(selecao, out int numeroSelecionado))
                    {
                        switch (numeroSelecionado)
                        {
                            case 1:
                                cadastroConcluido = false;
                                break;
                            case 2:
                                sair = true;
                                cadastroConcluido = true;
                                break;
                            default:
                                cadastroConcluido = true;
                                break;
                        }
                    }

                } while (cadastroConcluido == false);

            }

            if (sair == false)
            {

                cadastroConcluido = false;

                do
                {
                    Console.Write("\n Digite agora a quantidade de temporadas da série: ");

                    var ret = Console.ReadLine();

                    if (int.TryParse(ret, out int retorno))
                    {
                        serie.Temporadas = retorno;

                        Console.WriteLine($"\n ### Ok. Você digitou: '{serie.Temporadas}'. ###\n");

                        ExibirMenu();

                        var selecao = Console.ReadLine();

                        if (int.TryParse(selecao, out int numeroSelecionado))
                        {
                            switch (numeroSelecionado)
                            {
                                case 1:
                                    cadastroConcluido = false;
                                    break;
                                case 2:
                                    sair = true;
                                    cadastroConcluido = true;
                                    break;
                                default:
                                    cadastroConcluido = true;
                                    break;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine($"\n ### Atenção! Número de temporadas '{ret}' digitada é inválida. Digite um valor numérico inteiro. ###\n");
                    }

                } while (cadastroConcluido == false);
            }


            if (sair == false)
            {

                cadastroConcluido = false;

                do
                {
                    Console.Write("\n Digite agora a Classificação da série: ");

                    var ret = Console.ReadLine();

                    if (int.TryParse(ret, out int retorno))
                    {
                        serie.Classificacao = retorno;

                        Console.WriteLine($"\n ### Ok. Você digitou: '{serie.Classificacao}'. ###\n");

                        ExibirMenu();

                        var selecao = Console.ReadLine();

                        if (int.TryParse(selecao, out int numeroSelecionado))
                        {
                            switch (numeroSelecionado)
                            {
                                case 1:
                                    cadastroConcluido = false;
                                    break;
                                case 2:
                                    sair = true;
                                    cadastroConcluido = true;
                                    break;
                                default:
                                    cadastroConcluido = true;
                                    break;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine($"\n ### Atenção! Classificação '{ret}' digitada é inválida. Digite um valor numérico inteiro. ###\n");
                    }

                } while (cadastroConcluido == false);
            }

            if (sair == false)
            {
                cadastroConcluido = false;

                do
                {
                    Console.Write("\n Digite agora o Ano da série: ");

                    var ret = Console.ReadLine();

                    if (int.TryParse(ret, out int retorno))
                    {
                        serie.Ano = retorno;

                        Console.WriteLine($"\n ### Ok. Você digitou: '{serie.Ano}'. ###\n");

                        ExibirMenu();

                        var selecao = Console.ReadLine();

                        if (int.TryParse(selecao, out int numeroSelecionado))
                        {
                            switch (numeroSelecionado)
                            {
                                case 1:
                                    cadastroConcluido = false;
                                    break;
                                case 2:
                                    sair = true;
                                    cadastroConcluido = true;
                                    break;
                                default:
                                    cadastroConcluido = true;
                                    break;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine($"\n ### Atenção! Ano '{ret}' digitada é inválida. Digite um valor numérico inteiro. ###\n");
                    }

                } while (cadastroConcluido == false);
            }

            if (sair == false)
            {
                cadastroConcluido = false;

                do
                {
                    Console.Write("\n Digite agora o Elenco da série: ");
                    serie.Elenco = Console.ReadLine();
                    Console.WriteLine($"\n ### Ok. Você digitou: '{serie.Elenco}'. ###\n");

                    ExibirMenu();

                    var selecao = Console.ReadLine();

                    if (int.TryParse(selecao, out int numeroSelecionado))
                    {
                        switch (numeroSelecionado)
                        {
                            case 1:
                                cadastroConcluido = false;
                                break;
                            case 2:
                                sair = true;
                                cadastroConcluido = true;
                                break;
                            default:
                                cadastroConcluido = true;
                                break;
                        }
                    }
                } while (cadastroConcluido == false);
            }


            if (sair == false)
            {
                cadastroConcluido = false;


                do
                {
                    foreach (int i in Enum.GetValues(typeof(Genero)))
                    {
                        Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));
                    }
                    Console.Write("Digite o gênero entre as opções acima: ");

                    var ret = Console.ReadLine();

                    if (int.TryParse(ret, out int retorno))
                    {
                        serie.Genero = (Genero)retorno;

                        Console.WriteLine($"\n ### Ok. Você digitou: '{serie.Genero}'. ###\n");

                        ExibirMenu();

                        var selecao = Console.ReadLine();

                        if (int.TryParse(selecao, out int numeroSelecionado))
                        {
                            switch (numeroSelecionado)
                            {
                                case 1:
                                    cadastroConcluido = false;
                                    break;
                                case 2:
                                    sair = true;
                                    cadastroConcluido = true;
                                    break;
                                default:
                                    cadastroConcluido = true;
                                    break;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine($"\n ### Atenção! Gênero '{ret}' digitado é inválido. Digite um valor numérico inteiro. ###\n");
                    }

                } while (cadastroConcluido == false);
            }


            if (sair == false)
            {
                try
                {
                    if (add)
                        await _servicoAppSerie.Adicionar(serie);
                    else
                        await _servicoAppSerie.Alterar(serie);

                    if (_notificacao.TemNotificacao())
                    {
                        var notificacoes = _notificacao.ObterNotificaoes();

                        Console.WriteLine($"\n *** :( Atenção! Não foi possível efetuar o cadastro da série. :( ***\n");
                        Console.WriteLine(" *****************************************************************************");
                        Console.WriteLine("\n LISTAMOS ABAIXO O(S) MOTIVO(S): \n");

                        foreach (var item in notificacoes)
                        {
                            Console.WriteLine($" ########## {item.Mensagem}. ##########\n\n");
                        }

                        _notificacao.LimparNotificacoes();
                    }
                    else
                    {
                        Console.WriteLine("\n\n *****************************************************************************");
                        if (add)
                            Console.WriteLine(" ************** :) SÉRIE CADASTRADA COM SUCESSO! :) **************");
                        else
                            Console.WriteLine(" ************** :) SÉRIE ALTERADA COM SUCESSO! :) **************");
                        Console.WriteLine(" *****************************************************************************");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n *** :( Atenção! Não foi possível efetuar o cadastro da série. :( ***\n");
                    Console.WriteLine(" *****************************************************************************");
                    Console.WriteLine("\n LISTAMOS ABAIXO O(S) MOTIVO(S): \n");

                    Console.WriteLine($"{ex.Message}.");
                }
            }

            Console.WriteLine("\n POR FAVOR DIGITE UMA DAS OPÇÕES ABAIXO: \n");
            await ExibirMenuSerie();
        }


        public async Task ExibirMenuEpisodio()
        {
            var serie = await ObterSerieParaManipularEpisodios();

            if (serie == null)
                await ExibirMenuEpisodio();

            Console.WriteLine(" POR FAVOR DIGITE UMA DAS OPÇÕES ABAIXO: \n");

            Console.WriteLine(" 1 - Listar todos os episódios");
            Console.WriteLine(" 2 - Obter um episódio");
            Console.WriteLine(" 3 - Listar episódios excluídos");
            Console.WriteLine(" 4 - Adicionar novo episódio");
            Console.WriteLine(" 5 - Atualizar episódio");
            Console.WriteLine(" 6 - Excluir episódio");
            Console.WriteLine(" 7 - Voltar para menu principal");
            Console.WriteLine(" 8 - Sair da aplicação");

            Console.Write("\n Digite a opção desejada: ");

            var selecao = Console.ReadLine();

            if (int.TryParse(selecao, out int numeroSelecionado))
            {
                switch (numeroSelecionado)
                {
                    case 1:
                        await ListarTodosEpisodios(serie);
                        break;
                    case 2:
                        await ExibirExcluirUmEpisodioPorId(false, serie);
                        break;
                    case 3:
                        await ListarTodosEpisodiosExcluidos(serie);
                        break;
                    case 4:
                        await AdicionarAlterarEpisodio(true, serie);
                        break;
                    case 5:
                        await AdicionarAlterarEpisodio(false, serie);
                        break;
                    case 6:
                        await ExibirExcluirUmEpisodioPorId(true, serie);
                        break;
                    case 7:
                        await ExibirMenuPrincipal();
                        break;
                    case 8:
                        Sair();
                        break;
                    default:
                        await SelecaoInvalidaEpisodio(numeroSelecionado);
                        break;
                }
            }
            else
                await SelecaoInvalidaEpisodio(selecao);
        }

        private async Task<Serie> ObterSerieParaManipularEpisodios()
        {
            Console.WriteLine(" *****************************************************************************");
            Console.WriteLine(" VAMOS AGORA LOCALIZAR A SÉRIE PARA MANIPULAR OS EPISÓDIOS.");
            Console.WriteLine(" *****************************************************************************\n\n");

            Console.Write("\n Digite agora o ID da serie: ");

            var id = Console.ReadLine();

            Serie serie;

            if (Guid.TryParse(id, out Guid idConsulta))
            {
                serie = await _servicoAppSerie.ObterSeriePorIdComEpisodios(idConsulta);

                if (serie != null)
                {
                    Console.WriteLine(" *****************************************************************************");
                    Console.WriteLine(" A SÉRIE LOCALIZADA ESTÁ SENDO EXIBIDA ABAIXO.");
                    Console.WriteLine(" *****************************************************************************\n\n");

                    Console.WriteLine("\n *****************************************************************************");
                    Console.WriteLine($" Id: {serie.Id}");
                    Console.WriteLine($" Nome da Série: {serie.Nome}");
                    Console.WriteLine($" Descrição: {serie.Descricao}");
                    Console.WriteLine($" Data de Cadastro: {serie.DataCadastro.ToShortDateString()}");
                    Console.WriteLine($" Ano da Série: {serie.Ano}");
                    Console.WriteLine($" Elenco: {serie.Elenco}");
                    Console.WriteLine($" Qtd Temporadas: {serie.Temporadas}");
                    Console.WriteLine($" Classificação: {serie.Classificacao} anos");
                    Console.WriteLine($" Gênero: {(Genero)serie.Genero}");
                    Console.WriteLine(" *****************************************************************************\n");

                    return serie;
                }

                Console.WriteLine(" *****************************************************************************");
                Console.WriteLine(" ID DIGITADO NÃO FOI LOCALIZADA NO SISTEMA.");
                Console.WriteLine(" *****************************************************************************\n\n");
            }
            else
            {
                Console.WriteLine(" *****************************************************************************");
                Console.WriteLine(" ID DIGITADO NÃO É VÁLIDO.");
                Console.WriteLine(" *****************************************************************************\n\n");
            }

            return null;
        }

        private async Task AdicionarAlterarEpisodio(bool add, Serie serie)
        {
            Episodio episodio = null;

            if (add)
            {
                Console.WriteLine(" *****************************************************************************");
                Console.WriteLine($" NOVO EPISÓDIO NA SÉRIE: {serie.Nome}.");
                Console.WriteLine(" *****************************************************************************\n\n");

                episodio = new Episodio();
                episodio.SerieId = serie.Id;
            }
            else
            {
                Console.WriteLine(" *****************************************************************************");
                Console.WriteLine(" VAMOS COMEÇAR AGORA A ALTERAÇÃO DO EPÍSODIO.");
                Console.WriteLine(" *****************************************************************************\n\n");

                Console.Write("\n Digite agora o ID do episódio: ");

                var id = Console.ReadLine();

                if (Guid.TryParse(id, out Guid idConsulta))
                {
                    episodio = await _servicoAppEpisodio.ObterEpisodioPorId(idConsulta);

                    if (episodio != null)
                    {
                        Console.WriteLine(" *****************************************************************************");
                        Console.WriteLine(" A SÉRIE PARA ALTERAÇÃO ESTÁ SENDO EXIBIDA ABAIXO.");
                        Console.WriteLine(" *****************************************************************************\n\n");

                        Console.WriteLine("\n *****************************************************************************");
                        Console.WriteLine($" Id: {episodio.Id}");
                        Console.WriteLine($" Nome do Episódio: {episodio.NomeEpisodio}");
                        Console.WriteLine($" Descrição: {episodio.Descricao}");
                        Console.WriteLine($" Data de Cadastro: {episodio.DataCadastro.ToShortDateString()}");
                        Console.WriteLine($" Tempo em Minutos: {episodio.MinutosEpisodio}");
                        Console.WriteLine($" Número do Episódio: {episodio.NumeroEpisodio}");
                        Console.WriteLine($" Temporada: {episodio.Temporada}");
                        Console.WriteLine($" Id da Série: {episodio.SerieId}");
                        Console.WriteLine(" *****************************************************************************\n");
                    }
                }
            }

            if (episodio == null)
            {
                Console.WriteLine(" *****************************************************************************");
                Console.WriteLine(" ID DO EPISÓDIO NÃO FOI ENCONTRADO NO SISTEMA.");
                Console.WriteLine(" *****************************************************************************\n\n");
                await ExibirMenuPrincipal();
            }

            var cadastroConcluido = false;
            var sair = false;

            void ExibirMenu()
            {
                Console.WriteLine(" 1 - Para alterar");
                Console.WriteLine(" 2 - Sair do cadastro");
                Console.WriteLine(" 3 - Continuar");
                Console.Write("\n Digite a opção desejada: ");
            }

            do
            {
                Console.Write("\n Digite agora o Nome do episódio: ");
                episodio.NomeEpisodio = Console.ReadLine();
                Console.WriteLine($"\n ### Ok. Você digitou: '{episodio.NomeEpisodio}'. ###\n");

                ExibirMenu();

                var selecao = Console.ReadLine();

                if (int.TryParse(selecao, out int numeroSelecionado))
                {
                    switch (numeroSelecionado)
                    {
                        case 1:
                            cadastroConcluido = false;
                            break;
                        case 2:
                            sair = true;
                            cadastroConcluido = true;
                            break;
                        default:
                            cadastroConcluido = true;
                            break;
                    }
                }
            } while (cadastroConcluido == false);

            if (sair == false)
            {
                do
                {
                    Console.Write("\n Digite agora a descrição do episódio: ");
                    episodio.Descricao = Console.ReadLine();
                    Console.WriteLine($"\n ### Ok. Você digitou: '{episodio.Descricao}'. ###\n");

                    ExibirMenu();

                    var selecao = Console.ReadLine();

                    if (int.TryParse(selecao, out int numeroSelecionado))
                    {
                        switch (numeroSelecionado)
                        {
                            case 1:
                                cadastroConcluido = false;
                                break;
                            case 2:
                                sair = true;
                                cadastroConcluido = true;
                                break;
                            default:
                                cadastroConcluido = true;
                                break;
                        }
                    }
                } while (cadastroConcluido == false);
            }

            if (sair == false)
            {
                cadastroConcluido = false;

                do
                {
                    Console.Write("\n Digite agora o tempo do episódio em minutos: ");

                    var ret = Console.ReadLine();

                    if (int.TryParse(ret, out int retorno))
                    {
                        episodio.MinutosEpisodio = retorno;

                        Console.WriteLine($"\n ### Ok. Você digitou: '{episodio.MinutosEpisodio}'. ###\n");

                        ExibirMenu();

                        var selecao = Console.ReadLine();

                        if (int.TryParse(selecao, out int numeroSelecionado))
                        {
                            switch (numeroSelecionado)
                            {
                                case 1:
                                    cadastroConcluido = false;
                                    break;
                                case 2:
                                    sair = true;
                                    cadastroConcluido = true;
                                    break;
                                default:
                                    cadastroConcluido = true;
                                    break;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine($"\n ### Atenção! Tempo do episódio em minutos '{ret}' digitado é inválida. Digite um valor numérico inteiro. ###\n");
                    }

                } while (cadastroConcluido == false);
            }

            if (sair == false)
            {
                cadastroConcluido = false;

                do
                {
                    Console.Write("\n Digite agora o número do episódio: ");

                    var ret = Console.ReadLine();

                    if (int.TryParse(ret, out int retorno))
                    {
                        episodio.NumeroEpisodio = retorno;

                        Console.WriteLine($"\n ### Ok. Você digitou: '{episodio.NumeroEpisodio}'. ###\n");

                        ExibirMenu();

                        var selecao = Console.ReadLine();

                        if (int.TryParse(selecao, out int numeroSelecionado))
                        {
                            switch (numeroSelecionado)
                            {
                                case 1:
                                    cadastroConcluido = false;
                                    break;
                                case 2:
                                    sair = true;
                                    cadastroConcluido = true;
                                    break;
                                default:
                                    cadastroConcluido = true;
                                    break;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine($"\n ### Atenção! Número do episódio '{ret}' digitado é inválida. Digite um valor numérico inteiro. ###\n");
                    }

                } while (cadastroConcluido == false);
            }

            if (sair == false)
            {
                cadastroConcluido = false;

                do
                {
                    Console.Write("\n Digite agora a temporada do episódio: ");

                    var ret = Console.ReadLine();

                    if (int.TryParse(ret, out int retorno))
                    {
                        episodio.Temporada = retorno;

                        Console.WriteLine($"\n ### Ok. Você digitou: '{episodio.Temporada}'. ###\n");

                        ExibirMenu();

                        var selecao = Console.ReadLine();

                        if (int.TryParse(selecao, out int numeroSelecionado))
                        {
                            switch (numeroSelecionado)
                            {
                                case 1:
                                    cadastroConcluido = false;
                                    break;
                                case 2:
                                    sair = true;
                                    cadastroConcluido = true;
                                    break;
                                default:
                                    cadastroConcluido = true;
                                    break;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine($"\n ### Atenção! Temporada do episódio '{ret}' digitado é inválida. Digite um valor numérico inteiro. ###\n");
                    }

                } while (cadastroConcluido == false);
            }

            if (sair == false)
            {
                try
                {
                    if (add)
                        await _servicoAppEpisodio.Adicionar(episodio);
                    else
                        await _servicoAppEpisodio.Alterar(episodio);

                    if (_notificacao.TemNotificacao())
                    {
                        var notificacoes = _notificacao.ObterNotificaoes();

                        Console.WriteLine($"\n *** :( Atenção! Não foi possível efetuar o cadastro do episódio. :( ***\n");
                        Console.WriteLine(" *****************************************************************************");
                        Console.WriteLine("\n LISTAMOS ABAIXO O(S) MOTIVO(S): \n");

                        foreach (var item in notificacoes)
                        {
                            Console.WriteLine($" ########## {item.Mensagem}. ##########\n\n");
                        }

                        _notificacao.LimparNotificacoes();
                    }
                    else
                    {
                        Console.WriteLine("\n\n *****************************************************************************");
                        if (add)
                            Console.WriteLine(" ************** :) EPISÓDIO CADASTRADO COM SUCESSO! :) **************");
                        else
                            Console.WriteLine(" ************** :) EPISÓDIO ALTERADO COM SUCESSO! :) **************");
                        Console.WriteLine(" *****************************************************************************");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\n *** :( Atenção! Não foi possível efetuar o cadastro do episódio. :( ***\n");
                    Console.WriteLine(" *****************************************************************************");
                    Console.WriteLine("\n LISTAMOS ABAIXO O(S) MOTIVO(S): \n");

                    Console.WriteLine($"{ex.Message}.");
                }
            }

            Console.WriteLine("\n POR FAVOR DIGITE UMA DAS OPÇÕES ABAIXO: \n");
            await IniciarMenu();
        }

        private async Task ListarTodosEpisodios(Serie serie)
        {
            var episodios = await _servicoAppEpisodio.ListarTodosPorSerie(serie.Id);

            if (episodios.Any())
            {
                Console.WriteLine(" *****************************************************************************");
                Console.WriteLine(" TODOS OS EPISÓDIOS ENCONTRADOS DESTA SÉRIE ESTÃO LISTADOS ABAIXO.");
                Console.WriteLine(" *****************************************************************************\n\n");

                foreach (var episodio in episodios)
                {
                    Console.WriteLine("\n *****************************************************************************");
                    Console.WriteLine($" Id: {episodio.Id}");
                    Console.WriteLine($" Nome do Episódio: {episodio.NomeEpisodio}");
                    Console.WriteLine($" Descrição: {episodio.Descricao}");
                    Console.WriteLine($" Data de Cadastro: {episodio.DataCadastro.ToShortDateString()}");
                    Console.WriteLine($" Tempo em Minutos: {episodio.MinutosEpisodio}");
                    Console.WriteLine($" Número do Episódio: {episodio.NumeroEpisodio}");
                    Console.WriteLine($" Temporada: {episodio.Temporada}");
                    Console.WriteLine($" Id da Série: {episodio.SerieId}");
                    Console.WriteLine(" *****************************************************************************\n");
                }

                Console.WriteLine("\n *****************************************************************************");
                Console.WriteLine(" FIM DA LISTA DE EPISÓDIOS CADASTRADOS NESTA SÉRIE.");
                Console.WriteLine(" *****************************************************************************\n\n");
            }
            else
            {
                Console.WriteLine(" *****************************************************************************");
                Console.WriteLine(" AINDA NÃO EXISTE EPISÓDIOS CADASTRADOS PARA ESTA SÉRIE.");
                Console.WriteLine(" *****************************************************************************\n\n");
            }
            Console.WriteLine("\n POR FAVOR DIGITE UMA DAS OPÇÕES ABAIXO: \n");
            await IniciarMenu();
        }

        private async Task ListarTodosEpisodiosExcluidos(Serie serie)
        {
            var episodios = await _servicoAppEpisodio.ListarTodosExcluidosPorSerieId(serie.Id);

            if (episodios.Any())
            {
                Console.WriteLine(" *****************************************************************************");
                Console.WriteLine(" TODOS OS EPISÓDIOS EXCLUÍDOS DESTA SÉRIE ESTÃO LISTADOS ABAIXO.");
                Console.WriteLine(" *****************************************************************************\n\n");

                foreach (var episodio in episodios)
                {
                    Console.WriteLine("\n *****************************************************************************");
                    Console.WriteLine($" Id: {episodio.Id}");
                    Console.WriteLine($" Nome do Episódio: {episodio.NomeEpisodio}");
                    Console.WriteLine($" Descrição: {episodio.Descricao}");
                    Console.WriteLine($" Data de Cadastro: {episodio.DataCadastro.ToShortDateString()}");
                    Console.WriteLine($" Tempo em Minutos: {episodio.MinutosEpisodio}");
                    Console.WriteLine($" Número do Episódio: {episodio.NumeroEpisodio}");
                    Console.WriteLine($" Temporada: {episodio.Temporada}");
                    Console.WriteLine($" Id da Série: {episodio.SerieId}");
                    Console.WriteLine(" *****************************************************************************\n");
                }

                Console.WriteLine("\n *****************************************************************************");
                Console.WriteLine(" FIM DA LISTA DE EPISÓDIOS EXCLUÍDOS NESTA SÉRIE.");
                Console.WriteLine(" *****************************************************************************\n\n");
            }
            else
            {
                Console.WriteLine(" *****************************************************************************");
                Console.WriteLine(" NÃO EXISTE EPISÓDIOS EXCLUÍDOS PARA ESTA SÉRIE.");
                Console.WriteLine(" *****************************************************************************\n\n");
            }
            Console.WriteLine("\n POR FAVOR DIGITE UMA DAS OPÇÕES ABAIXO: \n");
            await IniciarMenu();
        }

        private async Task ExibirExcluirUmEpisodioPorId(bool excluir, Serie serie)
        {
            Console.Write("\n Digite agora o ID do Episódio: ");

            var id = Console.ReadLine();

            if (Guid.TryParse(id, out Guid idConsulta))
            {
                var episodio = await _servicoAppEpisodio.ObterEpisodioPorId(idConsulta);

                if (episodio != null)
                {
                    Console.WriteLine(" *****************************************************************************");
                    Console.WriteLine(" O EPISÓDIO ESTÁ SENDO EXIBIDO ABAIXO.");
                    Console.WriteLine(" *****************************************************************************\n\n");

                    Console.WriteLine("\n *****************************************************************************");
                    Console.WriteLine($" Id: {episodio.Id}");
                    Console.WriteLine($" Nome do Episódio: {episodio.NomeEpisodio}");
                    Console.WriteLine($" Descrição: {episodio.Descricao}");
                    Console.WriteLine($" Data de Cadastro: {episodio.DataCadastro.ToShortDateString()}");
                    Console.WriteLine($" Tempo em Minutos: {episodio.MinutosEpisodio}");
                    Console.WriteLine($" Número do Episódio: {episodio.NumeroEpisodio}");
                    Console.WriteLine($" Temporada: {episodio.Temporada}");
                    Console.WriteLine($" Id da Série: {episodio.SerieId}");
                    Console.WriteLine(" *****************************************************************************\n");

                    if (excluir)
                    {
                        Console.WriteLine(" *****************************************************************************");
                        Console.WriteLine(" DESEJA REALMENTE EXCLUIR ESTE EPISÓDIO?");
                        Console.WriteLine(" *****************************************************************************\n\n");

                        Console.Write("\n DIGITE 'S' PARA SIM: ");
                        var sim = Console.ReadLine();

                        if (sim.Trim().ToLower() == "s")
                        {
                            try
                            {
                                episodio.Excluido = true;
                                await _servicoAppEpisodio.Alterar(episodio);

                                if (_notificacao.TemNotificacao())
                                {
                                    var notificacoes = _notificacao.ObterNotificaoes();

                                    Console.WriteLine($"\n *** :( Atenção! Não foi possível excluir o episódio. :( ***\n");
                                    Console.WriteLine(" *****************************************************************************");
                                    Console.WriteLine("\n LISTAMOS ABAIXO O(S) MOTIVO(S): \n");

                                    foreach (var item in notificacoes)
                                    {
                                        Console.WriteLine($" ########## {item.Mensagem}. ##########\n\n");
                                    }

                                    _notificacao.LimparNotificacoes();
                                }
                                else
                                {
                                    Console.WriteLine("\n\n *****************************************************************************");
                                    Console.WriteLine(" ************** :) EPISÓDIO EXCLUÍDO COM SUCESSO! :) **************");
                                    Console.WriteLine(" *****************************************************************************");
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"\n *** :( Atenção! Não foi possível excluir o episódio. :( ***\n");
                                Console.WriteLine(" *****************************************************************************");
                                Console.WriteLine("\n LISTAMOS ABAIXO O(S) MOTIVO(S): \n");

                                Console.WriteLine($"{ex.Message}.");
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine(" *****************************************************************************");
                    Console.WriteLine(" ID DO EPISÓDIO NÃO FOI LOCALIZADO NO SISTEMA.");
                    Console.WriteLine(" *****************************************************************************\n\n");
                }
                Console.WriteLine("\n POR FAVOR DIGITE UMA DAS OPÇÕES ABAIXO: \n");
                await IniciarMenu();
            }
            else
            {
                Console.WriteLine(" *****************************************************************************");
                Console.WriteLine(" ID DIGITADO NÃO É VÁLIDO.");
                Console.WriteLine(" *****************************************************************************\n\n");
            }


            Console.WriteLine("\n POR FAVOR DIGITE UMA DAS OPÇÕES ABAIXO: \n");
            await ExibirMenuSerie();
        }

        public async Task ExibirMenuPrincipal()
        {
            Console.WriteLine("\n\n *****************************************************************************");
            Console.WriteLine(" ************** SEJA BEM-VINDO AO SISTEMA DE CADASTRO DE SÉRIES **************");
            Console.WriteLine(" *****************************************************************************");
            Console.WriteLine("\n POR FAVOR DIGITE UMA DAS OPÇÕES ABAIXO: \n");
            await IniciarMenu();
        }

        public void Sair()
        {
            Console.WriteLine("\n\n ****************************************************************************************");
            Console.WriteLine(" **************  OBRIGADO POR UTILIZAR NOSSO SISTEMA DE CADASTRO DE SÉRIES **************");
            Console.WriteLine(" ****************************************************************************************");

            Console.WriteLine("\n\n *****************************************************************************");
            Console.WriteLine("          **************  FOI UM PRAZER ESTÁ COM VOCÊ **************");
            Console.WriteLine(" *****************************************************************************");
            Environment.Exit(0);
        }

        private async Task SelecaoInvalidaSerie(object opcao)
        {
            Console.WriteLine($"\n *** Atenção! Opção '{opcao}' digitada é inválida. ***\n");
            Console.WriteLine(" *****************************************************************************");
            Console.WriteLine("\n POR FAVOR DIGITE UMA DAS OPÇÕES ABAIXO: \n");
            await ExibirMenuSerie();
        }
        private async Task SelecaoInvalidaEpisodio(object opcao)
        {
            Console.WriteLine($"\n *** Atenção! Opção '{opcao}' digitada é inválida. ***\n");
            Console.WriteLine(" *****************************************************************************");
            Console.WriteLine("\n POR FAVOR DIGITE UMA DAS OPÇÕES ABAIXO: \n");
            await ExibirMenuEpisodio();
        }

        private async Task SelecaoInvalidaMenuInicial(object opcao)
        {
            Console.WriteLine($"\n *** Atenção! Opção '{opcao}' digitada é inválida. ***\n");
            Console.WriteLine(" *****************************************************************************");
            Console.WriteLine("\n POR FAVOR DIGITE UMA DAS OPÇÕES ABAIXO: \n");
            await IniciarMenu();
        }
    }
}
