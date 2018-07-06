using System.Linq;
using AutoMapper;
using CPAU.RevistaNotas.Configuration;
using CPAU.RevistaNotas.Models.RevistasViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace CPAU.RevistaNotas.Controllers
{
    [Route("[controller]")]
    public class NotasController : Controller
    {
        private readonly Data.ApplicationDbContext context;
        private readonly RevistaNotasConfiguration revistaNotasConfiguration;
        public NotasController(
            Data.ApplicationDbContext context,
            IOptions<RevistaNotasConfiguration> revistaNotasConfiguration)
        {
            this.context = context;
            this.revistaNotasConfiguration = revistaNotasConfiguration.Value;
        }

        [HttpGet]
        public IActionResult Index()
        {

            var data = context.Contenidos
                .AsNoTracking()
                .Include(x => x.Autores)
                .ThenInclude(x => x.Autor)
                .Include(x => x.Tags)
                .ThenInclude(x => x.Tag)
                .Where(w => w.EsNota)
                .OrderByDescending(o => o.FechaAlta)
                .ToList();

            var tags = context.Tags
                .AsNoTracking()
                .Where(w => w.EsNota)
                .OrderBy(o => o.Nombre)
                .ToList();

            var model = new Models.NotasViewModels.IndexViewModel();
            model.Tags = tags;
            data.ForEach(x =>
            {
                var contenidoModel = Mapper.Map<Models.RevistasViewModels.ContenidoViewModel>(x);
                if (string.IsNullOrEmpty(contenidoModel.Imagen))
                {
                    contenidoModel.Imagen = revistaNotasConfiguration.ImagesRootUri + "/contenidos/default.jpg";
                }
                model.Items.Add(contenidoModel);
            });

            return View(model);
        }

        [HttpGet("{id}")]
        public IActionResult Nota(int id)
        {
            var data = context.Contenidos
                .AsNoTracking()
                .Include(x => x.Autores)
                .ThenInclude(x => x.Autor)
                .Include(x => x.Tags)
                .ThenInclude(x => x.Tag)
                .SingleOrDefault(w => w.Id == id);

            var model = Mapper.Map<ContenidoViewModel>(data);
            if (data != null)
            {
                var tags = data.Tags.Select(x => x.TagId).ToList();

                /*
                var relacionados = context.Contenidos
                    .AsNoTracking()
                    .Include(x => x.Autores)
                    .ThenInclude(x => x.Autor)
                    .Include(x => x.Tags)
                    .ThenInclude(x => x.Tag)
                    .Where(
                        w =>
                            w.Tipo == data.Tipo &&
                            w.Id != data.Id
                            && w.Tags.Any(tag => tags.Contains(tag.Id)))
                    .ToList();

                model.Relacionados = Mapper.Map<List<RevistaContenidoViewModel>>(relacionados);
                model.Relacionados.ForEach(x =>
                {
                    if (x.Contenido.ImagenMimeType.HasValue)
                    {
                        x.Contenido.UrlImagen = revistaNotasConfiguration.ImagesRootUri + "/contenidos/" +
                                                        x.Contenido.Id + "." +
                                                        MimeTypeUtils.ToString(x.Contenido.ImagenMimeType.Value);
                    }
                    else
                    {
                        x.Contenido.UrlImagen = revistaNotasConfiguration.ImagesRootUri + "/contenidos/default.jpg";
                    }
                });
                */
            }
            
            ViewBag.Title = model.Titulo;
            return View(model);
        }
    }
}
