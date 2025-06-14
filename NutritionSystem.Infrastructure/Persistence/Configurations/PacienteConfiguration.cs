namespace NutritionSystem.Infrastructure.Persistence.Configurations
{
    public class PacienteConfiguration : IEntityTypeConfiguration<Paciente>
    {
        public void Configure(EntityTypeBuilder<Paciente> builder)
        {
            builder.ToTable("Pacient"); // Nombre de la tabla en la BD

            builder.HasKey(p => p.Id); // IdPacient en la BD

            builder.Property(p => p.Id)
                .HasColumnName("IdPacient")
                .IsRequired();

            builder.Property(p => p.Activo)
                .HasColumnName("Activo")
                .HasConversion<string>() // Mapea enum a string ('0' o '1')
                .HasMaxLength(1); // varchar(1)

            builder.Property(p => p.FechaCreacion)
                .HasColumnName("FechaCreacion")
                .HasColumnType("date");

            // Relación con Persona (la FK es la PK de Paciente)
            builder.HasOne(pa => pa.Persona)
                .WithOne(p => p.Paciente)
                .HasForeignKey<Paciente>(pa => pa.Id); // La FK es el Id de Paciente
        }
    }
}