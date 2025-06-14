namespace NutritionSystem.Infrastructure.Persistence.Configurations
{
    public class ReservaConfiguration : IEntityTypeConfiguration<Reserva>
    {
        public void Configure(EntityTypeBuilder<Reserva> builder)
        {
            builder.ToTable("Reserva");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id)
                .HasColumnName("Id")
                .IsRequired();

            builder.Property(r => r.FechaCreacion)
                .HasColumnName("FechaCreacion")
                .HasColumnType("date");

            builder.Property(r => r.HoraCreacion)
                .HasColumnName("HoraCreacion")
                .HasMaxLength(10);

            builder.Property(r => r.Activo)
                .HasColumnName("Activo")
                .HasConversion<string>()
                .HasMaxLength(1);

            builder.Property(r => r.FechaModificacion)
                .HasColumnName("FechaModificacion")
                .HasColumnType("date");

            builder.Property(r => r.HoraModificacion)
                .HasColumnName("HoraModificacion")
                .HasMaxLength(10);

            //// FKs lógicas que asumimos para la Reserva
            //builder.Property(r => r.IdNutricionista)
            //    .HasColumnName("IdNutricionista")
            //    .IsRequired(false); // Asumo que no es NOT NULL en la BD si no hay FK explícita

            //builder.Property(r => r.IdPacient)
            //    .HasColumnName("IdPacient")
            //    .IsRequired(false); // Asumo que no es NOT NULL en la BD si no hay FK explícita

            //builder.HasOne(r => r.Nutricionista)
            //    .WithMany()
            //    .HasForeignKey(r => r.IdNutricionista)
            //    .IsRequired(false)
            //    .OnDelete(DeleteBehavior.Restrict);

            //builder.HasOne(r => r.Paciente)
            //    .WithMany()
            //    .HasForeignKey(r => r.IdPacient)
            //    .IsRequired(false)
            //    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}