namespace NutritionSystem.Infrastructure.Persistence.Configurations
{
    public class ConsultaConfiguration : IEntityTypeConfiguration<Consulta>
    {
        public void Configure(EntityTypeBuilder<Consulta> builder)
        {
            builder.ToTable("Consulta");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasColumnName("Id")
                .IsRequired();

            builder.Property(c => c.Descripcion)
                .HasColumnName("Descripcion")
                .HasMaxLength(50);

            builder.Property(c => c.IdNutricionista)
                .HasColumnName("IdNutricionista")
                .IsRequired();

            builder.Property(c => c.IdPacient)
                .HasColumnName("IdPacient")
                .IsRequired();

            builder.Property(c => c.FechaCreacion)
                .HasColumnName("FechaCreacion")
                .HasColumnType("date");

            builder.Property(c => c.Estatus)
                .HasColumnName("Estatus")
                .HasConversion<string>()
                .HasMaxLength(1);

            // Relaciones de FK
            builder.HasOne(c => c.Nutricionista)
                .WithMany() // No hay colección de Consultas en Nutricionista por ahora
                .HasForeignKey(c => c.IdNutricionista)
                .OnDelete(DeleteBehavior.Restrict); // Evita borrado en cascada por defecto

            builder.HasOne(c => c.Paciente)
                .WithMany() // No hay colección de Consultas en Paciente por ahora
                .HasForeignKey(c => c.IdPacient)
                .OnDelete(DeleteBehavior.Restrict);

            // Relaciones con colecciones (Evaluaciones, Diagnosticos, Planes)
            builder.HasMany(c => c.Evaluaciones)
                .WithOne(e => e.Consulta)
                .HasForeignKey(e => e.ConsultaId)
                .OnDelete(DeleteBehavior.Cascade); // Si se borra una consulta, se borran sus evaluaciones

            builder.HasMany(c => c.Diagnosticos)
                .WithOne(d => d.Consulta)
                .HasForeignKey(d => d.ConsultaId)
                .OnDelete(DeleteBehavior.Cascade); // Si se borra una consulta, se borran sus diagnosticos

            builder.HasMany(c => c.Planes)
                .WithOne(p => p.Consulta)
                .HasForeignKey(p => p.ConsultaId)
                .OnDelete(DeleteBehavior.Cascade); // Si se borra una consulta, se borran sus planes
        }
    }
}