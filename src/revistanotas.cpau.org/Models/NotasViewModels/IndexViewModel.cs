using System.Collections.Generic;
using CPAU.RevistaNotas.Data;
using CPAU.RevistaNotas.Models.RevistasViewModels;

namespace CPAU.RevistaNotas.Models.NotasViewModels
{
    public class IndexViewModel
    {
        public List<ContenidoViewModel> Items { get; set; }

        public List<Tag> Tags { get; set; }

        public IndexViewModel()
        {
            this.Tags = new List<Tag>();
            this.Items = new List<ContenidoViewModel>();
        }
    }
}
