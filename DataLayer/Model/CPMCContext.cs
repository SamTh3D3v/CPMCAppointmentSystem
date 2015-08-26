using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    public class CpmcContext:DbContext
    {
        public CpmcContext()
            : base("CpmcAppointmentDb")  //"CpmcConnectionString"
        {
            //DeFault Db Initializer
            //Database.SetInitializer<CpmcContext>(new DropCreateDatabaseIfModelChanges<CpmcContext>());

            //Disable initializer
            //-> Database.SetInitializer<CpmcContext>(null);

            //Custom Db Inializer to populate Db With Fake Data
            //Database.SetInitializer<CpmcContext>(new CpmcDbInitializer());
            

            //-> By Using the DataMigration
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<CpmcContext, Migrations.Configuration>()); //"CpmcConnectionString"
        }
        public DbSet<Adresse> Adresses { get; set; }
        public DbSet<Medecin> Medecins { get; set; }
        public DbSet<Pathology> Pathologies { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<RendezVous> RendezVouses { get; set; }
        public DbSet<Specialite> Specialites { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Willaya> Willayas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }
    }
}
