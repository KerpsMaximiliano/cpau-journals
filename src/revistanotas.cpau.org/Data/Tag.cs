using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CPAU.RevistaNotas.Data
{
    [Table("Tags", Schema = "revistanotas")]
    public class Tag
    {
        [Key]
        public int Id { get; set; }

        public bool EsNota { get; set; }

        public string Nombre { get; set; }
    }
}
