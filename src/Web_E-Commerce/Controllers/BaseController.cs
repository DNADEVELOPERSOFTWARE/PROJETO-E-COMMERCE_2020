using Application.Interfaces.ISistemas;
using Entity.Entities.Enuns;
using Entity.Entities.Sistema;
using Entity.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Web_E_Commerce.Controllers
{
    public class BaseController : Controller
    {
        private readonly ILogger<BaseController> logger;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly ILogSistemaApp _iLogSistemaApp;

        public BaseController(ILogger<BaseController> logger, UserManager<ApplicationUser> userManager, ILogSistemaApp iLogSistemaApp)
        {
            this.logger = logger;
            this._userManager = userManager;
            this._iLogSistemaApp = iLogSistemaApp;

        }

        public async Task<string> RetornarIdUsuarioLogado()
        {
            var idUsuario = await _userManager.GetUserAsync(User);

            return idUsuario.Id;
        }

        public async Task LogEcommerce(TipoLog tipoLog, object objeto)
        {
            string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();

            await _iLogSistemaApp.Add(new LogSistema
            {
                TipoLog = tipoLog,
                JsonInformacao = JsonConvert.SerializeObject(objeto),
                UserId = await RetornarIdUsuarioLogado(),
                NomeAction = actionName,
                NomeController = controllerName

            });
        }
    }
}
