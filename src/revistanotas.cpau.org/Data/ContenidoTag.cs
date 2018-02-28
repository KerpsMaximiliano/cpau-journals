using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CPAU.RevistaNotas.Data
{
    [Table("ContenidoTags", Schema = "revistanotas")]
    public class ContenidoTag
    {
        [Key]
        public int Id { get; set; }

        public int ContenidoId { get; set; }

        public Contenido Contenido { get; set; }

        public int TagId { get; set; }

        public Tag Tag { get; set; }
    }
}
