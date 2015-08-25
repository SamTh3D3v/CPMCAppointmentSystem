using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    [Table("Specialite")]
    public class Specialite
    {
        public Specialite()
        {
            
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid SpecialiteId { get; set; }
        
        [Required]
        public String Name { get; set; }
        public String Description { get; set; }
        
        public virtual ICollection<Medecin> Medecins { get; set; }

    }
}
