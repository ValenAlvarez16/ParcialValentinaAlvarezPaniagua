using System.ComponentModel.DataAnnotations;


namespace TicketingSystem.DAL.Entities
{
    public class Ticket : Entity
    {


        [Display(Name = "Use Date of the ticket")] //show by UI
        public DateTime? UseDate { get; set;}

        [Display(Name = "It was used? ")] //show by UI
        [Required(ErrorMessage = "El campo {0} es obligatorio.")] //NOT NULL
        public bool IsUsed { get; set;}

        [Display(Name = "Porter's lodge")] //show by UI
        [MaxLength(10, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres.")]
        public string EntranceGate { get; set;}

    }

}
