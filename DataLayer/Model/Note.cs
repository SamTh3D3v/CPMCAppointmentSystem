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
    [Table("Note")]
    public class Note : INotifyPropertyChanged,IDataErrorInfo
    {
        #region Fields
        private string _title;
        private string _content;
        #endregion
        #region Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid NoteId { get; set; }

        public String Title
        {
            get { return _title; }
            set
            {
                if (value == _title) return;
                _title = value;
                OnPropertyChanged();
            }
        }

        public String Content
        {
            get { return _content; }
            set
            {
                if (value == _content) return;
                _content = value;
                OnPropertyChanged();
            }
        }

        public Guid PatientId { get; set; }
        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }
        [NotMapped]
        public string Error
        {
            get
            {
                return String.Empty;
            }
        }
        #endregion              
        #region INotifyPropertyChanged and IDataError related logic
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
                if (columnName == "Title")
                {
                    if (String.IsNullOrEmpty(Title))
                        result = "Spesifier le titre du note";
                }
                if (columnName == "Content")
                {
                    if (String.IsNullOrEmpty(Content))
                        result = "Spesifier le contenu du note";
                }
                return result;
            }
        }   
        #endregion            
    }
}
