namespace NutritionSystem.Infrastructure.Persistence.Configurations
{
    public class EvaluacionConfiguration : IEntityTypeConfiguration<Evaluacion>
    {
        public void Configure(EntityTypeBuilder<Evaluacion> builder)
        {
            builder.ToTable("Evaluacion");

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                .HasColumnName("IdEvaluacion")
                .IsRequired();

            builder.Property(e => e.Descripcion)
                .HasColumnName("Descripcion")
                .HasMaxLength(255);

            builder.Property(e => e.TipoEvaluacion)
                .HasColumnName("TipoEvaluacion")
                .HasConversion<string>()
                .HasMaxLength(5); // varchar(5)

            builder.Property(e => e.FechaCreacion)
                .HasColumnName("FechaCreacion")
                .HasColumnType("date");

            //builder.Property(e => e.HoraCreacion)
            //    .HasColumnName("HoraCreacion")
            //    .HasMaxLength(10);

            builder.Property(e => e.Activo)
                .HasColumnName("Activo")
                .HasConversion<string>()
                .HasMaxLength(1);

            builder.Property(e => e.ConsultaId)
                .HasColumnName("IdConsulta")
                .IsRequired();

            // Relación de FK
            builder.HasOne(e => e.Consulta)
                .WithMany(c => c.Evaluaciones)
                .HasForeignKey(e => e.ConsultaId)
                .OnDelete(DeleteBehavior.Restrict); // Opcional: Restrict si no quieres borrado en cascada
        }
    }
}