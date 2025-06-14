namespace NutritionSystem.Infrastructure.Persistence.Configurations
{
    public class NutricionistaConfiguration : IEntityTypeConfiguration<Nutricionista>
    {
        public void Configure(EntityTypeBuilder<Nutricionista> builder)
        {
            builder.ToTable("Nutricionista");

            builder.HasKey(n => n.Id); // IdNutricionista en la BD

            builder.Property(n => n.Id)
                .HasColumnName("IdNutricionista")
                .IsRequired();

            builder.Property(n => n.Titulo)
                .HasColumnName("Titulo")
                .HasMaxLength(60);

            builder.Property(n => n.Turno)
                .HasColumnName("Turno")
                .HasMaxLength(60);

            builder.Property(n => n.Activo)
                .HasColumnName("Activo")
                .HasConversion<string>() // Mapea enum a string ('0' o '1')
                .HasMaxLength(1); // varchar(1)

            builder.Property(n => n.FechaCreacion)
                .HasColumnName("FechaCreacion")
                .HasColumnType("date");

            // Relación con Persona (la FK es la PK de Nutricionista)
            builder.HasOne(n => n.Persona)
                .WithOne(p => p.Nutricionista)
                .HasForeignKey<Nutricionista>(n => n.Id); // La FK es el Id de Nutricionista
        }
    }
}