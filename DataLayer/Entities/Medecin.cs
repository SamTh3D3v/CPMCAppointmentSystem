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
   
    [Table("Medecin")]
    public class Medecin :Auditable, INotifyPropertyChanged, IDataErrorInfo
    {
        #region Fields
        private Days _joursDeTravail;
        private ObservableCollection<Pathology> _pathologies;
        private ObservableCollection<Patient> _patients;
        private ObservableCollection<Specialite> _specialities;
        private ObservableCollection<RendezVous> _rendezVouses;
        private byte[] _profilePicture;
        //private Specialite _specialitePrincipale;
        private User _user;
        private string _telephoneMobile;
        private DateTime _dateDeNaissance=DateTime.Now;
        private string _telephoneFixe;
        //private Guid _specialitePrincipaleId;

        #endregion 
        #region Properties

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid MedecinId { get; set; }
        public DateTime DateDeNaissance
        {
            get { return _dateDeNaissance; }
            set
            {
                if (value.Equals(_dateDeNaissance)) return;
                _dateDeNaissance = value;
                OnPropertyChanged();
            }
        }
        public String TelephoneFixe
        {
            get { return _telephoneFixe; }
            set
            {
                if (value == _telephoneFixe) return;
                _telephoneFixe = value;
                OnPropertyChanged();
            }
        }

        public String TelephoneMobile
        {
            get { return _telephoneMobile; }
            set
            {
                if (value == _telephoneMobile) return;
                _telephoneMobile = value;
                OnPropertyChanged();
            }
        }     

        public Days JoursDeTravail
        {
            get
            {
                return _joursDeTravail;
            }
            set
            {
                if (value == _joursDeTravail) return;                
                _joursDeTravail = value;
                OnPropertyChanged();
            }
        }
        public Guid UserId { get; set; }

        //[ForeignKey("SpecialitePrincipaleId")]
        //public virtual Specialite SpecialitePrincipale
        //{
        //    get { return _specialitePrincipale; }
        //    set
        //    {
        //        if (Equals(value, _specialitePrincipale)) return;
        //        _specialitePrincipale = value;
        //        OnPropertyChanged();
        //    }
        //}
        [Parent]
        [ForeignKey("UserId")]
        public virtual User User
        {
            get { return _user; }
            set
            {
                if (Equals(value, _user)) return;
                _user = value;
                OnPropertyChanged();
            }
        }

        public virtual ObservableCollection<Pathology> Pathologies
        {
            get { return _pathologies; }
            set
            {
                if (Equals(value, _pathologies)) return;
                _pathologies = value;
                OnPropertyChanged();
            }
        }
        public virtual ObservableCollection<Patient> Patients
        {
            get { return _patients; }
            set
            {
                if (Equals(value, _patients)) return;
                _patients = value;
                OnPropertyChanged();
            }
        }
        public virtual ObservableCollection<Specialite> Specialities
        {
            get { return _specialities; }
            set
            {
                if (Equals(value, _specialities)) return;
                _specialities = value;
                OnPropertyChanged();
            }
        }
        public virtual ObservableCollection<RendezVous> RendezVouses
        {
            get { return _rendezVouses; }
            set
            {
                if (Equals(value, _rendezVouses)) return;
                _rendezVouses = value;
                OnPropertyChanged();
            }
        }

        public byte[] ProfilePicture
        {
            get { return _profilePicture; }
            set
            {
                if (value == _profilePicture) return;
                _profilePicture = value;
                OnPropertyChanged();
            }
        }
        public string Error
        {
            get { return String.Empty; }
        }
        #endregion                                   
        #region INotifyPropertyChanged related

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public string this[string columnName]
        {
            get
            {
                string result = null;
                //if (columnName == "SpecialitePrincipale")
                //{
                //    if (SpecialitePrincipale ==null)
                //        result = "Spesifiez la specialité principale du medecin";
                //}
                if (columnName == "JoursDeTravail")
                {
                    if (JoursDeTravail==Days.None)
                        result = "Spesifiez les jours de travail de medecin";
                }
                return result;
            }
        }

       
    }  
}
