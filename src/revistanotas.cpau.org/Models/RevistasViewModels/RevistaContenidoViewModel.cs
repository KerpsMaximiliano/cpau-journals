using System.Collections.Generic;
using CPAU.RevistaNotas.Data;

namespace CPAU.RevistaNotas.Models.RevistasViewModels
{
    public class RevistaContenidoViewModel
    {
        public int Id { get; set; }

        public RevistaViewModel Revista { get; set; }

        public ContenidoViewModel Contenido { get; set; }

        public string Style { get; set; }

        public int ListIndex { get; set; }

        public List<RevistaContenidoViewModel> Relacionados { get; set; }

        public RevistaContenidoViewModel()
        {
            this.Relacionados = new List<RevistaContenidoViewModel>();
        }
    }
}
