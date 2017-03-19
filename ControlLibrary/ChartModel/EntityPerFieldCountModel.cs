using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Annotations;

namespace ControlLibrary.ChartModel
{
    public class EntityPerFieldCountModel:INotifyPropertyChanged
    {
        private string _field;
        private double _count;

        public string Field
        {
            get { return _field; }
            set
            {
                if (value == _field) return;
                _field = value;
                OnPropertyChanged();
            }
        }

        public double Count
        {
            get { return _count; }
            set
            {
                if (value.Equals(_count)) return;
                _count = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}





