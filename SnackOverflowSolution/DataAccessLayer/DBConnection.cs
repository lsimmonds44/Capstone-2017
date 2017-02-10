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
            var connString = @"Data Source=nh229cf;Initial Catalog=SnackOverflowDB;Integrated Security=True"; // conn string for class computer
            //var connString = @"Data Source=MBE-PC;Initial Catalog=SnackOverflowDB;Integrated Security=True"; // conn string for laptop

            // var connString = @"Data Source=WIN-I4H924MDOL3\SQLEXPRESS;Initial Catalog=vehicleBuddyDB;Integrated Security=True"; // conn string for home iMac
            var conn = new SqlConnection(connString);
            return conn;
        }
    }
}
