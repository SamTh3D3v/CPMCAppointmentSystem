using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Model;
using JetBrains.Annotations;

namespace CPMCAppointmentSystem.SubModel
{
    public class EntityToAdd<T>:INotifyPropertyChanged
    {

        public T Entity { get; set; }
        private bool _isAdded = false;
        public bool IsAdded
        {
            get
            {
                return _isAdded;
            }

            set
            {
                if (_isAdded == value)
                {
                    return;
                }

                _isAdded = value;
                OnPropertyChanged();
            }
        }      

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
