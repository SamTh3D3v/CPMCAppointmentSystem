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
        private SqlCommand _command;
        private SqlDependency _dependency;
        private string _connectionString;
        private Guid _userId;

        #endregion      
        public NotificationHelper()
        {
            _connectionString = GetConnectionString();
            _connection = _connection ?? new SqlConnection(_connectionString);
            _command = _command ?? new SqlCommand(@"usp_GetNotifications", _connection);
            _command.CommandType = System.Data.CommandType.StoredProcedure;

           

            _command.Notification = null;
        }
        public void Start()
        {                       
            // Starting SQL Server Query Notifications.
            SqlDependency.Stop(_connectionString);
            SqlDependency.Start(_connectionString);            

            // Get the Initial Notifications Result set, after which changes will be tracked.
            List<Notification> initalNotificationsData = GetNotifications();

            // Firing Event to be Handled in UI App.
            if (NotificationsChange != null)
            {
                var args = new NotificationEventArgs<Notification>(initalNotificationsData);

                NotificationsChange(typeof(NotificationHelper), args);
            }
        }

        public void Start(Guid currentUserId)
        {
            _userId = currentUserId;

            if (_userId == Guid.Empty)
                _command.Parameters.AddWithValue("@UserId", DBNull.Value);
            else
                _command.Parameters.AddWithValue("@UserId", _userId);

            // Starting SQL Server Query Notifications.
            SqlDependency.Stop(_connectionString);
            SqlDependency.Start(_connectionString);

            // Get the Initial Notifications Result set, after which changes will be tracked.
            List<Notification> initalNotificationsData = GetNotifications();

            // Firing Event to be Handled in UI App.
            if (NotificationsChange != null)
            {
                var args = new NotificationEventArgs<Notification>(initalNotificationsData);

                NotificationsChange(typeof(NotificationHelper), args);
            }            
        }
        public void Stop()
        {
            // Stopping SQL Serevr Query Notifications.
            SqlDependency.Stop(GetConnectionString());
        }

        public event NotificationEventHandler<Notification> NotificationsChange;        

        #region Helper Methods 
        private void OnChange(object sender, SqlNotificationEventArgs e)
        {
            SqlDependency dependency = (SqlDependency)sender;

            // Unregister dependency.
            dependency.OnChange -= this.OnChange;

            // Get new Notifications Data (After Changes Done).
            List<Notification> newResult = GetNotifications();

            // Firing Event to be Handled in UI App.
            if (NotificationsChange != null)
            {
                var args = new NotificationEventArgs<Notification>(newResult);

                NotificationsChange(typeof(NotificationHelper), args);
            }
        }
        private string GetConnectionString()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["CpmcContext"].ConnectionString;

            return connectionString;
        }
        private List<Notification> GetNotifications()
        {
            List<Notification> newResult = new List<Notification>();

            _command.Notification = null;
            
            // Register Dependency with the Command.
            _dependency = new SqlDependency(_command);            
            _dependency.OnChange += this.OnChange;
                  

            // Get Actual Notifications
            _connection.Open();
            using (var reader = _command.ExecuteReader())
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
        
        #endregion
    }
}
