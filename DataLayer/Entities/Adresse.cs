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
    public class Adresse : INotifyPropertyChanged, IDataErrorInfo
    {
        #region Filelds
        private Guid _adressId;
        private Willaya _willaya;
        private string _pays;
        private string _city;
        private string _addressDesignation;
        private string _codePosatal;
        private int _willayaId;
        #endregion
        #region Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid AdressId
        {
            get { return _adressId; }
            set
            {
                if (value.Equals(_adressId)) return;
                _adressId = value;
                OnPropertyChanged();
            }
        }

        [Required]
        public string AddressDesignation
        {
            get { return _addressDesignation; }
            set
            {
                if (value == _addressDesignation) return;
                _addressDesignation = value;
                OnPropertyChanged();
                OnPropertyChanged("Item");
            }
        }

        public string City
        {
            get { return _city; }
            set
            {
                if (value == _city) return;
                _city = value;
                OnPropertyChanged();
                OnPropertyChanged("Item");
            }
        }

        [Required]
        public String CodePosatal
        {
            get { return _codePosatal; }
            set
            {
                if (value == _codePosatal) return;
                _codePosatal = value;
                OnPropertyChanged();
                OnPropertyChanged("Item");
            }
        }

        public string Pays
        {
            get { return _pays; }
            set
            {
                if (value == _pays) return;
                _pays = value;
                OnPropertyChanged();
            }
        }

        [Required]
        public int WillayaId
        {
            get { return _willayaId; }
            set
            {
                if (value == _willayaId) return;
                _willayaId = value;
                OnPropertyChanged();
                OnPropertyChanged("Item");
            }
        }

        [ForeignKey("WillayaId")]
        public virtual Willaya Willaya
        {
            get { return _willaya; }
            set
            {
                if (Equals(value, _willaya)) return;
                _willaya = value;
                OnPropertyChanged();
            }
        }
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
                string result = null;
                if (columnName == "AddressDesignation")
                {
                    if (string.IsNullOrEmpty(AddressDesignation))
                        result = "donnez l'adresse";
                }
                if (columnName == "City")
                {
                    if (string.IsNullOrEmpty(City))
                        result = "Spesifiez la City";
                }
                //if (columnName == "CodePosatal")
                //{
                //    if (string.IsNullOrEmpty(CodePosatal))
                //        result = "Spesifiez le code postale";
                //}
                if (columnName == "WillayaId")
                {
                    if (WillayaId == 0)
                        result = "Spesifiez la willaya";
                }
                return result;
            }
        }
        #endregion
    }
}
