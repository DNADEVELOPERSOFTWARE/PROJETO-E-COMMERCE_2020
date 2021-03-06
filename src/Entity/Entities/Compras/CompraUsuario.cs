﻿using Entity.Entities.Enuns;
using Entity.Entities.Produtos;
using Entity.Entities.Users;
using Entity.Notification;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace Entity.Entities.Compras
{
    [Table("CompraUsuario")]
    public class CompraUsuario : Notifica
    {
        [Column("CodigoUsuario")]
        [Display(Name = "Código do usuário")]
        public int Id { get; set; }

        [Column("CompraUsuarioEstado")]
        [Display(Name = "Estado")]
        public EstadoCompra Estado { get; set; }

        [Column("CompraUsuarioQntde")]
        [Display(Name = "Quantidade")]
        public int QuantidadeCompra { get; set; }

        [NotMapped]
        [Display(Name = "Quantidade Total")]
        public int QuantidadeProduto { get; set; }

        [NotMapped]
        [Display(Name = "Valor Total")]
        public decimal ValorTotal { get; set; }

        [NotMapped]
        [Display(Name = "Endereço de Entrega")]
        public string EnderecoCompleto { get; set; }

        [NotMapped]
        public List<Produto> ListaProdutos { get; set; }

        //======------Chaves estrangeiras------======//

        [Display(Name = "Produto")]
        [ForeignKey("Produto")]
        [Column(Order = 1)]
        public int ProdutoId { get; set; }

        public virtual Produto Produto { get; set; }

        [Display(Name = "Usuário")]
        [ForeignKey("ApplicationUser")]
        [Column(Order = 1)]
        public string UserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }

        [Display(Name = "Compra")]
        [ForeignKey("COMPRA")]
        [Column(Order = 1)]
        public int CompraId { get; set; }

        public virtual Compra Compra { get; set; }

    }
}
