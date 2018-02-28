using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AutoMapper;
using CPAU.RevistaNotas.Configuration;
using CPAU.RevistaNotas.Models.RevistasViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CPAU.RevistaNotas.Controllers
{
    public class RevistasController : Controller
    {
        private readonly IHostingEnvironment environment;
        private readonly Data.ApplicationDbContext context;
        private readonly RevistaNotasConfiguration revistaNotasConfiguration;

        public RevistasController(
            IHostingEnvironment environment,
            Data.ApplicationDbContext context,
            IOptions<RevistaNotasConfiguration> revistaNotasConfiguration)
        {
            this.environment = environment;
            this.context = context;
            this.revistaNotasConfiguration = revistaNotasConfiguration.Value;
        }

        [HttpGet("revistas")]
        public IActionResult Index()
        {
            var data = context.Revistas
                .AsNoTracking()
                .Where(w => w.IsPublished)
                .OrderByDescending(x => x.ListIndex)
                .ToList();

            var model = new IndexViewModel
            {
                Items = Mapper.Map<List<RevistaViewModel>>(data)
            };
            return View(model);
        }

        [HttpGet("revistas/{numero}")]
        public IActionResult Revista(string numero)
        {
            var data = context.Revistas
                .AsNoTracking()
                .Include(x => x.Items)
                .ThenInclude(x => x.Contenido)
                .ThenInclude(x => x.Autores)
                .ThenInclude(x => x.Autor)
                .Include(x => x.Items)
                .ThenInclude(x => x.Contenido)
                .ThenInclude(x => x.Tags)
                .ThenInclude(x => x.Tag)
                .SingleOrDefault(w => w.Tipo == 0 && w.Numero == numero);

            var tags = context.Tags
                .AsNoTracking()
                .Where(w => !w.EsNota)
                .ToList();

            var model = Mapper.Map<RevistaViewModel>(data);
            model.Tags = tags;
            var color = model.Color1 ?? Defaults.BackgroundColor;
            if (!string.IsNullOrEmpty(model.Html))
            {
                model.Html = model.Html.Replace("{$color1}", color);
            }

            foreach (var contenido in model.Contenidos)
            {
                if (string.IsNullOrEmpty(contenido.Contenido.Imagen))
                {
                    contenido.Contenido.Imagen = revistaNotasConfiguration.ImagesRootUri + "/contenidos/default.jpg";
                }
            }

            var cssPath = Path.Combine(environment.WebRootPath, "css", "colorvariable.css");
            var style = System.IO.File.ReadAllText(cssPath, Encoding.UTF8);

            if (string.IsNullOrEmpty(style))
            {
                throw new Exception("Revista css file not found");
            }

            style = style.Replace("ed008c", color.Replace("#", string.Empty));
            model.Style = style;

            ViewBag.Title = model.Titulo;

            model.Contenidos = model.Contenidos.OrderBy(o => o.ListIndex).ToList();
            return View(model);
        }

        [HttpGet("revistas/cpauinfo/{numero}")]
        public IActionResult CPAUInfo(string numero)
        {
            var data = context.Revistas
                .AsNoTracking()
                .Include(x => x.Items)
                .ThenInclude(x => x.Contenido)
                .ThenInclude(x => x.Autores)
                .ThenInclude(x => x.Autor)
                .SingleOrDefault(w => w.Tipo == 1 && w.Numero == numero);

            var model = Mapper.Map<RevistaViewModel>(data);
            return View(model);
        }

        [HttpGet("revistas/{numero}/{codigoContenido}")]
        public IActionResult Contenido(string numero, string codigoContenido)
        {
            int id;
            if (!int.TryParse(codigoContenido.Substring(0, codigoContenido.IndexOf('-')), out id))
            {
                throw new Exception("Código de contenido inválido");
            }

            var data = context.RevistaContenidos
                .AsNoTracking()
                .Include(x => x.Revista)
                .Include(x => x.Contenido)
                .ThenInclude(x => x.Autores)
                .ThenInclude(x => x.Autor)
                .Include(x => x.Contenido)
                .ThenInclude(x => x.Tags)
                .ThenInclude(x => x.Tag)
                .SingleOrDefault(w => w.Revista.Tipo == 0 && w.Revista.Numero == numero && w.ContenidoId == id);

            var model = Mapper.Map<RevistaContenidoViewModel>(data);
            if (data != null)
            {
                var tags = data.Contenido.Tags.Select(x => x.TagId).ToList();

                var relacionados = context.RevistaContenidos
                    .AsNoTracking()
                    .Include(x => x.Revista)
                    .Include(x => x.Contenido)
                    .ThenInclude(x => x.Autores)
                    .ThenInclude(x => x.Autor)
                    .Include(x => x.Contenido)
                    .ThenInclude(x => x.Tags)
                    .ThenInclude(x => x.Tag)
                    .Where(w => w.Revista.Numero == numero)
                    .ToList();

                foreach (var relacionado in relacionados)
                {
                    if (relacionado.Contenido.Tags.Select(x => x.TagId).Intersect(tags).Any())
                    {
                        var relacionadoModel = Mapper.Map<RevistaContenidoViewModel>(relacionado);
                        if (string.IsNullOrEmpty(relacionadoModel.Contenido.Imagen))
                        {
                            relacionadoModel.Contenido.Imagen = revistaNotasConfiguration.ImagesRootUri + "/contenidos/default.jpg";
                        }
                        model.Relacionados.Add(relacionadoModel);
                    }
                }
            }

            var cssPath = Path.Combine(environment.WebRootPath, "css", "colorvariable.css");
            var style = System.IO.File.ReadAllText(cssPath, Encoding.UTF8);

            if (string.IsNullOrEmpty(style))
            {
                throw new Exception("Revista css file not found");
            }

            var color = model.Revista.Color1 ?? Configuration.Defaults.BackgroundColor;
            style = style.Replace("ed008c", color.Replace("#", string.Empty));
            model.Style = style;

            ViewBag.Title = model.Contenido.Titulo;
            return View(model);
        }
    }
}
