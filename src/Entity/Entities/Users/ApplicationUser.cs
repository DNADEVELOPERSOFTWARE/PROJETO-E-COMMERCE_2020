using Entity.Entities.Enuns;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Entities.Users
{

    public class ApplicationUser : IdentityUser<string>
    {
        [Column("CPF")]
        [MaxLength(50)]
        [Display(Name = "CPF/CNPJ")]
        public string CPF { get; set; }

        [Column("Idade")]
        [Display(Name = "Idade")]
        public int Idade { get; set; }

        [Column("Nome")]
        [MaxLength(255)]
        [Display(Name = "Nome")]
        public string Nome { get; set; }

        [Column("CEP")]
        [MaxLength(15)]
        [Display(Name = "CEP")]
        public string CEP { get; set; }

        [Column("Endereco")]
        [MaxLength(255)]
        [Display(Name = "Endereço")]
        public string Endereco { get; set; }

        [Column("ComplementoEndereco")]
        [MaxLength(450)]
        [Display(Name = "Complemento de Endereço")]
        public string ComplementoEndereco { get; set; }

        [Column("Celular")]
        [MaxLength(20)]
        [Display(Name = "Celular")]
        public string Celular { get; set; }

        [Column("Telefone")]
        [MaxLength(20)]
        [Display(Name = "Telefone fixo")]
        public string Telefone { get; set; }

        [Column("Estado")]
        [Display(Name = "Estado")]
        public bool Estado { get; set; }

        [Column("Tipo")]
        [Display(Name = "Tipo usúario")]
        public TipoUsuario? Tipo { get; set; }
    }
}
