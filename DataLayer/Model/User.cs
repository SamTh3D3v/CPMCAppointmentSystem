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
    public class User : INotifyPropertyChanged
    {
        #region Fields
        private Guid _userId;
        private String _userNom;
        private String _userPrenom;
        private String _userName;
        #endregion
        #region Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserId
        {
            get
            {
                return _userId;
            }

            set
            {
                if (_userId == value)
                {
                    return;
                }

                _userId = value;
                OnPropertyChanged();
            }
        }
        [Required]
        public String UserNom
        {
            get
            {
                return _userNom;
            }

            set
            {
                if (_userNom == value)
                {
                    return;
                }

                _userNom = value;
                OnPropertyChanged();
            }
        }
        [Required]
        public String UserPrenom
        {
            get
            {
                return _userPrenom;
            }

            set
            {
                if (_userPrenom == value)
                {
                    return;
                }

                _userPrenom = value;
                OnPropertyChanged();
            }
        }
        [Required]
        public String UserName
        {
            get
            {
                return _userName;
            }

            set
            {
                if (_userName == value)
                {
                    return;
                }

                _userName = value;
                OnPropertyChanged();
            }
        }
        #endregion
        public User()
        {

        }     
        [Required]
        public Guid RolesCollectionId { get; set; }
                
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
