using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    public partial class CpmcContext:DbContext
    {
        public CpmcContext()
            : base("CpmcContext")  //CpmcContext
        {
            //DeFault Db Initializer
            //Database.SetInitializer<CpmcContext>(new DropCreateDatabaseIfModelChanges<CpmcContext>());

            //Disable initializer
            // Database.SetInitializer<CpmcContext>(null);

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
        public DbSet<RolesCollection> RolesCollections { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Willaya> Willayas { get; set; }
        public DbSet<Sexe> Sexes { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<PieceJointeType> PieceJointeTypes { get; set; }
        public DbSet<PieceJointe> PieceJointes { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<JourFerie> JourFeries { get; set; }
        public DbSet<Trace> Traces { get; set; }
        public DbSet<SchedulerSetting> SchedulerSettings { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Parameter> Parameters { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Those props are needed in the UI no need to save em on the database cuz they can be calculated
            modelBuilder.Entity<RendezVous>().Ignore(r => r.AppointmentBackground);
            modelBuilder.Entity<RendezVous>().Ignore(r => r.AllDay);
            modelBuilder.Entity<RendezVous>().Ignore(r => r.EndTime);
            modelBuilder.Entity<RendezVous>().Ignore(r => r.EndTimeZone);
            modelBuilder.Entity<RendezVous>().Ignore(r => r.IsRecursive);
            modelBuilder.Entity<RendezVous>().Ignore(r => r.IsSelected);
            modelBuilder.Entity<RendezVous>().Ignore(r => r.Location);
            modelBuilder.Entity<RendezVous>().Ignore(r => r.Notes);
            modelBuilder.Entity<RendezVous>().Ignore(r => r.ObjectID);
            modelBuilder.Entity<RendezVous>().Ignore(r => r.ReadOnly);
            modelBuilder.Entity<RendezVous>().Ignore(r => r.ReadOnlyVisibility);
            modelBuilder.Entity<RendezVous>().Ignore(r => r.RecurrenceID);
            modelBuilder.Entity<RendezVous>().Ignore(r => r.RecurrenceProperites );
            modelBuilder.Entity<RendezVous>().Ignore(r => r.RecurrenceRule);
            modelBuilder.Entity<RendezVous>().Ignore(r => r.ReminderTime);
            modelBuilder.Entity<RendezVous>().Ignore(r => r.ResourceCollection);
            modelBuilder.Entity<RendezVous>().Ignore(r => r.StartTime);
            modelBuilder.Entity<RendezVous>().Ignore(r => r.StartTimeZone);
            modelBuilder.Entity<RendezVous>().Ignore(r => r.Status);
            modelBuilder.Entity<RendezVous>().Ignore(r => r.Subject);
            
            base.OnModelCreating(modelBuilder);
        }

      
    }
}
