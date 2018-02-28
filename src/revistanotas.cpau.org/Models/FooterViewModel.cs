using System.Collections.Generic;

namespace CPAU.RevistaNotas.Models
{
    public class FooterViewModel
    {
        public bool ShowBreadCrumb { get; set; }

        public List<BreadCrumbItem> BreadCrumbItems { get; set; }
        public string TagName { get; set; }

        public FooterViewModel()
        {
            this.TagName = "footer";
            this.ShowBreadCrumb = false;
            this.BreadCrumbItems = new List<BreadCrumbItem>();
        }
    }

    public class BreadCrumbItem
    {
        public string Href { get; set; }

        public string Caption { get; set; }
    }
}
