using DIO.Series.Domain.Entities;
using FluentValidation;

namespace DIO.Series.Domain.Validations
{
    public class ValidacaoSerie : AbstractValidator<Serie>
    {
        public ValidacaoSerie()
        {
            RuleFor(serie => serie.Nome)
               .NotEmpty().WithMessage("O campo Nome precisa ser fornecido")
               .Length(2, 100)
               .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(serie => serie.Descricao)
               .NotEmpty().WithMessage("O campo Descrição precisa ser fornecido")
               .Length(2, 255)
               .WithMessage("O campo Descrição precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(serie => serie.Elenco)
              .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
              .Length(2, 255)
              .WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

            RuleFor(serie => serie.Classificacao)
              .GreaterThan(0).WithMessage("O campo Classificação precisa ser maior que 0");

            RuleFor(serie => serie.Temporadas)
              .GreaterThan(0).WithMessage("O campo Temporadas precisa ser maior que 0");

            RuleFor(serie => serie.Ano)
              .GreaterThan(1900).WithMessage("O campo {PropertyName} precisa ser maior que 1900");

            RuleFor(serie => serie.Genero)
              .NotNull().WithMessage("O campo Gênero precisa ser fornecido")
              .NotEmpty().WithMessage("O campo Gênero precisa ser fornecido");

            When(serie => serie.Genero == Genero.Terror || serie.Genero == Genero.Violencia, () =>
            {
                RuleFor(serie => serie.Classificacao)
               .GreaterThan(15).WithMessage("O campo Classificação precisa ser maior ou igual a 16");
            });

        }
    }
}
