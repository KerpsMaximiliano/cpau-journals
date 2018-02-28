using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CPAU.RevistaNotas.Data
{
    [Table("RevistaContenidos", Schema = "revistanotas")]
    public class RevistaContenido
    {
        [Key]
        public int Id { get; set; }

        public int RevistaId { get; set; }

        public Revista Revista { get; set; }

        public int ContenidoId { get; set; }

        public Contenido Contenido { get; set; }

        public int ListIndex { get; set; }
    }
}
