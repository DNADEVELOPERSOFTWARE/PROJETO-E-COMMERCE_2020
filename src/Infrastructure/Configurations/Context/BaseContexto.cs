﻿using Entity.Entities.Compras;
using Entity.Entities.Produtos;
using Entity.Entities.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Configurations.Context
{
    public class BaseContexto : IdentityDbContext<ApplicationUser>
    {
        public BaseContexto(DbContextOptions<BaseContexto> options) : base(options)
        {

        }

        public DbSet<Produto> Produto { get; set; }
        public DbSet<CompraUsuario> CompraUsuario { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            ////Utilizando o Banc de Dados SqlServer
            //if (!optionsBuilder.IsConfigured)
            //{  
            //    optionsBuilder.UseSqlServer(GetStringConnectionConfig());
            //    base.OnConfiguring(optionsBuilder);
            //}

            //Utilizando o Mysql
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(GetStringConnectionConfig());
                base.OnConfiguring(optionsBuilder);
            }

        }
        private string GetStringConnectionConfig()
        {
            //String para o banco de dados SqlServer
            //string strCon = "Data Source==(localdb)\\MSSQLLocalDB;Initial Catalog=E-Commercer2020;Integrated Security=False;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False";

            ////String para o bando de dados Mysql
            string strCon = "Data Source = localhost;port=3308;database=E-Commerce_1_2020;user=root;password=toor";

            return strCon;
        }


    }
}
