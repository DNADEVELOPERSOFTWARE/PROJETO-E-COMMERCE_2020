using Entity.Notification;
using System.ComponentModel.DataAnnotations;

namespace Entity.Entities
{
    public class Base : Notifica
    {
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Display(Name = "Nome")]
        public int Nome { get; set; }
    }
}
