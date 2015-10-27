using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Model;
using Syncfusion.UI.Xaml.Grid;
using System.Windows.Interactivity;

namespace CPMCAppointmentSystem.Behavior
{
    public class InitialSetupBehaviour : Behavior<SfDataGrid>
    {
        protected override void OnAttached()
        {
            this.AssociatedObject.RowValidating += OnRowValidating;
        }

        void OnRowValidating(object sender, RowValidatingEventArgs args)
        {
            if (args.RowData != null)
            {
                var jf = (args.RowData as JourFerie);
                string result = null;

                if (string.IsNullOrEmpty(jf.TitreJourFerie))
                {
                    args.ErrorMessages.Add("TitreJourFerie", "Donnez le titre du jour ferie");
                    args.IsValid = false;
                }
                if (jf.DateJourFerie==null)
                {
                    args.ErrorMessages.Add("DateJourFerie", "Donner la date du jour ferier");
                    args.IsValid = false;
                }                
            }
        }

        protected override void OnDetaching()
        {
            this.AssociatedObject.RowValidating -= OnRowValidating;
        }
    }
}
