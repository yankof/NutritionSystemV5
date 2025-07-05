using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace NutritionSystem.Domain.Entities
{
    public class Plan : EntityBase<Guid>
    {
        public Guid ConsultaId { get; private set; } // FK a Consulta
        public Consulta Consulta { get; private set; }


        public string TipoPlanClave { get; private set; }
        public string Descripcion { get; private set; }
        
        [NotMapped]
        public TipoPlan TipoPlan { get; private set; }
        public DateOnly FechaCreacion { get; private set; }
        public TipoStatus TipoStatus { get; private set; }
        public int DiasTratamiento { get; private set; }
        public Guid IdContrato { get; private set; }

        private Plan() { }

        public Plan(Guid id, Guid consultaId, string descripcion, TipoPlan tipoPlan, TipoStatus tipoStatus, int diasTratamiento, Guid idContrato)
        {
            Id = id; // Asigna el ID proporcionado o genera uno si es necesario
            ConsultaId = consultaId;
            Descripcion = descripcion;
            TipoPlanClave = ((int)tipoPlan).ToString();
            //TipoPlan = tipoPlan;
            FechaCreacion = DateOnly.FromDateTime(DateTime.UtcNow);
            DiasTratamiento = diasTratamiento;
            TipoStatus = tipoStatus;
            IdContrato = idContrato;

            //// REGISTRAR EL EVENTO DE DOMINIO:
            //AddDomainEvent(new PlanCreatedEvent(this.Id, this.ConsultaId, this.TipoPlan, this.Descripcion, this.TipoStatus, this.DiasTratamiento));

            //aqui hacemos el envio a rabbit
            PlanAlimentarioCreado planAlimentarioCreado = new PlanAlimentarioCreado(
                this.Id,
                this.Descripcion,
                TipoPlan.ToString(),
                DiasTratamiento,
                IdContrato
                );

            AddDomainEvent(planAlimentarioCreado);

            PlanAlimentarioAsignado planAlimentarioAsignado = new PlanAlimentarioAsignado(
                IdContrato,
                this.Id
                );
            AddDomainEvent(planAlimentarioAsignado);

        }

        public void UpdateDetails(string newDescripcion, TipoPlan newTipoPlan)
        {
            Descripcion = newDescripcion;
            TipoPlan = newTipoPlan;
            // Podrías añadir un PlanUpdatedEvent aquí
        }
    }
}