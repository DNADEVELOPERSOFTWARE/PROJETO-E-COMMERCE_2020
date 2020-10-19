using Application.Interfaces.IComprasApps;
using Application.Interfaces.IProduto;
using Application.OpenApp.AppCompras;
using Application.OpenApp.AppProdutos;
using Domain.Interfaces.Generic;
using Domain.Interfaces.InterfaceCompra;
using Domain.Interfaces.InterfaceProduto;
using Domain.Interfaces.InterfaceServico;
using Domain.Services.ServiceCompras;
using Domain.Services.ServiceProdutos;
using Entity.Entities.Users;
using Infrastructure.Configurations.Context;
using Infrastructure.Repository.Generic;
using Infrastructure.Repository.Repositories.Produtos;
using Infrastructure.Repository.Repositories.Compras;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Domain.Interfaces.InterfaceSistema.LogsSitema;
using Infrastructure.Repository.Repositories.Sistemas;
using Application.OpenApp.AppSistemas;
using Application.Interfaces.ISistemas;

namespace Web_E_Commerce
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //COnxão Com Sql Server
            services.AddDbContext<BaseContexto>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<BaseContexto>();

            ////Conexão com Mysql
            //services.AddDbContext<BaseContexto>(options =>
            //    options.UseMySql(
            //        Configuration.GetConnectionString("DefaultConnection")));
            //services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<BaseContexto>();

            // INTERFACES E REPOSITÓRIOS
            services.AddSingleton(typeof(IGenerica<>), typeof(RepositorioGenerico<>));
            services.AddSingleton<IProduto, RepositorioProduto>();
            services.AddSingleton<ICompraUsuario, RepositorioCompraUsuario>();
            services.AddSingleton<ICompra, RepositorioCompra>();
            services.AddSingleton<ILogSistema, RepositorioLogSistema>();

            // INTERFACES DA APPLICATION
            services.AddSingleton<InterfaceProdutoApp, AppProduto>();
            services.AddSingleton<ICompraUsuarioApp, AppCompraUsuario>();
            services.AddSingleton<ICompra, RepositorioCompra>();
            services.AddSingleton<ICompraApp, AppCompra>();
            services.AddSingleton<ILogSistemaApp, AppLogSistema>();

            //SERVIÇOS DO DOMINIO
            services.AddSingleton<IServiceProduto, ServiceProduto>();
            services.AddSingleton<IServiceCompraUsuario, ServiceCompraUuuario>();

            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
