using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonDataAccess
{
    internal static class DBConnection
    {
        public static SqlConnection getDBConnection()
        {
            String conString = @"Data Source=localhost;Initial Catalog=BillsPC;
                Integrated Security=True;Connect Timeout=15;
                Encrypt=False;TrustServerCertificate=False";
            SqlConnection conn = new SqlConnection(conString);
            /*Should be only place in application where connection string appears!
             * Should be only method any class uses to create a database connection.
             * This ensures database string updates only ever need to be done in one place*/
            return conn;
        }
    }
}
