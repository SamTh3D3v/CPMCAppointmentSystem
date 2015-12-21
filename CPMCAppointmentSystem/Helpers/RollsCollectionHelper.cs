using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Model;
using Syncfusion.Data.Extensions;

namespace CPMCAppointmentSystem.Helpers
{
    public static class RollsCollectionHelper
    {
        public static RolesCollection MergeRolls(IEnumerable<RolesCollection> rolls)
        {

            var resRollsCollection=new RolesCollection();
            var properties = typeof(RolesCollection).GetProperties();
            rolls.ForEach(r =>
            {                
                properties.ForEach(p =>
                {
                    if (p.PropertyType == typeof (bool))
                    {
                        var rr = (bool) p.GetValue(resRollsCollection, null)
                                 || (bool)p.GetValue(r, null);
                        p.SetValue(resRollsCollection, rr);                        
                    }                    
                });
            });

            return resRollsCollection;

        }
    }
}
