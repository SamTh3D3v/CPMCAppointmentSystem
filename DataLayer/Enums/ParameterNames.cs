using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    public enum ParameterNames
    {
        NotifyPatientRDVDateBefore, // int - Days
        NotifyWhenSMSNotSendForPatientRDVSince, // int - Days
        NotifyWhenPatientNotConfirmRDVSince, // int - Days
        NotifyWhenPatientWithoutRDVSince, // int - Days
        SMSCenterNumber, // string - Sync on change
        SMSBodyTemplate, // string - Sync on change
        DelayBetweenATCommand,// int - Sync on change
        MaxNumberOfRetryAfterSMSSendFailure // int - Sync on change        
    }
}
