using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CPAU.RevistaNotas.Data
{
    [Table("Contenidos", Schema = "revistanotas")]
    public class Contenido
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public bool EsNota { get; set; }

        public string Titulo { get; set; }

        //public string TituloSe { get; set; }

        //public Guid Guid { get; set; }

        public string Texto { get; set; }

        public string Url { get; set; }

        public byte UrlTarget { get; set; }

        public byte Tipo { get; set; }

        public string Imagen { get; set; }

        public DateTime FechaAlta { get; set; }

        public DateTime FechaUltimaModificacion { get; set; }

        public List<Data.ContenidoAutor> Autores { get; set; }

        public List<Data.ContenidoTag> Tags { get; set; }

        public List<Data.RevistaContenido> Revistas { get; set; }
    }
}
