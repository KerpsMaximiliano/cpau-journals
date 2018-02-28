using System.Collections.Generic;

namespace CPAU.RevistaNotas.Models.AutoresViewModel
{
    public class AutoresViewModel
    {
        public List<AutorViewModel> Autores { get; set; }

        public AutoresViewModel()
        {
            this.Autores = new List<AutorViewModel>();
        }
    }
}
