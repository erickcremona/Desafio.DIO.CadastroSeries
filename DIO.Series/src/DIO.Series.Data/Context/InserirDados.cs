using DIO.Series.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace DIO.Series.Data.Context
{
    public static class InserirDados
    {
        public static void DadosIniciais(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Serie>().HasData(
               new Serie
               {
                   Id = Guid.Parse("B349C719-7E30-4E51-AEAE-9737966446F6"),
                   DataCadastro = DateTime.Now.Date,
                   Excluido = false,
                   Nome = "A ANATOMIA DE GREY ",
                   Ano = 2020,
                   Classificacao = 14,
                   Descricao = "DURANTE SUA RESIDÊNCIA, MEREDITH GREY, VIVE PAIXÕES PROFISSIONAIS E PESSOAIS COM SEUS COLEGAS MÉDICOS EM UM HOSPITAL DE SEATTLE.",
                   Elenco = "ELLEN POMPAO, SANDRA OH, KATHERINE HEIGL, E OUTROS",
                   Genero = Genero.Drama,
                   Temporadas = 17
               },
               new Serie
               {
                   Id = Guid.Parse("8CF187FB-5DAA-4A7A-B974-D1CA8B4591AD"),
                   DataCadastro = DateTime.Now.Date,
                   Excluido = false,
                   Nome = "SINTONIA",
                   Ano = 2021,
                   Classificacao = 16,
                   Descricao = "CRIADOS JUNTOS NA QUEBRADA DE SÃO PAULO, TRÊS JOVENS AMIGOS CORREM ATRÁS DOS SEUS SONHOS RODEADOS DE MÚSICA, DROGAS E RELIGIÃO.",
                   Elenco = "CHRISTIAN MALHEIROS, JOTTAPÊ, BRUNA MASCARENHAS",
                   Genero = Genero.Policial,
                   Temporadas = 2
               }
           );


            modelBuilder.Entity<Episodio>().HasData(
              new Episodio
              {
                  Id = Guid.NewGuid(),
                  SerieId = Guid.Parse("B349C719-7E30-4E51-AEAE-9737966446F6"),
                  DataCadastro = DateTime.Now.Date,
                  Excluido = false,
                  NomeEpisodio = "LONGA NOITE, LONGO DIA",
                  MinutosEpisodio = 43,
                  NumeroEpisodio = 1,
                  Descricao = "NO SEU PRIMEIRO DIA NO ANO DE RESIDêNCIA, MEREDITH GREY, FILHA DE UMA FAMOSA CIRURGIÃ, CONHECE SEUS COLEGAS NO HOSPITAL DE SEATTLE GRACE.",
                  Temporada = 1
              },
               new Episodio
               {
                   Id = Guid.NewGuid(),
                   SerieId = Guid.Parse("B349C719-7E30-4E51-AEAE-9737966446F6"),
                   DataCadastro = DateTime.Now.Date,
                   Excluido = false,
                   NomeEpisodio = "O PRIMEIRO PLANTÃO É O MAIS DIFÍCIL",
                   MinutosEpisodio = 42,
                   NumeroEpisodio = 2,
                   Descricao = "MEREDITH COLOCA SUA CARREIRA EM RISCO PARA SALVAR UM RECÉM-NASCIDO NO BERÇÁRIO DO HOSPITAL.",
                   Temporada = 1
               },
               new Episodio
               {
                   Id = Guid.NewGuid(),
                   SerieId = Guid.Parse("B349C719-7E30-4E51-AEAE-9737966446F6"),
                   DataCadastro = DateTime.Now.Date,
                   Excluido = false,
                   NomeEpisodio = "GANHAMOS A BATALHA, PERDEMOS A GERRA",
                   MinutosEpisodio = 43,
                   NumeroEpisodio = 3,
                   Descricao = "INTERNOS COMPETEM PARA TRATAR VÁRIOS CASOS GRAVES QUANDO A EMERGÊNCIA FICA CHEIA COM PACIENTES DE UMA CORRIDA ANUAL DE BICICLETA.",
                   Temporada = 1
               },
               new Episodio
               {
                   Id = Guid.NewGuid(),
                   SerieId = Guid.Parse("8CF187FB-5DAA-4A7A-B974-D1CA8B4591AD"),
                   DataCadastro = DateTime.Now.Date,
                   Excluido = false,
                   NomeEpisodio = "PEGARAM A CACAU",
                   MinutosEpisodio = 37,
                   NumeroEpisodio = 1,
                   Descricao = "DONI TEM ESPERANÇA DE SER UM ASTRO DO FUNK. NANDO TENTA FAZER CARREIRA NO TRÁFICO. RITA SE PREOCUPA COM UMA AMIGA.",
                   Temporada = 1
               },
               new Episodio
               {
                   Id = Guid.NewGuid(),
                   SerieId = Guid.Parse("8CF187FB-5DAA-4A7A-B974-D1CA8B4591AD"),
                   DataCadastro = DateTime.Now.Date,
                   Excluido = false,
                   NomeEpisodio = "FIZ UMA PRO CRIME",
                   MinutosEpisodio = 47,
                   NumeroEpisodio = 2,
                   Descricao = "MACHUCADA, RITA SE SENTE CULPADA POR CACAU. NANDO TEM PROBLEMAS COM A POLÍCIA. DONI ACUSA DONDOKA DE ROUBAR SUA MÚSICA.",
                   Temporada = 1
               },
               new Episodio
               {
                   Id = Guid.NewGuid(),
                   SerieId = Guid.Parse("8CF187FB-5DAA-4A7A-B974-D1CA8B4591AD"),
                   DataCadastro = DateTime.Now.Date,
                   Excluido = false,
                   NomeEpisodio = "SEGUNDA CHANCE",
                   MinutosEpisodio = 42,
                   NumeroEpisodio = 3,
                   Descricao = "DONI COMEÇA A CURTIR O SUCESSO, MAS OS AMIGOS INSISTEM PARA ELE METER OS PÉS NO CHÃO. NANDO DEFENDE JUNINHO CONTRA AMEAÇAS.",
                   Temporada = 1
               }
          );

        }
    }
}
