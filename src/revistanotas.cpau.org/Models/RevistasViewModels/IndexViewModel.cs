using System.Collections.Generic;

namespace CPAU.RevistaNotas.Models.RevistasViewModels
{
    public class IndexViewModel
    {
        public IndexViewModel()
        {
            this.Items = new List<RevistaViewModel>();
        }
        public List<RevistaViewModel> Items { get; set; }
    }
}
