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
using Syncfusion.UI.Xaml.Schedule;

namespace DataLayer.Model
{
    [Table("RendezVous")]
    public class RendezVous : ScheduleAppointment, INotifyPropertyChanged,IDataErrorInfo, IAuditable
    {
        #region Fileds
        private DateTime _dateTimeRdv=DateTime.Now;
        private string _lieuRdv;
        private Guid _medecinId;
        private Guid _patientId;
        private Patient _patient;
        private Medecin _medecin;
        private bool _notificationSent;
        private bool _patientConfirmRdv;
        private bool _isTheLastOne;
        #endregion
        #region Properties
        [Required]
        public DateTime DateTimeRdv
        {
            get
            {
                return _dateTimeRdv;
            }

            set
            {
                if (_dateTimeRdv == value)
                {
                    return;
                }

                _dateTimeRdv = value;
                OnPropertyChanged();
                this.StartTime = value;
                this.EndTime = value+new TimeSpan(0,30,0);
            }
        }        
      
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid RendezVousId { get; set; }

        [Required]
        public String LieuRdv
        {
            get { return _lieuRdv; }
            set
            {
                if (value == _lieuRdv) return;
                _lieuRdv = value;
                OnPropertyChanged();
            }
        }

        public Guid MedecinId
        {
            get { return _medecinId; }
            set
            {
                if (value.Equals(_medecinId)) return;
                _medecinId = value;
                OnPropertyChanged();
            }
        }

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

        [ForeignKey("PatientId")]
        public virtual Patient Patient
        {
            get { return _patient; }
            set
            {
                if (Equals(value, _patient)) return;
                _patient = value;
                OnPropertyChanged();
            }
        }

        [ForeignKey("MedecinId")]
        public virtual Medecin Medecin
        {
            get { return _medecin; }
            set
            {
                if (Equals(value, _medecin)) return;
                _medecin = value;
                OnPropertyChanged();
            }
        }

        public bool NotificationSent
        {
            get { return _notificationSent; }
            set
            {
                if (value.Equals(_notificationSent)) return;
                _notificationSent = value;
                OnPropertyChanged();
            }
        }

        public bool PatientConfirmRdv
        {
            get { return _patientConfirmRdv; }
            set
            {
                if (value.Equals(_patientConfirmRdv)) return;
                _patientConfirmRdv = value;
                OnPropertyChanged();
            }
        }

        //This field is used when you report a client rdv, you need to set his other rdv to false and the last one to true
        public bool IsTheLastOne
        {
            get { return _isTheLastOne; }
            set
            {
                if (value.Equals(_isTheLastOne)) return;
                _isTheLastOne = value;
                OnPropertyChanged();
            }
        }
        [NotMapped]
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
                if (columnName == "Medecin")
                {
                    if (MedecinId==Guid.Empty)
                        result = "Selectionnez un medecin";
                }
                if (columnName == "LieuRdv")
                {
                    if (string.IsNullOrEmpty(LieuRdv))
                        result = "Spesifiez le lieu du rendez vous";
                }
                if (columnName == "CodePathology")
                {
                    if (DateTimeRdv == null)                    
                        result = "Spesifiez la date du rendez vous";
                    if (DateTimeRdv < DateTime.Now)                    
                        result = "Cette date est invalide";                    
                        
                }
                return result;
            }
        }
        
        #endregion       
    
        public DateTime CreatedOn
        {
            get;
            set;
        }

        public DateTime ModifiedOn
        {
            get;
            set;
        }

        public Guid ModifiedBy
        {
            get;
            set;
        }

        public Guid CreatedBy
        {
            get;
            set;
        }
    }
}
