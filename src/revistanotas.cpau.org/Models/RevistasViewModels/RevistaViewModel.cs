using System.Collections.Generic;
using System.Runtime.Serialization;
using CPAU.RevistaNotas.Data;

namespace CPAU.RevistaNotas.Models.RevistasViewModels
{
    public class RevistaViewModel
    {
        public int Id { get; set; }

        public string Titulo { get; set; }

        public string Numero { get; set; }

        public string Descripcion { get; set; }

        public byte Tipo { get; set; }

        public string ImagenTapa { get; set; }

        public string Html { get; set; }

        public string Style { get; set; }
        public string Color1 { get; set; }

        public string Url { get; set; }

        public string IssuUrl { get; set; }

        public bool IsPublished { get; set; }

        public List<RevistaContenidoViewModel> Contenidos { get; set; }

        public List<Tag> Tags { get; set; }

        public RevistaViewModel()
        {
            this.Contenidos = new List<RevistaContenidoViewModel>();
            this.Tags = new List<Tag>();
        }
    }
}
