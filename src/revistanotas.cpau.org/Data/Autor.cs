using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CPAU.RevistaNotas.Data
{
    [Table("Autores", Schema = "revistanotas")]
    public class Autor
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string ShortBio { get; set; }

        public string Bio { get; set; }

        public string ImagenPerfil { get; set; }

        public List<Data.ContenidoAutor> Contenidos { get; set; }
    }
}
