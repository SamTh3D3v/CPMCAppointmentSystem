using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public class Patient :  INotifyPropertyChanged,IDataErrorInfo
    {
        #region Fields
        private Guid _patientId;
        private string _numeroDordre;
        private string _nom;
        private string _prenom;
        private int _sexeId;
        private string _telephoneFixe;
        private string _telephoneMobile1;
        private string _telephoneDaccompagnant;
        private Guid _adressId;
        private bool _carteProfessionel;
        private Guid? _pathologyId;
        private DateTime _dateDeNaissance;
        private Sexe _sexe;
        private Adresse _adresse;
        private Pathology _pathology;
        private DateTime _dateDeDepot;
        private ObservableCollection<PieceJointe> _pieceJointes;
        private byte[] _profilePicture;
        private string _nomPrenomDaccompagnant;
        private ObservableCollection<Medecin> _medecins;
        private ObservableCollection<RendezVous> _rendezVouses;
        private ObservableCollection<Note> _notes;

        #endregion
        #region Properties                
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

        [Required]
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

        public String TelephoneDaccompagnant
        {
            get { return _telephoneDaccompagnant; }
            set
            {
                if (value == _telephoneDaccompagnant) return;
                _telephoneDaccompagnant = value;
                OnPropertyChanged();
            }
        }

        public String NomPrenomDaccompagnant
        {
            get { return _nomPrenomDaccompagnant; }
            set
            {
                if (value == _nomPrenomDaccompagnant) return;
                _nomPrenomDaccompagnant = value;
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

        public virtual Guid? PathologyId
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

        [NotMapped]
        public string Error
        {
            get { return String.Empty; }
        }

        public virtual ObservableCollection<Medecin> Medecins
        {
            get { return _medecins; }
            set
            {
                if (Equals(value, _medecins)) return;
                _medecins = value;
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

        public virtual ObservableCollection<PieceJointe> PieceJointes
        {
            get { return _pieceJointes; }
            set
            {
                if (Equals(value, _pieceJointes)) return;
                _pieceJointes = value;
                OnPropertyChanged();
            }
        }

        public virtual ObservableCollection<Note> Notes
        {
            get { return _notes; }
            set
            {
                if (Equals(value, _notes)) return;
                _notes = value;
                OnPropertyChanged();
            }
        }

        #endregion
        #region INotifyPropertyChanged and IDataErrorInfo related logic

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        public string this[string columnName]
        {
            get
            {
                string result = null;
                if (columnName == "NumeroDordre")
                {
                    if (string.IsNullOrEmpty(NumeroDordre))
                        result = "Spesifiez le numero d'ordre";
                    var dbContext = new CpmcContext();
                    if (dbContext.Patients.Any(p => p.NumeroDordre == NumeroDordre))
                        return "Ce numero d'ordre exist deja";

                }
                if (columnName == "Nom")
                {
                    if (string.IsNullOrEmpty(Nom))
                        result = "Spesifiez le nom du patient";
                }
                if (columnName == "Prenom")
                {
                    if (string.IsNullOrEmpty(Prenom))
                        result = "Spesifiez le prenom du patient";
                }
                if (columnName == "SexeId")
                {
                    if (SexeId == 0)
                        result = "Spesifiez le sexe du patient";
                }
                if (columnName == "TelephoneFixe")
                {
                    if (String.IsNullOrEmpty(TelephoneFixe))
                        result = "Spesifiez le numero de tel fix du patient";
                }
                if (columnName == "TelephoneMobile1")
                {
                    if (String.IsNullOrEmpty(TelephoneMobile1))
                        result = "Spesifiez le numero de tel mobile de l'assurant";
                }
                if (columnName == "TelephoneDaccompagnant")
                {
                    if (String.IsNullOrEmpty(TelephoneDaccompagnant))
                        result = "Spesifiez le numero de tel mobile de l'accompagnant";
                }
                return result;
            }
        }
        #endregion       
    }


}
