using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NutritionSystem.Domain.Enums;

namespace NutritionSystem.Infrastructure.Persistence.Configurations
{
    public class DiagnosticoConfiguration : IEntityTypeConfiguration<Diagnostico>
    {
        public void Configure(EntityTypeBuilder<Diagnostico> builder)
        {
            builder.ToTable("Diagnostico");

            // Como la tabla SQL no tiene PK, EF Core necesita una.
            // Le damos una PK Guid en el modelo de dominio.
            //builder.HasKey(d => d.Id);

            //builder.Property(d => d.Id)
            //    .ValueGeneratedOnAdd(); // EF Core generará este GUID
            builder.Ignore(x => x.Id);

            builder.HasKey(x => x.ConsultaId);

            builder.Property(d => d.ConsultaId)
                .HasColumnName("IdConsulta")
                .IsRequired();

            var tipoDiagnosticoConverter = new ValueConverter<TipoDiagnostico, string>(
                tipoDiagnosticoEnumValue => tipoDiagnosticoEnumValue.ToString(),
                tipoDiagnostico => (TipoDiagnostico)Enum.Parse(typeof(TipoDiagnostico), tipoDiagnostico)
                );

            builder.Property(d => d.TipoDiagnostico)
                .HasConversion(tipoDiagnosticoConverter)
                .HasColumnName("TipoDiagnostico")
                .HasMaxLength(50);
            //builder.Property(d => d.TipoDiagnostico)
            //    .HasColumnName("TipoDiagnostico")
            //    .HasConversion<string>()
            //    .HasMaxLength(50);

            builder.Property(d => d.Descripcion)
                .HasColumnName("Valor")
                .HasMaxLength(200);

            var tipoStatusConverter = new ValueConverter<TipoStatus, string>(
                tipoStatusEnumValue => tipoStatusEnumValue.ToString(),
                tipoStatus => (TipoStatus)Enum.Parse(typeof(TipoStatus), tipoStatus)
                );
            builder.Property(d => d.TipoStatus)
                .HasConversion(tipoStatusConverter)
                .HasColumnName("TipoStatus")
                .HasMaxLength(10);

            // Si la combinación (IdConsulta, TipoDiagnostico) debe ser única:
            // builder.HasIndex(d => new { d.IdConsulta, d.TipoDiagnostico }).IsUnique();

            // Relación de FK
            builder.HasOne(d => d.Consulta)
                .WithMany(c => c.Diagnosticos)
                .HasForeignKey(d => d.ConsultaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}