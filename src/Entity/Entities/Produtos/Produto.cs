using Entity.Entities.Users;
using Entity.Notification;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Entities.Produtos
{
    [Table("Produto")]
    public class Produto : Notifica
    {
        [Column("ProdutoId")]
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Column("ProdutoNome")]
        [Display(Name = "Nome")]
        [MaxLength(255)]
        public string Nome { get; set; }

        [Column("ProdutoDescricao")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Column("ProdutoObservacao")]
        [Display(Name = "Observação")]
        [MaxLength(2000)]
        public string Observacao { get; set; }

        [Column("ProdutoValor")]
        [Display(Name = "Valor")]
        public decimal Valor { get; set; }

        [Column("ProdutoQtdEstoque")]
        [Display(Name = "Quantidade estoque")]
        public int QuantidadeEstoque { get; set; }
      
        [Column("ProdutoEstado")]
        [Display(Name = "Estado")]
        public bool Estado { get; set; }
        
        [Display(Name = "Usuário")]
        [ForeignKey("ApplicationUser")]
        [Column(Order = 1)]
        public string UserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        [Column("ProdutoDataCadastro")]
        [Display(Name = "Data do cadastro")]
        public DateTime DataCadastro { get; set; }
        

        [Column("ProdutoDataAlteracao")]
        [Display(Name = "Data de Alteração")]
        public DateTime DataAlteracao { get; set; }
       
    }
}
