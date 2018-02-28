using CPAU.RevistaNotas.Models.AutoresViewModel;

namespace CPAU.RevistaNotas.Models.RevistasViewModels
{
    public class ContenidoAutorViewModel
    {
        public int Id { get; set; }

        public int ContenidoId { get; set; }

        public ContenidoViewModel Contenido { get; set; }

        public int AutorId { get; set; }

        public AutorViewModel Autor { get; set; }

        public int ListIndex { get; set; }
    }
}
