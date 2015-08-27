using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    [Table("Sexe")]
    public class Sexe
    {
        [Key]
        public int SexeId { get; set; }
        [Required]
        public String Designation { get; set; }
        public virtual ICollection<Patient> Patients { get; set; } 
    }
}
