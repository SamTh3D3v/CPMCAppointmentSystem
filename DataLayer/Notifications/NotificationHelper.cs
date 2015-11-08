using DataLayer.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Notifications
{
    public class NotificationHelper
    {
        #region Fields

        private SqlConnection _connection;
        private SqlCommand _notificationsCommand;
        private SqlCommand _parametersCommand;
        private SqlDependency _notificationsDependency;
        private SqlDependency _parametersDependency;

        private string _connectionString;
        private Guid _userId;
        private bool _started;

        #endregion
        public NotificationHelper()
        {
            _connectionString = GetConnectionString();
            _connection = _connection ?? new SqlConnection(_connectionString);

            // initialize notifications command.
            _notificationsCommand = _notificationsCommand ?? new SqlCommand(@"usp_GetNotifications", _connection);
            _notificationsCommand.CommandType = System.Data.CommandType.StoredProcedure;
            _notificationsCommand.Notification = null;

            // initialize parameters command.
            _parametersCommand = _parametersCommand ?? new SqlCommand(@"usp_GetSyncedParameters", _connection);
            _parametersCommand.CommandType = System.Data.CommandType.StoredProcedure;
            _parametersCommand.Notification = null;
        }
        public void Start()
        {
            if (!_started)
            {
                _notificationsCommand.Parameters.AddWithValue("@UserId", DBNull.Value);

                // Starting SQL Server Query Notifications.
                SqlDependency.Stop(_connectionString);
                SqlDependency.Start(_connectionString);

                _started = true;// IMPORTANT
            }

            // Get the Initial Notifications Result set, after which changes will be tracked.
            List<Notification> initalNotificationsData = GetNotifications();

            // Firing Event to be Handled in UI App.
            if (NotificationsChange != null)
            {
                var args = new NotificationEventArgs<Notification>(initalNotificationsData);

                NotificationsChange(this, args);
            }

            // Get the Initial Synced Parameters.
            List<Parameter> initialSyncedParametersData = GetSyncedParameters();

            // Firing Event to be Handled in UI App.
            if (SyncedParametersChange != null)
            {
                var args = new NotificationEventArgs<Parameter>(initialSyncedParametersData);
                SyncedParametersChange(this, args);
            }

        }
        public void Start(Guid currentUserId)
        {
            if (!_started)
            {
                _userId = currentUserId;

                if (_userId == Guid.Empty)
                    _notificationsCommand.Parameters.AddWithValue("@UserId", DBNull.Value);
                else
                    _notificationsCommand.Parameters.AddWithValue("@UserId", _userId);

                // Starting SQL Server Query Notifications.
                SqlDependency.Stop(_connectionString);
                SqlDependency.Start(_connectionString);

                _started = true;// IMPORTANT
            }

            // Get the Initial Notifications Result set, after which changes will be tracked.
            List<Notification> initalNotificationsData = GetNotifications();

            // Firing Event to be Handled in UI App.
            if (NotificationsChange != null)
            {
                var args = new NotificationEventArgs<Notification>(initalNotificationsData);

                NotificationsChange(typeof(NotificationHelper), args);
            }

            // Get the Initial Synced Parameters.
            List<Parameter> initialSyncedParametersData = GetSyncedParameters();

            // Firing Event to be Handled in UI App.
            if (SyncedParametersChange != null)
            {
                var args = new NotificationEventArgs<Parameter>(initialSyncedParametersData);
                SyncedParametersChange(this, args);
            }
        }
        public void Stop()
        {
            // Stopping SQL Serevr Query Notifications.
            SqlDependency.Stop(GetConnectionString());
        }

        public event NotificationEventHandler<Notification> NotificationsChange;

        public event NotificationEventHandler<Parameter> SyncedParametersChange;

        #region Helper Methods
        private string GetConnectionString()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["CpmcContext"].ConnectionString;

            return connectionString;
        }
        private List<Notification> GetNotifications()
        {
            List<Notification> newResult = new List<Notification>();

            _notificationsCommand.Notification = null;

            // Register Dependency with the Command.
            _notificationsDependency = new SqlDependency(_notificationsCommand);
            _notificationsDependency.OnChange += this.OnNotificationsChange;


            // Get Actual Notifications
            _connection.Open();
            using (var reader = _notificationsCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    newResult.Add(new Notification
                    {
                        NotificationId = (Guid)reader["NotificationId"],
                        NotificationTitle = (string)reader["NotificationTitle"],
                        NotificationMessage = (string)reader["NotificationMessage"],
                        NotificationType = (TypeNotification)reader["NotificationType"],
                        NotifyUserId = reader["NotifyUserId"] == DBNull.Value ? null : (Guid?)reader["NotifyUserId"],
                        TypeUser = (TypeUser)reader["TypeUser"],
                        IsSystem = (bool)reader["IsSystem"],
                        CreatedOn = (DateTime)reader["CreatedOn"],
                        ModifiedOn = (DateTime)reader["ModifiedOn"],
                        IsActive = (bool)reader["IsActive"]
                    });
                }
            }

            _connection.Close();

            return newResult;
        }
        private List<Parameter> GetSyncedParameters()
        {
            List<Parameter> newResult = new List<Parameter>();

            _parametersCommand.Notification = null;

            // Register Dependency with the Command.
            _parametersDependency = new SqlDependency(_parametersCommand);
            _parametersDependency.OnChange += this.OnSyncedParametersChange;


            // Get Actual synced parameters.
            _connection.Open();
            using (var reader = _parametersCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    newResult.Add(new Parameter
                    {
                        Id = (Guid)reader["Id"],
                        Name = (string)reader["Name"],
                        Title = (string)reader["Title"],
                        Value = (string)reader["Value"],
                        SyncOnChange = (bool)reader["SyncOnChange"]
                    });
                }
            }

            _connection.Close();

            return newResult;
        }
        private void OnSyncedParametersChange(object sender, SqlNotificationEventArgs e)
        {
            SqlDependency dependency = (SqlDependency)sender;

            // Unregister dependency.
            dependency.OnChange -= this.OnSyncedParametersChange;

            // Get new Notifications Data (After Changes Done).
            List<Parameter> newResult = GetSyncedParameters();

            // Firing Event to be Handled in UI App.
            if (SyncedParametersChange != null)
            {
                var args = new NotificationEventArgs<Parameter>(newResult);

                SyncedParametersChange(this, args);
            }
        }
        private void OnNotificationsChange(object sender, SqlNotificationEventArgs e)
        {
            SqlDependency dependency = (SqlDependency)sender;

            // Unregister dependency.
            dependency.OnChange -= this.OnNotificationsChange;

            // Get new Notifications Data (After Changes Done).
            List<Notification> newResult = GetNotifications();

            // Firing Event to be Handled in UI App.
            if (NotificationsChange != null)
            {
                var args = new NotificationEventArgs<Notification>(newResult);

                NotificationsChange(typeof(NotificationHelper), args);
            }
        }

        #endregion
    }
}
