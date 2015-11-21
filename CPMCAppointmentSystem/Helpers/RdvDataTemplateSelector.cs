using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using DataLayer.Model;

namespace CPMCAppointmentSystem.Helpers
{
    public class RdvDataTemplateSelector:DataTemplateSelector
    {
        public DataTemplate DefaultRdvDataTemplate { get; set; }
        public DataTemplate RestDayDataTemplate { get; set; }
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            DependencyPropertyDescriptor dpi = item as DependencyPropertyDescriptor;
            if (dpi != null && dpi.PropertyType == typeof(RendezVous))
            {
                return DefaultRdvDataTemplate;
            }
            if (dpi != null && dpi.PropertyType == typeof(JourFerie))
            {
                return RestDayDataTemplate;
            }

            return base.SelectTemplate(item, container);
        }
    }
}
