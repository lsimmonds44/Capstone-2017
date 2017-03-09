using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IDataAccessor : IRetriever
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
        String UpdateScript { get; }
        /**Fills the list of parameters for the command object in order to update a record
        */
        void SetUpdateParameters(SqlCommand cmd);
        /**Provides the name of the stored procedure used to deactivate a record
        */
        String DeactivateScript { get;}
        /**Generates a data transfer object based on information from a reader
        */
    }
}
