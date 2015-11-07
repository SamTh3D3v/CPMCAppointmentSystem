using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    public enum ParameterNames
    {
        RemindPatientRDVBefore, // int - Days
        SMSNotSendPatientRDVSince, // int - Days
        PatientNotConfirmRDVSince, // int - Days
        PatientWihoutRDVSince, // int - Days
        SMSCenterNumber, // string - Sync on change
        SMSBodyTemplate, // string - Sync on change
        DelayBetweenATCommand,// int - Sync on change
        MaxNumberOfRetryAfterSMSSendFailure // int - Sync on change        
    }
}
