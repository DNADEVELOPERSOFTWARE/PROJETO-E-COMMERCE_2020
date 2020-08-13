using Entity.Entities.Produtos;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Configurations.Context
{
    public class BaseContexto : IdentityDbContext
    {
        public BaseContexto(DbContextOptions<BaseContexto> options) : base(options)
        {

        }

        public DbSet<Produto> Produto { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //Utilizando o Banc de Dados SqlServer
                optionsBuilder.UseSqlServer(GetStringConnectionConfig());
                base.OnConfiguring(optionsBuilder);
            }

            ////Utilizando o Mysql
            //if (!optionsBuilder.IsConfigured)
            //{
            //    optionsBuilder.UseMySql(GetStringConnectionConfig());
            //    base.OnConfiguring(optionsBuilder);
            //}

        }
        private string GetStringConnectionConfig()
        {
            //String para o banco de dados SqlServer
            string strCon = "Data Source==(localdb)\\MSSQLLocalDB;Initial Catalog=E-Commercer2020;Integrated Security=False;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";

            ////String para o bando de dados Mysql
            // string strCon = "Data Source = localhost;port=3308;database=sDrinkShopDDD;user=root;password=toor";

            return strCon;
        }


    }
}
