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
    [Table("Willaya")]    
    public class Willaya:INotifyPropertyChanged
    {    
        public Willaya()
        {
            
        }
        [Key]
        public int WillayaId { get; set; }
        [Required]
        public String Designation  { get; set; }
        public virtual ICollection<Adresse> Adresses { get; set; }
        #region INotifyPropertyChanged related MyRegion                
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
