using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Model
{
    public partial class CpmcContext
    {
        public string Machine
        {
            get
            {
                return Environment.MachineName;
            }
        }

        public Guid UserId
        {
            get
            {
                string userName = System.Threading.Thread.CurrentPrincipal.Identity.Name;
                User connectedUser = this.Users.SingleOrDefault(u => u.UserName == userName);
                if (connectedUser == null)
                    throw new System.Security.Authentication.AuthenticationException("You must be authenticated");
                return connectedUser.UserId;
            }
        }
        public override int SaveChanges()
        {
            List<Trace> traceList = new List<Trace>();

            try
            {
                // Get all changes made.
                var changes = this.ChangeTracker.Entries<IAuditable>().
                               Where(e => e.State == System.Data.Entity.EntityState.Added ||
                                     e.State == System.Data.Entity.EntityState.Modified ||
                                     e.State == System.Data.Entity.EntityState.Deleted).ToList();


                // create a trace object for each change.
                foreach (var stateEntryEntity in changes)
                {
                    // Get the trace object for auditing.
                    Trace trace = this.GetTrace(stateEntryEntity);

                    if (trace != null)
                        traceList.Add(trace);
                }

                //add all traces 
                if (traceList.Count > 0)
                    foreach (var trace in traceList)
                        this.Traces.Add(trace);
                return base.SaveChanges();
            } // Cacth any Exception due to Auditing Actions.
            catch (Exception ex)
            {                
                System.Diagnostics.Trace.TraceError(ex.Message);
            } 
            return -1;    //Something went wrong
        }


        #region Tracing Help methods

        private Trace GetTrace(DbEntityEntry entry)
        {
            // Ensure that Tracing is only for IAuditable entities.
            var entity = entry.Entity as IAuditable;

            if (entity == null)
                return null;

            #region Local Variables
            Guid entityId = Guid.Empty;
            string message = string.Empty;
            string entityKeyName = entry.GetEntityKeyPropertyName();
            string entitySet = entry.Entity.GetType().GetEntityTypeName();
            string parentEntitySet = null;
            object parentEntityId = null;
            DateTime now = DateTime.Now;
            #endregion

            Trace trace = new Trace();
            // set properties independent from entity information.
            trace.Id = Guid.NewGuid();
            trace.Date = now;
            trace.Machine = this.Machine;
            trace.UserId = this.UserId;

            if (entry.State == EntityState.Added)
            {
                entity.CreatedOn = now;
                entity.ModifiedOn = now;
                entity.CreatedBy = UserId;
                entity.ModifiedBy = UserId;
                entityId = (Guid)entry.Property(entry.GetEntityKeyPropertyName()).CurrentValue;
                trace.Action = AuditAction.Insert;

                trace.ActionName = string.Format("Insertion d'un(e) {0}", entitySet);

            }
            else if (entry.State == EntityState.Modified)
            {
                entity.ModifiedBy = UserId;
                entity.ModifiedOn = now;
                entityId = (Guid)entry.Property(entry.GetEntityKeyPropertyName()).CurrentValue;
                trace.Action = AuditAction.Update;

                trace.ActionName = string.Format("Modification des informations d'un(e) {0}", entitySet);
            }
            else if (entry.State == EntityState.Deleted)
            {
                entityId = (Guid)entry.Property(entry.GetEntityKeyPropertyName()).OriginalValue;
                trace.Action = AuditAction.Delete;

                trace.ActionName = string.Format("Suppresion d'un(e) {0}", entitySet);
            }


            if (entityId == Guid.Empty)
                throw new ArgumentException("Auditable entity must have valid Id !");

            trace.EntityId = entityId;
            trace.EntitySet = entitySet;
            entry.GetAuditDetail(out message, out parentEntitySet, out parentEntityId);

            trace.Message = message;
            trace.ParentEntitySet = parentEntitySet;
            trace.ParentEntityId = parentEntitySet == null ? null : (Guid?)parentEntityId;         

            return trace;
        }

        #endregion

    }

    public static class AuditExtensions
    {

        public static string GetEntityKeyPropertyName(this DbEntityEntry entry)
        {
            Type entityType = entry.Entity.GetType();

            // Use reflection to get properties.
            var properties = entityType.GetProperties();

            foreach (var property in properties)
            {
                // Use code first convention for searching Key property Name.
                if (property.Name.Equals("id", StringComparison.OrdinalIgnoreCase) || property.Name.Equals(entityType.GetEntityTypeName() + "id", StringComparison.OrdinalIgnoreCase))
                    return property.Name;
            }

            return null;
        }

        public static string GetEntityKeyPropertyName(this Type entityType)
        {
            // Use reflection to get properties.
            var properties = entityType.GetProperties();

            foreach (var property in properties)
            {
                // Use code first convention for searching Key property Name.
                if (property.Name.Equals("id", StringComparison.OrdinalIgnoreCase) || property.Name.Equals(entityType.GetEntityTypeName() + "id", StringComparison.OrdinalIgnoreCase))
                    return property.Name;
            }

            return null;
        }

        public static string GetEntityTypeName(this Type entityType)
        {
            // Get entity Type Name and avoid having Dynamic proxies as TypeName.
            string entityTypeName = entityType.FullName.Contains("System.Data.Entity.DynamicProxies") ? entityType.BaseType.Name : entityType.Name;

            return entityTypeName;
        }

        public static string ToAuditString(this DbEntityEntry entry)
        {
            StringBuilder sb = new StringBuilder();

            Type entityType = entry.Entity.GetType();

            // Use reflection to get properties.
            var properties = entityType.GetProperties();

            foreach (var property in properties)
            {
                try
                {
                    var propertyEntry = entry.Property(property.Name); // Get Property info for Primitive properties non primistive throw ArgumentException.

                    if (propertyEntry != null)
                    {
                        sb.AppendFormat("{0}='{1}';", propertyEntry.Name, entry.State == EntityState.Added ? propertyEntry.CurrentValue : entry.State == EntityState.Modified ? propertyEntry.CurrentValue : propertyEntry.OriginalValue);
                    }
                }
                catch (ArgumentException) // ArgumentException is thrown when the entry.Property is called for non primitive property type.
                {
                    continue;
                }
            }

            return sb.ToString();
        }

        public static void GetAuditDetail(this DbEntityEntry entry, out string message, out string parentEntitySet, out object parentEntityId)
        {
            StringBuilder sb = new StringBuilder();

            Type entityType = entry.Entity.GetType();

            string[] ignoredProperties = new string[] { "AppointmentBackground", "AllDay", "EndTime", "EndTimeZone", "IsRecursive", "IsSelected", "Location",
            "Notes","ObjectID","ReadOnly", "ReadOnlyVisibility", "RecurrenceID","RecurrenceProperites", "RecurrenceRule","ReminderTime", "ResourceCollection", 
            "StartTime", "StartTimeZone","Status", "Subject", "DependencyObjectType","Dispatcher","IsSealed"};

            parentEntitySet = null;
            parentEntityId = null;

            // Use reflection to get properties.
            var properties = entityType.GetProperties();

            foreach (var property in properties)
            {
                if (property.Name == "Error" || ignoredProperties.Contains(property.Name))
                    continue;
                try
                {
                    var propertyEntry = entry.Property(property.Name); // Get Property info for Primitive properties non primistive throw ArgumentException.

                    if (propertyEntry != null)
                    {
                        sb.AppendFormat("{0}='{1}';", propertyEntry.Name, entry.State == EntityState.Added ? propertyEntry.CurrentValue : entry.State == EntityState.Modified ? propertyEntry.CurrentValue : propertyEntry.OriginalValue);
                    }

                    continue;
                }
                catch (ArgumentException) // ArgumentException is thrown when the entry.Property is called for non primitive property type.
                {

                }

                try
                {
                    if (property.GetCustomAttributes(typeof(ParentAttribute), false).Count() <= 0)
                        continue;

                    var referenceEntry = entry.Reference(property.Name);

                    if (referenceEntry != null && referenceEntry.CurrentValue is IAuditable)
                    {
                        var parentType = referenceEntry.CurrentValue.GetType();
                        parentEntitySet = parentType.GetEntityTypeName();
                        parentEntityId = parentType.GetProperty(parentType.GetEntityKeyPropertyName()).GetValue(referenceEntry.CurrentValue);
                    }
                    continue;
                }
                catch (ArgumentException)
                {

                }
            }

            message = sb.ToString();
        }
    }
}
