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

/* réunion de concertation pluridisciplinaire
 *Réunion entre professionnels de santé où se discutent les traitements
 *proposés à un patient.
 */

namespace DataLayer.Model
{
    [Table("RCP")]
    public class RCP : INotifyPropertyChanged
    {
        #region Fields    
        private DateTime _dateTimeRcp;
        private ObservableCollection<Patient> _patients;
        private ObservableCollection<User> _participants;
        #endregion
        #region Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid RcpId { get; set; }
        public virtual String Description { get; set; }
        public String RcpTitle { get; set; }
        [Required]
        public DateTime DateTimeRcp
        {
            get
            {
                return _dateTimeRcp;
            }

            set
            {
                if (_dateTimeRcp == value)
                {
                    return;
                }

                _dateTimeRcp = value;
                OnPropertyChanged();
                RcpTitle = "RCP :" + _dateTimeRcp.Date;
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
        public virtual ObservableCollection<User> Participants
        {
            get { return _participants; }
            set
            {
                if (Equals(value, _participants)) return;
                _participants = value;
                OnPropertyChanged();
            }
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
    }
}
