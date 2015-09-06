using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Model;

namespace CPMCAppointmentSystem.SubModel
{
    public class MedecinToAdd : Medecin
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

        //public MedecinToAdd(Medecin medecin)
        //{
            

        //}
    }
}
