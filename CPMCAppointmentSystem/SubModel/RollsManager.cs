using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.Model;

namespace CPMCAppointmentSystem.SubModel
{    
    public static class RollsManager
    {
        
        public static RolesCollection GetDefaultUserRolls(String userType)
        {
            //the defaut rolls collection will be getted from the xml settngs file //todo
            var rolls = new RolesCollection()
            {
                AppointementViewAllow = true,
                AppointementEditAllow = true,
                DoctorsViewAllow = true,
                DoctorsAddAllow = true,
                PatientsViewAllow = true,
                PatientsEditAllow = true,
                PatientsEditAppointementAllow = true,
                SpecialitiesViewAllow = true,
                SpecialitiesEditAllow = true,
                PathologiesViewAllow = true,
                PathologiesEditAllow = true,
                MyPatientsViewAllow = userType==App.Medecin?true:false,
                MyPatientsEditAllow = true,
                MyPatientsEditAppointementAllow = true,
                SettingsViewUsersAllow = true,
                SettingsEditUsersAllow = true,
                SettingsMangeThemeAllow = true,
                SmsNotificationViewAllow = true,
                SmsNotificationEditAllow = true,
                StatisticsViewAllow = true
            };
            return rolls;
        }
    }
}
