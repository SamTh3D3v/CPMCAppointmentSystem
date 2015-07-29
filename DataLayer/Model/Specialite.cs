using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    public class Specialite
    {
        public Specialite()
        {
            
        }

        public Guid SpecialiteId { get; set; }
        public virtual ICollection<Medecin> Medecins { get; set; }

    }
}
