using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;

namespace AMSSetupCustomAction
{
    [RunInstaller(true)]
    public partial class CustomInstaller : System.Configuration.Install.Installer
    {
        public CustomInstaller()
        {
            InitializeComponent();
        }


        public override void Install(IDictionary stateSaver)
        {


            base.Install(stateSaver);

            // Get actual configuration
            Configuration config = ConfigurationManager.OpenExeConfiguration(this.Context.Parameters["targetDir"].ToString() + "CPMCAppointmentSystem.exe");

            // Get Connection String Section.
            ConnectionStringsSection connectionStringSection = config.ConnectionStrings;

            // Encrypt Connection String Section.
            if (!connectionStringSection.SectionInformation.IsProtected)
                connectionStringSection.SectionInformation.ProtectSection("DataProtectionConfigurationProvider");

            config.Save();
        }
    }
}
