using Entity.Notification;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Entities.Produtos
{
    [Table("Produto")]
    public class Produto : Notifica
    {
        [Column("PRODUTOID")]
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Column("PRODUTONOME")]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Column("PRODUTOVALOR")]
        [Display(Name = "Valor")]
        public decimal Valor { get; set; }

        [Column("PRODUTOESTADO")]
        [Display(Name = "Estado")]
        public bool Estado { get; set; }
    }
}
