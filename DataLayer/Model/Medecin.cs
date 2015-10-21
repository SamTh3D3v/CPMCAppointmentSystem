using System;
using System.Collections.ObjectModel;
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
    [Flags]
    public enum Days
    {
        None = 0,
        Saturday = 1,
        Sunday = 2,
        Monday = 4,
        Tuesday = 8,
        Wednesday = 16,
        Thursday=32,
        Friday = 64       
    }
    [Table("Medecin")]
    public class Medecin : INotifyPropertyChanged
    {
        private Days _joursDeTravail;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid MedecinId { get; set; }        
        public DateTime DateDeNaissance { get; set; }
        public String TelephoneFixe { get; set; }
        public String TelephoneMobile { get; set; }        
        public Guid SpecialitePrincipaleId { get; set; }

        public Days JoursDeTravail
        {
            get
            {
                return _joursDeTravail;
            }
            set
            {
                if (value == _joursDeTravail) return;
                //_joursDeTravail = ((decimal) value > 0)
                //    ? _joursDeTravail | (Days) value
                //    : _joursDeTravail & (Days) value;
                _joursDeTravail = value;
                OnPropertyChanged();
            }
        }

        public Guid UserId { get; set; }
        [ForeignKey("SpecialitePrincipaleId")]
        public virtual  Specialite SpecialitePrincipale { get; set; }
        [ForeignKey("UserId")]  
        public virtual User User { get; set; }              
        public virtual ICollection<Pathology> Pathologies { get; set; }    
        public virtual ICollection<Patient> Patients { get; set; }            
        public virtual ObservableCollection<Specialite> Specialities { get; set; }            

        #region INotifyPropertyChanged related

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }  
}
