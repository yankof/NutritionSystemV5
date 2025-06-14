namespace NutritionSystem.Infrastructure.Persistence.Configurations
{
    public class PersonaConfiguration : IEntityTypeConfiguration<Persona>
    {
        public void Configure(EntityTypeBuilder<Persona> builder)
        {
            builder.ToTable("Persona"); // Nombre de la tabla en la BD

            builder.HasKey(p => p.Id); // Clave primaria

            builder.Property(p => p.Id)
                .HasColumnName("Id")
                .IsRequired();

            builder.Property(p => p.Nombre)
                .HasColumnName("Nombre")
                .HasMaxLength(90)
                .IsRequired();

            builder.Property(p => p.ApPaterno)
                .HasColumnName("ApPaterno")
                .HasMaxLength(60)
                .IsRequired();

            builder.Property(p => p.ApMaterno)
                .HasColumnName("ApMaterno")
                .HasMaxLength(60); // Asumo que ApMaterno puede ser nulo

            builder.Property(p => p.Mail)
                .HasColumnName("Mail")
                .HasMaxLength(60);

            builder.Property(p => p.CI)
                .HasColumnName("CI")
                .HasMaxLength(15);

            builder.Property(p => p.Nit)
                .HasColumnName("Nit")
                .HasMaxLength(15);

            builder.Property(p => p.Direccion)
                .HasColumnName("Direccion")
                .HasMaxLength(60);

            builder.Property(p => p.Telefono)
                .HasColumnName("Telefono")
                .HasMaxLength(60);

            builder.Property(p => p.Celular)
                .HasColumnName("Celular")
                .HasMaxLength(60);

            builder.Property(p => p.FechaCreacion)
                .HasColumnName("FechaCreacion")
                .HasColumnType("date") // Mapea a tipo DATE en SQL Server
                .IsRequired();

            // Relación 1-a-1 con Nutricionista
            builder.HasOne(p => p.Nutricionista)
                .WithOne(n => n.Persona)
                .HasForeignKey<Nutricionista>(n => n.Id) // Id de Nutricionista es FK a Persona
                .IsRequired(false); // Puede que una Persona no sea Nutricionista

            // Relación 1-a-1 con Paciente
            builder.HasOne(p => p.Paciente)
                .WithOne(pa => pa.Persona)
                .HasForeignKey<Paciente>(pa => pa.Id) // Id de Paciente es FK a Persona
                .IsRequired(false); // Puede que una Persona no sea Paciente
        }
    }
}