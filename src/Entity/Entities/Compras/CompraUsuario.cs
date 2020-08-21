using Entity.Entities.Enuns;
using Entity.Entities.Produtos;
using Entity.Entities.Users;
using Entity.Notification;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Entities.Compras
{
    [Table("CompraUsuario")]
    public class CompraUsuario : Notifica
    {
        [Column("CodigoUsuario")]
        [Display(Name = "Código do usuário")]
        public int Id { get; set; }

        [Display(Name = "Produto")]
        [ForeignKey("Produto")]
        [Column(Order = 1)]
        public int ProdutoId { get; set; }

        public virtual Produto Produto { get; set; }

        [Column("CompraUsuarioEstado")]
        [Display(Name = "Estado")]
        public EstadoCompra Estado { get; set; }

        [Column("CompraUsuarioQntde")]
        [Display(Name = "Quantidade")]
        public int QuantidadeCompra { get; set; }

        [Display(Name = "Usuário")]
        [ForeignKey("ApplicationUser")]
        [Column(Order = 1)]
        public string UserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
