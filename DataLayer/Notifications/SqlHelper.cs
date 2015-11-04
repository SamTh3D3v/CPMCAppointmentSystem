using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Notifications
{
    public class SqlHelper
    {
        private SqlConnection GetConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["CpmcContext"].ConnectionString;

            return new SqlConnection(connectionString);
        }

        public void StartNotification()
        {
            SqlConnection connection = GetConnection();
            SqlCommand command = new SqlCommand("usp_GetNotifications", connection);
            SqlDependency dependency = new SqlDependency(command);            
        }
    }
}
