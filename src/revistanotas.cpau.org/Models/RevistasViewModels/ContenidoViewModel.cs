using System;
using System.Collections.Generic;
using CPAU.RevistaNotas.Data;

namespace CPAU.RevistaNotas.Models.RevistasViewModels
{
    public class ContenidoViewModel
    {
        public int Id { get; set; }

        public bool EsNota { get; set; }

        public string Titulo { get; set; }

        public string TituloSe { get; set; }

        public Guid Guid { get; set; }

        public string Texto { get; set; }

        public string Url { get; set; }

        public byte UrlTarget { get; set; }

        public byte Tipo { get; set; }

        public DateTime FechaAlta { get; set; }

        public DateTime FechaUltimaModificacion { get; set; }

        public string Imagen { get; set; }

        public List<ContenidoAutorViewModel> Autores { get; set; }

        public List<Data.RevistaContenido> Revistas { get; set; }

        public List<ContenidoTag> Tags { get; set; }

        public ContenidoViewModel()
        {
            this.Autores = new List<ContenidoAutorViewModel>();
            this.Tags = new List<ContenidoTag>();
        }
    }
}
