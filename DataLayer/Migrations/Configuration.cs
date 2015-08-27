using System.Collections.Generic;
using DataLayer.Model;

namespace DataLayer.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CpmcContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;  
        }

        protected override void Seed(CpmcContext context)
        {
            ////Sexe Table
            //context.Sexes.Add(new Sexe() {SexeId = 0, Designation = "Male"});
            //context.Sexes.Add(new Sexe() { SexeId = 1, Designation = "Femelle" });

            ////Willaya Table

            //context.Willayas.Add(new Willaya() {WillayaId = 2, Designation = "Chlef"});
            //context.Willayas.Add(new Willaya() {WillayaId = 31, Designation = "Oran"});
            //context.Willayas.Add(new Willaya() {WillayaId = 16, Designation = "Alger"});
            //context.Willayas.Add(new Willaya() {WillayaId = 28, Designation = "Msilla"});
            //context.Willayas.Add(new Willaya() {WillayaId = 29, Designation = "Mascara"});

            ////Adress Table
            //IList<Adresse> fakeAdressesList = new List<Adresse>();
            //fakeAdressesList.Add(new Adresse(){AddressDesignation = "03, Rue Good Weather",City = "Elward",CodePosatal = "16000",WillayaId = 16});
            //fakeAdressesList.Add(new Adresse(){AddressDesignation = "9, Rue The Awosomenes",City = "City",CodePosatal = "29000",WillayaId = 29});
            //fakeAdressesList.Add(new Adresse(){AddressDesignation = "2, Rue Healthy people",City = "Madjd",CodePosatal = "31000",WillayaId = 31});
            //fakeAdressesList.Add(new Adresse(){AddressDesignation = "8, Rue Hadjar",City = "Nour",CodePosatal = "31500",WillayaId = 31});
            //fakeAdressesList.Add(new Adresse(){AddressDesignation = "201, Rue Boumediane",City = "Imran",CodePosatal = "2000",WillayaId = 2});
            //fakeAdressesList.Add(new Adresse(){AddressDesignation = "13, Rue El Nassir",City = "TellAvive",CodePosatal = "28790",WillayaId = 28});
            //fakeAdressesList.Add(new Adresse() { AddressDesignation = "07, Rue El modjahidine", City = "Faloudja", CodePosatal = "16300", WillayaId = 16 });

            //foreach (Adresse adr in fakeAdressesList)
            //    context.Adresses.Add(adr);

            ////Patient Table
            //IList<Patient> fakePatientsList = new List<Patient>();
            //fakePatientsList.Add(new Patient() { Nom = "Lahlou", Prenom = "Amine",SexeId = 0, DateDeNaissance = new DateTime(1989, 4, 13), TelephoneFixe = "021549865", TelephoneMobile1 = "0698626598", TelephoneMobile2 = "0772548798" });
            //fakePatientsList.Add(new Patient() { Nom = "Bentalha", Prenom = "Nadhir", SexeId = 0, DateDeNaissance = new DateTime(1988, 5, 23), TelephoneFixe = "021549865", TelephoneMobile1 = "0698626598", TelephoneMobile2 = "0772548798" });
            //fakePatientsList.Add(new Patient() { Nom = "Boumedian", Prenom = "Farid Djilali", SexeId = 0, DateDeNaissance = new DateTime(1990, 7, 22), TelephoneFixe = "021549865", TelephoneMobile1 = "0698626598", TelephoneMobile2 = "0772548798" });
            //fakePatientsList.Add(new Patient() { Nom = "Benmira", Prenom = "Ibtissam", SexeId = 1, DateDeNaissance = new DateTime(1978, 9, 3), TelephoneFixe = "021549865", TelephoneMobile1 = "0698626598", TelephoneMobile2 = "0772548798" });
            //fakePatientsList.Add(new Patient() { Nom = "Mansour", Prenom = "Ismail", SexeId = 0, DateDeNaissance = new DateTime(1996, 1, 9), TelephoneFixe = "021549865", TelephoneMobile1 = "0698626598", TelephoneMobile2 = "0772548798" });
            //fakePatientsList.Add(new Patient() { Nom = "Zahaf", Prenom = "Ahmed", SexeId = 0, DateDeNaissance = new DateTime(2005, 8, 16), TelephoneFixe = "021549865", TelephoneMobile1 = "0698626598", TelephoneMobile2 = "0772548798" });
            //fakePatientsList.Add(new Patient() { Nom = "Moumen", Prenom = "Radhia", SexeId = 1, DateDeNaissance = new DateTime(1993, 12, 7), TelephoneFixe = "021549865", TelephoneMobile1 = "0698626598", TelephoneMobile2 = "0772548798" });

            //foreach (Patient patient in fakePatientsList)
            //    context.Patients.Add(patient);
            base.Seed(context);
        }
    }
}
