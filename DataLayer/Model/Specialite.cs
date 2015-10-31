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
    [Table("Specialite")]
    public class Specialite : INotifyPropertyChanged, IDataErrorInfo
    {      
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid SpecialiteId { get; set; }
        
        [Required]
        public String Name { get; set; }
        [Required]
        public String Code { get; set; }
        public String Description { get; set; }     
        public virtual ObservableCollection<Medecin> Medecins { get; set; }
        
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
                if (columnName == "Name")
                {
                    if (string.IsNullOrEmpty(Name))
                        result = "Spesifiez le nom du specialité";
                }
                if (columnName == "Code")
                {
                    if (string.IsNullOrEmpty(Code))
                        result = "Spesifiez le code du specialité";
                }
                return result;
            }
        }
        public string Error
        {
            get { return String.Empty; }
        }
    }
}
