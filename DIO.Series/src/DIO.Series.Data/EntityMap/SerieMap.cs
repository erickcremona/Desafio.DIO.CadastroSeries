using DIO.Series.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DIO.Series.Data.EntityMap
{
    public class SerieMap : IEntityTypeConfiguration<Serie>
    {
        public void Configure(EntityTypeBuilder<Serie> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.Nome)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(s => s.Descricao)
                .IsRequired()
                .HasColumnType("varchar(255)");

            builder.Property(s => s.Elenco)
                .IsRequired()
                .HasColumnType("varchar(255)");

           

            builder.ToTable(nameof(Serie));
        }
    }
}
