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
        private Guid _patientId;
        private string _numeroDordre;
        private string _nom;
        private string _prenom;
        private int _sexeId;
        private string _telephoneFixe;
        private string _telephoneMobile1;
        private string _telephoneMobile2;
        private Guid _adressId;
        private bool _carteProfessionel;
        private Guid _pathologyId;
        private DateTime _dateDeNaissance;
        private Sexe _sexe;
        private Adresse _adresse;
        private Pathology _pathology;
        private DateTime _dateDeDepot;

        public Patient()
        {
            
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PatientId
        {
            get { return _patientId; }
            set
            {
                if (value.Equals(_patientId)) return;
                _patientId = value;
                OnPropertyChanged();
            }
        }

        //[Required]
        public String NumeroDordre
        {
            get { return _numeroDordre; }
            set
            {
                if (value == _numeroDordre) return;
                _numeroDordre = value;
                OnPropertyChanged();
            }
        }

        [Required]
        public String Nom
        {
            get { return _nom; }
            set
            {
                if (value == _nom) return;
                _nom = value;
                OnPropertyChanged();
            }
        }

        [Required]
        public String Prenom
        {
            get { return _prenom; }
            set
            {
                if (value == _prenom) return;
                _prenom = value;
                OnPropertyChanged();
            }
        }

        [Required]
        public int SexeId
        {
            get { return _sexeId; }
            set
            {
                if (value == _sexeId) return;
                _sexeId = value;
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

        [Required]
        public String TelephoneMobile1
        {
            get { return _telephoneMobile1; }
            set
            {
                if (value == _telephoneMobile1) return;
                _telephoneMobile1 = value;
                OnPropertyChanged();
            }
        }

        public String TelephoneMobile2
        {
            get { return _telephoneMobile2; }
            set
            {
                if (value == _telephoneMobile2) return;
                _telephoneMobile2 = value;
                OnPropertyChanged();
            }
        }

        [Required]
        public Guid AdressId
        {
            get { return _adressId; }
            set
            {
                if (value.Equals(_adressId)) return;
                _adressId = value;
                OnPropertyChanged();
            }
        }

        public bool CarteProfessionel
        {
            get { return _carteProfessionel; }
            set
            {
                if (value.Equals(_carteProfessionel)) return;
                _carteProfessionel = value;
                OnPropertyChanged();
            }
        }

        public Guid PathologyId
        {
            get { return _pathologyId; }
            set
            {
                if (value.Equals(_pathologyId)) return;
                _pathologyId = value;
                OnPropertyChanged();
            }
        }

        [Required]
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

        [ForeignKey("SexeId")]
        public virtual Sexe Sexe
        {
            get { return _sexe; }
            set
            {
                if (Equals(value, _sexe)) return;
                _sexe = value;
                OnPropertyChanged();
            }
        }

        [ForeignKey("AdressId")]
        public virtual Adresse Adresse
        {
            get { return _adresse; }
            set
            {
                if (Equals(value, _adresse)) return;
                _adresse = value;
                OnPropertyChanged();
            }
        }

        [ForeignKey("PathologyId")]
        public virtual Pathology Pathology
        {
            get { return _pathology; }
            set
            {
                if (Equals(value, _pathology)) return;
                _pathology = value;
                OnPropertyChanged();
            }
        }

        //[Required]
        public DateTime DateDeDepot
        {
            get { return _dateDeDepot; }
            set
            {
                if (value.Equals(_dateDeDepot)) return;
                _dateDeDepot = value;
                OnPropertyChanged();
            }
        }

        public virtual ICollection<Medecin> Medecins { get; set; }
        public virtual ICollection<RendezVous> RendezVouses { get; set; }
        public virtual ICollection<PieceJointe> PieceJointes { get; set; }
        public virtual ICollection<Note> Notes { get; set; }
        

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
