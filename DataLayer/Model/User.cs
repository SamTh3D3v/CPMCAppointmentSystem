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
    public class User : INotifyPropertyChanged, IDataErrorInfo
    {
        #region Fields
        private Guid _userId;
        private String _userNom;
        private String _userPrenom;
        private String _userName;
        private Guid? _rolesCollectionId;
        private string _userPass;
        private Guid _userTypeId;
        private UserType _userType;
        private RolesCollection _rolesCollection;

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



        public virtual Guid? RolesCollectionId
        {
            get { return _rolesCollectionId; }
            set
            {
                if (value.Equals(_rolesCollectionId)) return;
                _rolesCollectionId = value;
                OnPropertyChanged();
            }
        }

        [Required]
        public String UserPass
        {
            get { return _userPass; }
            set
            {
                if (value == _userPass) return;
                _userPass = value;
                OnPropertyChanged();
            }
        }

        public Guid UserTypeId
        {
            get { return _userTypeId; }
            set
            {
                if (value.Equals(_userTypeId)) return;
                _userTypeId = value;
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

        [ForeignKey("UserTypeId")]
        public virtual UserType UserType
        {
            get { return _userType; }
            set
            {
                if (Equals(value, _userType)) return;
                _userType = value;
                OnPropertyChanged();
            }
        }
        public string Error
        {
            get { return String.Empty; }
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

        public string this[string columnName]
        {
            get
            {
                if (columnName == "UserNom")
                {
                    if (String.IsNullOrEmpty(UserNom))
                        return "Spesifié votre nom";
                    if (!UserNom.All(Char.IsLetter))
                        return "Donnez un nom d'utilisateur valid";

                }
                if (columnName == "UserPrenom")
                {
                    if (String.IsNullOrEmpty(UserPrenom))
                        return "Spesifié votre prenom";
                    if (!UserPrenom.All(Char.IsLetter))
                        return "Donnez un prenom d'utilisateur valid";

                }
                if (columnName == "UserName")
                {
                    if (String.IsNullOrEmpty(UserName))
                        return "Spesifié votre prenom";
                    if (!UserName.All(Char.IsLetter))
                        return "Donnez un prenom d'utilisateur valid";

                    var dbContext = new CpmcContext();
                    if (dbContext.Users.Any(u => u.UserName == UserName))
                        return "Ce nom d'utlisateur est deja prise";

                }
                return String.Empty;
            }
        }

    }
}
