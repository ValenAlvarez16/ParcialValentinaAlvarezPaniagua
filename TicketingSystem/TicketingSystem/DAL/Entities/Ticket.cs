using System.ComponentModel.DataAnnotations;


namespace TicketingSystem.DAL.Entities
{
    public class Ticket : Entity
    {


        [Display(Name = "Use Date of the ticket")] //ASÍ ES COMO SE VA A MOSTRAR POR UI
        [Required(ErrorMessage = "El campo {0} es obligatorio.")] //NOT NULL
        public DateTime? UseDate { get; set;}

        [Display(Name = "It was used? ")] //ASÍ ES COMO SE VA A MOSTRAR POR UI
        [Required(ErrorMessage = "El campo {0} es obligatorio.")] //NOT NULL
        public bool IsUsed { get; set;}

        [Display(Name = "Porter's lodge")] //ASÍ ES COMO SE VA A MOSTRAR POR UI
        [Required(ErrorMessage = "El campo {0} es obligatorio.")] //NOT NULL
        public string EntranceGate { get; set;}

    }

}
