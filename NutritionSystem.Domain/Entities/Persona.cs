namespace NutritionSystem.Domain.Entities
{
    public class Persona : EntityBase<Guid>
    {
        public string Nombre { get; private set; }
        public string ApPaterno { get; private set; }
        public string ApMaterno { get; private set; }
        public string Mail { get; private set; }
        public string CI { get; private set; }
        public string Nit { get; private set; }
        public string Direccion { get; private set; }
        public string Telefono { get; private set; }
        public string Celular { get; private set; }
        public DateOnly FechaCreacion { get; private set; } // Usando DateOnly

        // Propiedades de navegación (relaciones)
        public Nutricionista Nutricionista { get; private set; }
        public Paciente Paciente { get; private set; }

        // Constructor para Entity Framework Core (requerido por EF Core para la deserialización)
        private Persona() { }

        // Constructor para crear una nueva Persona (establece el estado inicial y genera el ID)
        public Persona(Guid id, string nombre, string apPaterno, string apMaterno, string mail, string ci, string nit, 
            string direccion, string telefono, string celular)
        {
            Id = id == Guid.Empty ? Guid.NewGuid() : id;
            Nombre = nombre;
            ApPaterno = apPaterno;
            ApMaterno = apMaterno;
            Mail = mail;
            CI = ci;
            Nit = nit;
            Direccion = direccion;
            Telefono = telefono;
            Celular = celular;
            FechaCreacion = DateOnly.FromDateTime(DateTime.UtcNow);
        }

        // Método para actualizar el estado de la entidad (reglas de negocio)
        public void Update(string nombre, string apPaterno, string apMaterno, string mail, string ci, string nit, string direccion, string telefono, string celular)
        {
            // Aquí puedes agregar validaciones de dominio si es necesario
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(apPaterno))
            {
                throw new ArgumentException("El nombre y el apellido paterno son obligatorios.");
            }

            Nombre = nombre;
            ApPaterno = apPaterno;
            ApMaterno = apMaterno;
            Mail = mail;
            CI = ci;
            Nit = nit;
            Direccion = direccion;
            Telefono = telefono;
            Celular = celular;
            // FechaCreacion no se actualiza, es la fecha de creación original.
        }
    }
}