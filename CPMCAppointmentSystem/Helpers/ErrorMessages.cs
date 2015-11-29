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

        public static Message AlreadyExistingRdvMessage = new Message(){Body = "This patient has an active rdv that hasn't passed yet",Header = "#Error1_ActiveRdv"};
        public static Message ThisIsARestDayMessage = new Message(){Body = "This is a rest day pal, you can't add an Appointment here",Header = "#Error2_RestDay"};
        


    }
}
