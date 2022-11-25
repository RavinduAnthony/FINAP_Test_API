using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementSystem_test.DAL
{
    public class Dbaccess
    {
        SqlConnection sqlConnection = new SqlConnection();
        public SqlConnection getConnection(IConfiguration configuration) 
        {
            string connection = ConfigurationExtensions.GetConnectionString(configuration, "MainConnection");
            sqlConnection = new SqlConnection(connection);
            sqlConnection.Open();
            return sqlConnection;
        }
        public void closeConnection() 
        {
            sqlConnection.Close();
        }
    }
}
