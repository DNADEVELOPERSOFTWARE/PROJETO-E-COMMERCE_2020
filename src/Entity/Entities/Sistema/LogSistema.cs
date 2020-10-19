using Entity.Entities.Enuns;
using Entity.Entities.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entity.Entities.Sistema
{
    [Table("LogSistema")]
    public class LogSistema : Base
    {
        [Column("logInformacaoJson")]
        [Display(Name ="Informações Json")]
        public string JsonInformacao { get; set; }

        [Column("TipoLog")]
        [Display(Name = "Tipo de log")]
        public TipoLog TipoLog { get; set; }

        [Column("Controller")]
        [Display(Name = "Nome do controlador")]
        public string NomeController { get; set; }

        [Column("Action")]
        [Display(Name = "Nome da Action")]
        public string NomeAction { get; set; }

        //======------Chaves estrangeiras------======//

        [Display(Name = "Usuario")]
        [ForeignKey("ApplicationUser")]
        [Column(Order = 1)]
        public string UserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
