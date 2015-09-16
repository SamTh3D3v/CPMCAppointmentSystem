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
    [Table("RolesCollection")]
    public class RolesCollection : INotifyPropertyChanged
    {
        public RolesCollection()
        {

        }
        [Key, ForeignKey("User")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid RolesCollectionId { get; set; }
        //Roles

        #region CalendarView
        public bool AppointementViewAllow { get; set; }
        public bool AppointementEditAllow { get; set; }
        #endregion
        #region Doctors View
        public bool DoctorsViewAllow { get; set; }
        public bool DoctorsAddAllow { get; set; }
        #endregion
        #region Patient View
        public bool PatientsViewAllow { get; set; }
        public bool PatientsAditAllow { get; set; }
        public bool PatientsEditAppointementAllow { get; set; }

        #endregion
        #region Speciality View
        public bool SpecialitiesViewAllow { get; set; }
        public bool SpecialitiesEditAllow { get; set; }
        #endregion
        #region Pathology View
        public bool PathologiesViewAllow { get; set; }
        public bool PathologiesEditAllow { get; set; }
        #endregion
        #region MyPatients View
        public bool MyPatientsViewAllow { get; set; }   
        public bool MyPatientsEditAllow { get; set; }   
        public bool MyPatientsEditAppointementAllow { get; set; }   
     
        #endregion
        #region Settings View
        public bool SettingsViewAllow { get; set; } 
        #endregion
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
