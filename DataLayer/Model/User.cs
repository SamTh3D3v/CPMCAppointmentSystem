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
    [Table("User")]
    public class User:INotifyPropertyChanged
    {
        public User()
        {

        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserId { get; set; }
        [Required]
        public String UserNom { get; set; }
        [Required]
        public String UserPrenom { get; set; }
        [Required]
        public Guid RolesCollectionId { get; set; }
        [Required]
        public String UserName { get; set; }
        [Required]
        public String UserPass { get; set; }
        
        public Guid UserTypeId { get; set; }
        [ForeignKey("RolesCollectionId")]
        public virtual RolesCollection RolesCollection { get; set; }
        [ForeignKey("UserTypeId")]
        public virtual UserType UserType { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
