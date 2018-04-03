using System.Collections.Generic;

namespace CPAU.RevistaNotas.Models.AutoresViewModel
{
    public class AutorViewModelEqualityComparer : IEqualityComparer<AutorViewModel>
    {
        public AutorViewModelEqualityComparer()
        {
            
        }

        public bool Equals(AutorViewModel x, AutorViewModel y)
        {
            // If reference same object including null then return true
            if (ReferenceEquals(x, y))
            {
                return true;
            }

            // If one object null the return false
            if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
            {
                return false;
            }

            // Compare properties for equality
            return (x.Id == y.Id);
        }

        public int GetHashCode(AutorViewModel obj)
        {
            return (obj.Id + obj.Nombre).GetHashCode();
        }
    }
}
