using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    public class CpmcDbInitializer: DropCreateDatabaseAlways<CpmcContext>
    {
            protected override void Seed(CpmcContext context)
            {
                IList<Patient> fakePatientsList = new List<Patient>();

                fakePatientsList.Add(new Patient(){Nom = "Lahlou",Prenom = "Amine",DateDeNaissance = new DateTime(1989,4,13),TelephoneFixe = "021549865",TelephoneMobile1 = "0698626598",TelephoneMobile2 = "0772548798"});
                fakePatientsList.Add(new Patient() { Nom = "Bentalha", Prenom = "Nadhir", DateDeNaissance = new DateTime(1988, 5, 23), TelephoneFixe = "021549865", TelephoneMobile1 = "0698626598", TelephoneMobile2 = "0772548798" });
                fakePatientsList.Add(new Patient() { Nom = "Boumedian", Prenom = "Farid Djilali", DateDeNaissance = new DateTime(1990, 7, 22), TelephoneFixe = "021549865", TelephoneMobile1 = "0698626598", TelephoneMobile2 = "0772548798" });
                fakePatientsList.Add(new Patient() { Nom = "Benmira", Prenom = "Ibtissam", DateDeNaissance = new DateTime(1978, 9, 3), TelephoneFixe = "021549865", TelephoneMobile1 = "0698626598", TelephoneMobile2 = "0772548798" });
                fakePatientsList.Add(new Patient() { Nom = "Mansour", Prenom = "Ismail", DateDeNaissance = new DateTime(1996, 1, 9), TelephoneFixe = "021549865", TelephoneMobile1 = "0698626598", TelephoneMobile2 = "0772548798" });
                fakePatientsList.Add(new Patient() { Nom = "Zahaf", Prenom = "Ahmed", DateDeNaissance = new DateTime(2005, 8, 16), TelephoneFixe = "021549865", TelephoneMobile1 = "0698626598", TelephoneMobile2 = "0772548798" });
                fakePatientsList.Add(new Patient() { Nom = "Moumen", Prenom = "Radhia", DateDeNaissance = new DateTime(1993, 12, 7), TelephoneFixe = "021549865", TelephoneMobile1 = "0698626598", TelephoneMobile2 = "0772548798" });                

                foreach (Patient patient in fakePatientsList)
                    context.Patients.Add(patient);                
                base.Seed(context);
            }
        }        
}
