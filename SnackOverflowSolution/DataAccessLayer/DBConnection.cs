using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    class DBConnection
    {
        internal static SqlConnection GetConnection()
        {
            // this should be the only place in your 
            // app the connection string  can be found.
            var connString = @"Data Source=localhost;Initial Catalog=SnackOverflowDB;Integrated Security=True"; // conn string for localhost
            var conn = new SqlConnection(connString);
            return conn;
        }
    }
}
