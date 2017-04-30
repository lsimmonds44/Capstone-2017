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
            var connString = @"Data Source=LAPTOP-61UA4Q4T\SQLEXPRESS;Initial Catalog=SnackOverflowDB;Integrated Security=True"; // conn string for laptop

            // var connString = @"Data Source=WIN-I4H924MDOL3\SQLEXPRESS;Initial Catalog=vehicleBuddyDB;Integrated Security=True"; // conn string for home iMac

            var conn = new SqlConnection(connString);
            return conn;
        }
    }
}
