using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CPAU.RevistaNotas.Models.AutoresViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CPAU.RevistaNotas.Data;

namespace CPAU.RevistaNotas.Controllers
{
    [Route("[controller]")]
    public class AutoresController : Controller
    {
        private readonly Data.ApplicationDbContext context;
        public AutoresController(Data.ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var data = context.Autores
                .AsNoTracking()
                .OrderBy(x => x.Nombre)
                .ToList();

            var model = new AutoresViewModel
            {
                Autores = Mapper.Map<List<AutorViewModel>>(data)
            };
            return View(model);
        }

        [HttpGet("{id}")]
        public IActionResult Autor(int id)
        {
            var data = context.Autores
                .AsNoTracking()
                .Include(x => x.Contenidos)
                .ThenInclude(x => x.Contenido)
                .ThenInclude(x => x.Revistas)
                .ThenInclude(x => x.Revista)
                .SingleOrDefault(w => w.Id == id);
            var model = Mapper.Map<AutorViewModel>(data);
            return View(model);
        }
    }
}
