using Entity.Entities.Enuns;
using Entity.Entities.Users;
using Entity.Notification;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Entities.Compras
{
    [Table("Compra")]
    public class Compra : Notifica
    {
        [Column("ID")]
        [Display(Name = "Codigo")]
        public int Id { get; set; }

        [Column("ESTADO")]
        [Display(Name = "Estado")]
        public EstadoCompra Estado { get; set; }

        [Column("DATACOMPRA")]
        [Display(Name = "Data da Compra")]
        public DateTime DataCompra { get; set; }

        //======------Chaves estrangeiras------======//

        [Display(Name = "Usuario")]
        [ForeignKey("ApplicationUser")]
        [Column(Order = 1)]
        public string UserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

    }
}
