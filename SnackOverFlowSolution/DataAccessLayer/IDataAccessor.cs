using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IDataAccessor
    {
        /**Fills the list of parameters for the command object in order to create a record
        */
        void SetCreateParameters(SqlCommand cmd);
        /**Provides the name of the stored procedure used to create a record
        */
        String CreateScript { get;}
        /**Fills the list of parameters for the command object needed to identify an entity
        */
        void SetKeyParameters(SqlCommand cmd);
        /**Provides the name of the stored procedure used to retrieve a single record
        */
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
        String UpdateScript { get; }
        /**Fills the list of parameters for the command object in order to update a record
        */
        void SetUpdateParameters(SqlCommand cmd);
        /**Provides the name of the stored procedure used to deactivate a record
        */
        String DeactivateScript { get;}
        /**Generates a data transfer object based on information from a reader
        */
        void ReadSingle(SqlDataReader reader);
        /**Generates a list of data transfer objects based on information from a reader
        */
        void ReadList(SqlDataReader reader);
    }
}
