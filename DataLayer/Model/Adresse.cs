using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Annotations;

namespace DataLayer.Model
{
    [Table("Adresse")]
    public class Adresse:INotifyPropertyChanged
    {
       
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid AdressId { get; set; }                             
        [Required]     
        public string AddressDesignation { get; set; }                   
        public string City { get; set; }
        [Required]   
        public String CodePosatal { get; set; }        
        public string Pays { get; set; }
        [Required]
        public int WillayaId { get; set; }
        [ForeignKey("WillayaId")]                
        public virtual Willaya Willaya { get; set; }
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
