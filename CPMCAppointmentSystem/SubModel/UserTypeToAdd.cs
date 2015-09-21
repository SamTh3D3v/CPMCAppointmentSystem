using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CPMCAppointmentSystem.Helpers;
using DataLayer.Model;

namespace CPMCAppointmentSystem.SubModel
{
    public class UserTypeToAdd:UserType
    {
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
    }
}
