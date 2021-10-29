using DIO.Series.Domain.Entities;
using FluentValidation;

namespace DIO.Series.Domain.Validations
{
    public class ValidacaoEpisodio : AbstractValidator<Episodio>
    {
        public ValidacaoEpisodio()
        {
            RuleFor(episodio => episodio.NomeEpisodio)
               .NotEmpty().WithMessage("O campo Nome do Episódio precisa ser fornecido")
               .Length(2, 100)
               .WithMessage("O campo Nome do Episódio precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(episodio => episodio.Descricao)
               .NotEmpty().WithMessage("O campo Descrição precisa ser fornecido")
               .Length(2, 255)
               .WithMessage("O campo Descrição precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(episodio => episodio.NumeroEpisodio)
             .GreaterThan(0).WithMessage("O campo Número do Episódio precisa ser maior que 0");

            RuleFor(episodio => episodio.Temporada)
             .GreaterThan(0).WithMessage("O campo Temporada precisa ser maior que 0");

            RuleFor(episodio => episodio.MinutosEpisodio)
             .GreaterThan(0).WithMessage("O campo Tempo em Minutos precisa ser maior que 0");
        }
    }
}
