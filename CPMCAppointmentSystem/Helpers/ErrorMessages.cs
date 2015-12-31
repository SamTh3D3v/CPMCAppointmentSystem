using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPMCAppointmentSystem.Helpers
{
    public class Message
    {
        public String Header { get; set; }
        public String Body { get; set; }
    }
    public static class ErrorMessages
    {

        public static Message AlreadyExistingRdvMessage = new Message() { Body = "Ce patient a déja un rendez-vous active", Header = "Patient a déja un rdv" }; //This patient has an active rdv that hasn't passed yet
        public static Message ThisIsARestDayMessage = new Message() { Body = "Vous avez sélectionné un jour férié ou vous ne pouvez pas ajouter de rdv ", Header = "Jour férié" };
        public static Message CantLogInMessage = new Message() { Body = "Utilisateur inconnu ou mot de passe erroné", Header = "#Error2_Pass" };
        public static Message LogInInProgressMessage = new Message() { Body = "connexion en cours", Header = "#Error2_Login" };

        


    }
}
