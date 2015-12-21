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
    public class User : Auditable,INotifyPropertyChanged, IDataErrorInfo
    {
        #region Fields
        private Guid _userId;
        private String _userNom;
        private String _userPrenom;
        private String _userName;        
        private string _userPass;                        

        #endregion
        #region Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
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
        public virtual ICollection<UserType> UserTypes { get; set; }
        public string Error
        {
            get { return String.Empty; }
        }
        #endregion
        #region INotifyPropertyChanged and IDataErrorInfo related logic

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        public string this[string columnName]
        {
            get
            {
                if (columnName == "UserNom")
                {
                    if (String.IsNullOrEmpty(UserNom))
                        return "Spesifié votre nom";
                    if (!UserNom.All(Char.IsLetter))
                        return "Donnez un nom valide";

                }
                if (columnName == "UserPrenom")
                {
                    if (String.IsNullOrEmpty(UserPrenom))
                        return "Spesifié votre prenom";
                    if (!UserPrenom.All(Char.IsLetter))
                        return "Donnez un prenom valide";

                }              
                if (columnName == "UserName")
                {
                    if (String.IsNullOrEmpty(UserName))
                        return "Spesifié votre prenom";
                    if (!UserName.All(Char.IsLetterOrDigit))
                        return "Donnez un nomd'utilisateur valide";

                    var dbContext = new CpmcContext();
                    var firstOrDefault = dbContext.Users.FirstOrDefault(u => u.UserName == UserName);
                    if (firstOrDefault != null && ((dbContext.Users.Any(u => u.UserName == UserName) && UserId==Guid.Empty)
                                                                                                ||((firstOrDefault.UserId!= UserId && UserId!=Guid.Empty))))
                        return "Ce nom d'utilisateur est déjà pris";

                }
                return String.Empty;
            }
        }
        #endregion        
    }
}
