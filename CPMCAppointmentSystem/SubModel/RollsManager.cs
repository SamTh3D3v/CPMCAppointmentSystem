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
        
        public static void GetDefaultUserRolls(String userType, ref RolesCollection rolls)
        {
            //the defaut rolls collection will be getted from the xml settngs file //todo

            rolls.AppointementViewAllow = true;
            rolls.AppointementEditAllow = true;
            rolls.DoctorsViewAllow = true;
            rolls.DoctorsAddAllow = true;
            rolls.PatientsViewAllow = true;
            rolls.PatientsEditAllow = true;
            rolls.PatientsEditAppointementAllow = true;
            rolls.SpecialitiesViewAllow = true;
            rolls.SpecialitiesEditAllow = true;
            rolls.PathologiesViewAllow = true;
            rolls.PathologiesEditAllow = true;
            rolls.MyPatientsViewAllow = userType == App.Medecin ? true : false;
            rolls.MyPatientsEditAllow = true;
            rolls.MyPatientsEditAppointementAllow = true;
            rolls.SettingsViewUsersAllow = true;
            rolls.SettingsEditUsersAllow = true;
            rolls.SettingsMangeThemeAllow = true;
            rolls.SmsNotificationViewAllow = true;
            rolls.SmsNotificationEditAllow = true;
            rolls.StatisticsViewAllow = true;                                   
        }
    }
}
