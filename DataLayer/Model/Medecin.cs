using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Annotations;

namespace DataLayer.Model
{
    [Table("Medecin")]
    public class Medecin : INotifyPropertyChanged
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
        public Guid SpecialiteId { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey("SpecialiteId")]
        public virtual  Specialite Speciality { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }              
        public virtual ICollection<Pathology> Pathologies { get; set; }    
        public virtual ICollection<Patient> Patients { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));             
        }
    }  
}
