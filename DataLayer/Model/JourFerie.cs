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
    public enum TypeJourFerie
    {
        Fix=1,
        Ocas=2
    }
    [Table("JourFerie")]
    public class JourFerie:INotifyPropertyChanged,IDataErrorInfo
    {
        #region Fields
        
        private Guid _jourFerieId;
        private DateTime _dateJourFerie = new DateTime(2015,1,1);
        private string _titreJourFerie;    
        private string _descriptionJourFerie;
        private TypeJourFerie _typeJourFerie;

        #endregion

        #region Properties

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid JourFerieId
        {
            get { return _jourFerieId; }
            set
            {
                if (value.Equals(_jourFerieId)) return;
                _jourFerieId = value;
                OnPropertyChanged();
            }
        }
        public DateTime DateJourFerie
        {
            get { return _dateJourFerie; }
            set
            {
                if (value.Equals(_dateJourFerie)) return;
                _dateJourFerie = value; 
                OnPropertyChanged();
            }
        }
        public String TitreJourFerie
        {
            get { return _titreJourFerie; }
            set
            {
                if (value == _titreJourFerie) return;
                _titreJourFerie = value;
                OnPropertyChanged();
            }
        }
        public String DescriptionJourFerie
        {
            get { return _descriptionJourFerie; }
            set
            {
                if (value == _descriptionJourFerie) return;
                _descriptionJourFerie = value;
                OnPropertyChanged();
            }
        }

        public TypeJourFerie TypeJourFerie
        {
            get { return _typeJourFerie; }
            set
            {
                if (value == _typeJourFerie) return;
                _typeJourFerie = value;
                OnPropertyChanged();
            }
        }

        #endregion

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
                if (columnName == "TitreJourFerie")
                {
                    if (string.IsNullOrEmpty(TitreJourFerie))
                        result = "Spesifiez le titre du jour ferié";
                }
                if (columnName == "DateJourFerie")
                {
                    if (DateJourFerie==null)
                        result = "Donner la date du jour ferier";
                }
                return result;
            }
        }
        [NotMapped]
        public string Error { get; private set; }
    }
}
