using System.Collections.Generic;

namespace CPAU.RevistaNotas.Models.AutoresViewModel
{
    public class AutorViewModel
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string ShortBio { get; set; }

        public string Bio { get; set; }

        public string ImagenPerfil { get; set; }

        public List<RevistasViewModels.ContenidoAutorViewModel> Contenidos { get; set; }
    }
}
