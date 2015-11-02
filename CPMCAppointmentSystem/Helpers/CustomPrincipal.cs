using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace CPMCAppointmentSystem.Helpers
{
    public class CustomPrincipal : IPrincipal
    {
        IIdentity identity;
        public CustomPrincipal(IIdentity identity)
        {
            this.identity = identity;
        }

        public IIdentity Identity
        {
            get { return this.identity; }
        }

        public bool IsInRole(string role)
        {
            return true;
        }

    }   
}
