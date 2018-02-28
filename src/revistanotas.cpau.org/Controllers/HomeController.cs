using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CPAU.RevistaNotas.Controllers
{
    public class HomeController : Controller
    {
        private readonly Data.ApplicationDbContext context;

        public HomeController(Data.ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            ViewBag.Title = "Revista Notas";
            return View();
        }

        public IActionResult CPAU()
        {
            var data = context.Contenidos
                .AsNoTracking()
                .SingleOrDefault(w => w.Id == 1244);

            if (data != null)
            {
                var model = new Models.HomeViewModels.CpauViewModel { Texto = data.Texto };
                return View(model);
            }

            return View(null);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
