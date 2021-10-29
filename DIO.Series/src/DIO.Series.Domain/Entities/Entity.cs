using System;

namespace DIO.Series.Domain.Entities
{
    public abstract class Entidade
    {
        public Guid Id { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool Excluido { get; set; }
        public Entidade()
        {
            Id = Guid.NewGuid();
        }
    }
}
