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
    public class RendezVous : INotifyPropertyChanged 
    {
        public RendezVous()
        {
            
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid RendezVousId { get; set; }
        [Required]
        public DateTime DateTimeRdv { get; set; }
        [Required]
        public String LieuRdv { get; set; }        
        public Guid MedecinId { get; set; }
        public Guid PatientId { get; set; }
        [ForeignKey("PatientId")]
        public virtual Patient Patient { get; set; }
        [ForeignKey("MedecinId")]
        public virtual Medecin Medecin { get; set; }

        public bool NotificationSent { get; set; }
        public bool PatientConfirmRdv { get; set; }
        
        //This field is used when you report a client rdv, you need to set his other rdv to false and the last one to true
        public bool IsTheLastOne { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
