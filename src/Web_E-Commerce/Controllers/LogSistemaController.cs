using Application.Interfaces.ISistemas;
using Infrastructure.Configurations.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Web_E_Commerce.Controllers
{
    public class LogSistemaController : Controller
    {
        private readonly ILogSistemaApp  _iLogSistemaApp;

        public LogSistemaController(ILogSistemaApp iLogSistemaApp)
        {
            _iLogSistemaApp = iLogSistemaApp;
        }

        // GET: LogSistema
        public async Task<IActionResult> Index()
        {
            return View(await _iLogSistemaApp.List());
        }

        // GET: LogSistema/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var logSistema = await _iLogSistemaApp.GetEntityById((int)id);
            if (logSistema == null)
            {
                return NotFound();
            }

            return View(logSistema);
        }
    }
}
