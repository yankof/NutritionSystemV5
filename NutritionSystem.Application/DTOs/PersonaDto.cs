using System;

namespace NutritionSystem.Application.DTOs
{
    public class PersonaDto
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string ApPaterno { get; set; }
        public string ApMaterno { get; set; }
        public string Mail { get; set; }
        public string CI { get; set; }
        public string Nit { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public DateOnly FechaCreacion { get; set; }
    }
}