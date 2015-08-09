using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServiceLayer
{    
    public class CpmcService : IUserService,IAdminService,IDoctorService,IAuthentificationService
    {
        public void DoWork()
        {
        }
    }
}
