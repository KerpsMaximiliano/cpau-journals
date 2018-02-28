using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CPAU.RevistaNotas.Data
{
    [Table("Revistas", Schema = "revistanotas")]
    public class Revista
    {
        [Key]
        public int Id { get; set; }

        public string Titulo { get; set; }

        public string Numero { get; set; }

        public string Descripcion { get; set; }

        public byte Tipo { get; set; }

        public string ImagenTapa { get; set; }

        public int ListIndex { get; set; }

        public string Html { get; set; }

        public string Color1 { get; set; }

        public string Url { get; set; }

        public string IssuUrl { get; set; }

        public bool IsPublished { get; set; }

        public List<RevistaContenido> Items { get; set; }
    }
}
