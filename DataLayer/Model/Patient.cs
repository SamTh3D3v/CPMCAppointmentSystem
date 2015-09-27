using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Annotations;

namespace DataLayer.Model
{
    [Table("Patient")]
    public class Patient : INotifyPropertyChanged
    {
        public Patient()
        {
            
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PatientId { get; set; }
        //[Required]
        public String NumeroDordre { get; set; }        
        [Required]  
        public String Nom { get; set; }
        [Required]
        public String Prenom { get; set; }
        [Required]
        public int SexeId { get; set; }
        public String TelephoneFixe { get; set; }
        [Required]
        public String TelephoneMobile1 { get; set; }
        public String TelephoneMobile2 { get; set; }
        [Required]
        public Guid AdressId { get; set; }
        [Required]
        public DateTime DateDeNaissance { get; set; }
        [ForeignKey("SexeId")]
        public virtual Sexe Sexe { get; set; }
        [ForeignKey("AdressId")]
        public virtual Adresse Adresse { get; set; }
        public virtual ICollection<Medecin> Medecins { get; set; }
        public virtual ICollection<RendezVous> RendezVouses { get; set; }
        public virtual ICollection<PieceJointe> PieceJointes { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
        //[Required]
        public DateTime DateDeDepot { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
