using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    [Table("Medecin")]
    public class Medecin
    {
        public Medecin()
        {
                
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid MedecinId { get; set; }
        [Required]
        public String Nom { get; set; }
        [Required]
        public String Prenom { get; set; }
        public DateTime DateDeNaissance { get; set; }
        public String TelephoneFixe { get; set; }
        public String TelephoneMobile { get; set; }


        [Required]
        public virtual User User { get; set; }              
        public virtual ICollection<Pathology> Pathologies { get; set; }    
        public virtual ICollection<Patient> Patients { get; set; }    

    }
}
