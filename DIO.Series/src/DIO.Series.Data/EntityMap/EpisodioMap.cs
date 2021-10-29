using DIO.Series.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DIO.Series.Data.EntityMap
{
    public class EpisodioMap : IEntityTypeConfiguration<Episodio>
    {
        public void Configure(EntityTypeBuilder<Episodio> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.NomeEpisodio)
               .IsRequired()
               .HasColumnType("varchar(100)");

            builder.Property(s => s.Descricao)
                .IsRequired()
                .HasColumnType("varchar(255)");

            builder.HasOne(s => s.Serie)
               .WithMany(e => e.Episodios)
               .HasForeignKey(f => f.SerieId);

            builder.ToTable(nameof(Episodio));
        }
    }
}
