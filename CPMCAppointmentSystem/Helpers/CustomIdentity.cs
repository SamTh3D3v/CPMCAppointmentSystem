using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CPMCAppointmentSystem.Helpers
{
    public class CustomIdentity : IIdentity
    {
        private string _name;
        private bool _isAuthenticated;
        public CustomIdentity(bool isAuthenticated, string name)
        {
            _isAuthenticated = isAuthenticated;
            _name = name;
        }
        public string AuthenticationType
        {
            get { return "Custom"; }
        }

        public bool IsAuthenticated
        {
            get { return _isAuthenticated; }
        }

        public string Name
        {
            get { return _name; }
        }
    }
}
