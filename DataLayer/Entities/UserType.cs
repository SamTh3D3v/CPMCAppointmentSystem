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
    [Table("UserType")]
    public class UserType : INotifyPropertyChanged
    {
        #region Fields
        private Guid _rolesCollectionId;
        private RolesCollection _rolesCollection;

        #endregion
        #region Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserTypeId { get; set; }
        [Required]
        public String UserTypeName { get; set; }
        public int UserTypeIconId { get; set; }
        public virtual Guid RolesCollectionId
        {
            get { return _rolesCollectionId; }
            set
            {
                if (value.Equals(_rolesCollectionId)) return;
                _rolesCollectionId = value;
                OnPropertyChanged();
            }
        }
        [ForeignKey("RolesCollectionId")]
        public virtual RolesCollection RolesCollection
        {
            get { return _rolesCollection; }
            set
            {
                if (Equals(value, _rolesCollection)) return;
                _rolesCollection = value;
                OnPropertyChanged();
            }
        }                  
        #endregion
      
        
        public event PropertyChangedEventHandler PropertyChanged;
        public virtual ICollection<User> Users { get; set; }
        #region INotifyPropertyChanged related
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
