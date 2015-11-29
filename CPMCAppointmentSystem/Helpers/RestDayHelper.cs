using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Model;

namespace CPMCAppointmentSystem.Helpers
{
    public static class RestDayHelper
    {
        public static bool IsRestDay(DateTime rdDate)
        {
            using (var db = new CpmcContext())
            {
                return
                    db.JourFeries.AsEnumerable()
                        .Any(
                            d =>
                                (d.DateJourFerie == rdDate.Date && d.TypeJourFerie == TypeJourFerie.Ocas) ||
                                (d.DateJourFerie.Day == rdDate.Day && d.DateJourFerie.Month == rdDate.Month &&
                                 d.TypeJourFerie == TypeJourFerie.Fix));
            }            
        }
    }
}
