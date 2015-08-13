using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Views;

namespace CPMCAppointmentSystem.Helpers
{
    public interface IInnerFrameNavigationService : INavigationService
    {
        object Parameter { get; }
    }
}
