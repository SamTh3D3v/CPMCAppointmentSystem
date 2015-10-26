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
    [Table("JourFerie")]
    public class JourFerie:INotifyPropertyChanged
    {
        #region Fields
        private Guid _jourFerieId;
        private DateTime _dateJourFerie;
        private string _titreJourFerie;    
        private string _descriptionJourFerie;                  
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

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
