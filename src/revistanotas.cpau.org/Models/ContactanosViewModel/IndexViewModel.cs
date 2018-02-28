using System.ComponentModel.DataAnnotations;

namespace CPAU.RevistaNotas.Models.ContactanosViewModel
{
    public class IndexViewModel
    {
        [Required]
        public string NombresYApellidos { get; set; }

        [Required]
        public string Telefono { get; set; }

        [Required]
        public string CorreoElectronico { get; set; }

        [Required]
        public string Mensaje { get; set; }

        public bool MessageSent { get; set; }
    }
}
