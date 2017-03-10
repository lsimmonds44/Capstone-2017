using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IRetriever
    {
        String RetrieveSingleScript { get; }
        /**Provides the name of the stored procedure used to retrieve all records from a table
        */
        String RetrieveListScript { get; }
        /**Provides the name of the stored procedure used to retrieve records based on a search
        */
        String RetrieveSearchScript { get; }
        /**Fills the list of parameters for the command object in order to retrieve records based on a search
        */
        void SetRetrieveSearchParameters(SqlCommand cmd);
        /**Provides the name of the stored procedure used to update a record
        */
        void ReadSingle(SqlDataReader reader);
        /**Generates a list of data transfer objects based on information from a reader
        */
        void ReadList(SqlDataReader reader);
    }
}
