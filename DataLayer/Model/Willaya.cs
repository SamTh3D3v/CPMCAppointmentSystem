using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    [Table("Willaya")]
    public class Willaya
    {    
        public Willaya()
        {
            
        }
        [Key]
        public int WillayaId { get; set; }
        [Required]
        public String Designation  { get; set; }
        public virtual ICollection<Adresse> Adresses { get; set; } 
    }
}
