using System.ComponentModel.DataAnnotations;


namespace TicketingSystem.DAL.Entities
{
    public class Tickets : Entity
    {
        [Display(Name = "UseDate")] //Fecha de uso de la boleta
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")] //LONGITUD MÁXIMA (NVARCHAR(50))
        [Required(ErrorMessage = "El campo {0} es obligatorio.")] //NOT NULL
        public string Name { get; set; }

        [Display(Name = "Estado")]
        public ICollection<State> States { get; set; } //Esto significa que un país puede tener N Estados    

        //Propiedad de Lectura que no se mapea en la tabla de la DB
        public int StatesNumber => States == null ? 0 : States.Count; // If Ternario (? Entonces), (: sino)
    }
}
