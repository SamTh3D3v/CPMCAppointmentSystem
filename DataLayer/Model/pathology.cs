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
    [Table("Pathology")]
    public class Pathology : INotifyPropertyChanged, IDataErrorInfo
    {

        #region Fields
        private Guid _pathologyId;
        private string _codePathology;
        private string _nomPathology;
        private string _description;
        private ObservableCollection<Medecin> _medecins;
        private ObservableCollection<Patient> _patients;
        #endregion
        #region Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
        public String CodePathology
        {
            get { return _codePathology; }
            set
            {
                if (value == _codePathology) return;
                _codePathology = value;
                OnPropertyChanged();
                OnPropertyChanged("Item");
            }
        }

        [Required]
        public String NomPathology
        {
            get { return _nomPathology; }
            set
            {
                if (value == _nomPathology) return;
                _nomPathology = value;
                OnPropertyChanged();
                OnPropertyChanged("Item");
            }
        }

        public String Description
        {
            get { return _description; }
            set
            {
                if (value == _description) return;
                _description = value;
                OnPropertyChanged();
            }
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

        public string Error
        {
            get { return String.Empty; }

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
                if (columnName == "NomPathology")
                {
                    if (string.IsNullOrEmpty(NomPathology))
                        result = "donner le nom du pathology";
                }
                if (columnName == "CodePathology")
                {
                    if (string.IsNullOrEmpty(CodePathology))
                        result = "Donner le code de pathology";
                }
                return result;
            }
        }
        #endregion
    }
}
