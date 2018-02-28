using System.Collections.Generic;

namespace CPAU.RevistaNotas.Models.AutoresViewModel
{
    public class AutorViewModel : Data.Autor
    {
        public new List<RevistasViewModels.ContenidoAutorViewModel> Contenidos { get; set; }
    }
}
