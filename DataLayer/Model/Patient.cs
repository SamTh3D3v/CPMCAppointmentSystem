using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    [Table("Patient")]
    public class Patient
    {
        public Patient()
        {
            
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PatientId { get; set; }
        [Required]
        public String Nom { get; set; }
        [Required]
        public String Prenom { get; set; }
        public String TelephoneFixe { get; set; }
        [Required]
        public String TelephoneMobile1 { get; set; }
        public String TelephoneMobile2 { get; set; }  
      

        public virtual Adresse Adresse { get; set; }
        public virtual ICollection<Medecin> Medecins { get; set; }
        public virtual ICollection<RendezVous> RendezVouses { get; set; }
    }
}
