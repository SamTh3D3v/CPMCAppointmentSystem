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
    [Table("Pathology")]
    public class Pathology : INotifyPropertyChanged,IDataErrorInfo
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PathologyId { get; set; }
        [Required]
        public String CodePathology { get; set; }
        [Required]
        public String NomPathology { get; set; }
        public String Description { get; set; }
        public virtual ICollection<Medecin> Medecins { get; set; }
        public virtual ICollection<Patient> Patients { get; set; }
       
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
        
        public string Error { get; private set; }

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
