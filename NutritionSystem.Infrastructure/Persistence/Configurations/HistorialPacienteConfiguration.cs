namespace NutritionSystem.Infrastructure.Persistence.Configurations
{
    public class HistorialPacienteConfiguration : IEntityTypeConfiguration<HistorialPaciente>
    {
        public void Configure(EntityTypeBuilder<HistorialPaciente> builder)
        {
            builder.ToTable("HistorialPaciente");

            builder.HasKey(hp => hp.Id);

            builder.Property(hp => hp.Id)
                .HasColumnName("IdHistorial")
                .IsRequired();

            builder.Property(hp => hp.IdPaciente)
                .HasColumnName("IdPaciente"); // No es IsRequired en SQL, pero debería serlo lógicamente

            builder.Property(hp => hp.IdEvaluacion)
                .HasColumnName("IdEvaluacion"); // No es IsRequired en SQL, pero debería serlo lógicamente

            builder.Property(hp => hp.FechaCreacion)
                .HasColumnName("FechaCreacion")
                .HasColumnType("date");

            builder.Property(hp => hp.Valor)
                .HasColumnName("Valor")
                .HasMaxLength(1); // nvarchar(1)

            builder.Property(hp => hp.Resultado)
                .HasColumnName("Resultado")
                .HasMaxLength(200);

            // Relaciones de FK (asumiendo que IdPaciente e IdEvaluacion son FKs)
            builder.HasOne(hp => hp.Paciente)
                .WithMany() // No hay colección de HistorialPaciente en Paciente por ahora
                .HasForeignKey(hp => hp.IdPaciente)
                .IsRequired(false) // Si la columna SQL no es NOT NULL
                .OnDelete(DeleteBehavior.Restrict);

            //builder.HasOne(hp => hp.Evaluacion)
            //    .WithMany(e => e.HistorialesPaciente)
            //    .HasForeignKey(hp => hp.IdEvaluacion)
            //    .IsRequired(false) // Si la columna SQL no es NOT NULL
            //    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}