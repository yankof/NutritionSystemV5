using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using NutritionSystem.Domain.Enums;

namespace NutritionSystem.Infrastructure.Persistence.Configurations
{
    public class PlanConfiguration : IEntityTypeConfiguration<Plan>
    {
        public void Configure(EntityTypeBuilder<Plan> builder)
        {
            builder.ToTable("Plan");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("Id")
                .IsRequired();
            builder.Ignore(x => x.TipoPlan);
            //var tipoPlanConverter = new ValueConverter<TipoPlan, string>(
            //    statusEnumValue => statusEnumValue.ToString(),
            //    tipoPlan => (TipoPlan)Enum.Parse(typeof(TipoPlan), tipoPlan)
            //);
            builder.Property(p => p.TipoPlanClave)
                .HasColumnName("TipoPlan")
                .HasConversion<string>();

            builder.Property(p => p.Descripcion)
                .HasColumnName("Descripcion")
                .HasMaxLength(200);

            builder.Property(p => p.DiasTratamiento)
                .HasColumnName("DiasTratamiento");

            builder.Property(p => p.ConsultaId)
                .HasColumnName("IdConsulta")
                .IsRequired();

            builder.Property(p => p.TipoStatus)
                .HasColumnName("tipoStatus")
                .HasConversion<string>()
                .HasMaxLength(10);

            builder.Property(p => p.IdContrato)
                .HasColumnName("IdContrato")
                .IsRequired();

            // Relación de FK
            builder.HasOne(p => p.Consulta)
                .WithMany(c => c.Planes)
                .HasForeignKey(p => p.ConsultaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}