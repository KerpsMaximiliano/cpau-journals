using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CPAU.RevistaNotas.Data
{
    [Table("ContenidoAutores", Schema = "revistanotas")]
    public class ContenidoAutor
    {
        [Key]
        public int Id { get; set; }

        public int ContenidoId { get; set; }

        public Contenido Contenido { get; set; }

        public int AutorId { get; set; }

        public Autor Autor { get; set; }

        public int ListIndex { get; set; }
    }
}
