using DataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    /// <summary>
    /// Christian Lopez
    /// Created on 2017/02/02
    /// Accesses the database for information regarding Suppliers
    /// </summary>
    public class SupplierAccessor
    {
        /// <summary>
        /// Christian Lopez
        /// Created on 2017/02/02
        /// 
        /// Access database to store a new Supplier with given information
        /// </summary>
        /// <param name="userId">ID to associate the Supplier with</param>
        /// <param name="isApproved">Wether or not it is approved</param>
        /// <param name="approvedBy">The employee ID that approved the request</param>
        /// <param name="farmName">The name of the farm</param>
        /// <param name="farmCity">The city of the farm</param>
        /// <param name="farmState">The state of the farm</param>
        /// <param name="farmTaxId">The tax ID</param>
        /// <returns>A 1 if successful</returns>
        /// <remarks>Last modified by Christian Lopez on 2017/02/02</remarks>
        public static int CreateNewSupplier(int userId, bool isApproved, int approvedBy, string farmName,
            string farmCity, string farmState, string farmTaxId)
        {
            int rows = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_create_supplier";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@USER_ID", SqlDbType.Int);
            cmd.Parameters.Add("@IS_APPROVED", SqlDbType.Bit);
            cmd.Parameters.Add("@APPROVED_BY", SqlDbType.Int);
            cmd.Parameters.Add("@FARM_NAME", SqlDbType.NVarChar, 300);
            cmd.Parameters.Add("@FARM_CITY", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@FARM_STATE", SqlDbType.NChar, 2);
            cmd.Parameters.Add("@FARM_TAX_ID", SqlDbType.NVarChar, 64);

            cmd.Parameters["@USER_ID"].Value = userId;
            cmd.Parameters["@IS_APPROVED"].Value = isApproved;
            cmd.Parameters["@APPROVED_BY"].Value = approvedBy;
            cmd.Parameters["@FARM_NAME"].Value = farmName;
            cmd.Parameters["@FARM_CITY"].Value = farmCity;
            cmd.Parameters["@FARM_STATE"].Value = farmState;
            cmd.Parameters["@FARM_TAX_ID"].Value = farmTaxId;

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw new ApplicationException("There was a problem saving to the Database: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }


            return rows;
        }

        /// <summary>
        /// Christian Lopez
        /// Created 2017/03/02
        /// 
        /// Connection to DB to store an applied for supplier account.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="isApproved"></param>
        /// <param name="farmName"></param>
        /// <param name="farmCity"></param>
        /// <param name="farmState"></param>
        /// <param name="farmTaxId"></param>
        /// <returns></returns>
        public static int ApplyForSupplierAccount(int userId, bool isApproved, string farmName,
            string farmCity, string farmState, string farmTaxId)
        {
            int rows = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_create_supplier_not_approved";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@USER_ID", SqlDbType.Int);
            cmd.Parameters.Add("@IS_APPROVED", SqlDbType.Bit);
            cmd.Parameters.Add("@FARM_NAME", SqlDbType.NVarChar, 300);
            cmd.Parameters.Add("@FARM_CITY", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@FARM_STATE", SqlDbType.NChar, 2);
            cmd.Parameters.Add("@FARM_TAX_ID", SqlDbType.NVarChar, 64);

            cmd.Parameters["@USER_ID"].Value = userId;
            cmd.Parameters["@IS_APPROVED"].Value = isApproved;
            cmd.Parameters["@FARM_NAME"].Value = farmName;
            cmd.Parameters["@FARM_CITY"].Value = farmCity;
            cmd.Parameters["@FARM_STATE"].Value = farmState;
            cmd.Parameters["@FARM_TAX_ID"].Value = farmTaxId;

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return rows;
        }

        /// <summary>
        /// Christian Lopez
        /// Created on 2017/02/15
        /// 
        /// Retrieve and return a supplier based on a given user ID
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static Supplier RetrieveSupplierByUserId(int userId)
        {
            Supplier s = null;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_supplier_by_user_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@USER_ID", SqlDbType.Int);
            cmd.Parameters["@USER_ID"].Value = userId;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    s = new Supplier
                    {
                        SupplierID = reader.GetInt32(0),
                        UserId = reader.GetInt32(1),
                        IsApproved = reader.GetBoolean(2),
                        //ApprovedBy = reader.GetInt32(3),
                        FarmName = reader.GetString(4),
                        FarmCity = reader.GetString(5),
                        FarmState = reader.GetString(6),
                        FarmTaxID = reader.GetString(7),
                        Active = reader.GetBoolean(8)
                    };
                    if (!reader.IsDBNull(3))
                    {
                        s.ApprovedBy = reader.GetInt32(3);
                    }
                    else
                    {
                        s.ApprovedBy = null;
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Error connecting to DB: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return s;
        }

        public static Supplier RetrieveSupplierBySupplierId(int supplierId)
        {
            Supplier s = null;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_supplier";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@SUPPLIER_ID", SqlDbType.Int);
            cmd.Parameters["@SUPPLIER_ID"].Value = supplierId;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    s = new Supplier
                    {
                        SupplierID = reader.GetInt32(0),
                        UserId = reader.GetInt32(1),
                        IsApproved = reader.GetBoolean(2),
                        //ApprovedBy = reader.GetInt32(3),
                        FarmName = reader.GetString(4),
                        FarmCity = reader.GetString(5),
                        FarmState = reader.GetString(6),
                        FarmTaxID = reader.GetString(7),
                        Active = reader.GetBoolean(8)
                    };
                    if (!reader.IsDBNull(3))
                    {
                        s.ApprovedBy = reader.GetInt32(3);
                    }
                    else
                    {
                        s.ApprovedBy = null;
                    }
                }
                reader.Close();
            }
            catch (Exception ex)
            {

                throw new ApplicationException("Error connecting to DB: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return s;
        }

        /// <summary>
        /// Christian Lopez
        /// Created 2017/03/03
        /// 
        /// Get a list of all suppliers, regardless of status and approval.
        /// </summary>
        /// <returns></returns>
        public static List<Supplier> RetrieveAllSuppliers()
        {
            List<Supplier> suppliers = new List<Supplier>();
            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_supplier_list";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Supplier s = new Supplier
                        {
                            SupplierID = reader.GetInt32(0),
                            UserId = reader.GetInt32(1),
                            IsApproved = reader.GetBoolean(2),
                            FarmName = reader.GetString(4),
                            FarmCity = reader.GetString(5),
                            FarmState = reader.GetString(6),
                            FarmTaxID = reader.GetString(7),
                            Active = reader.GetBoolean(8)
                        };
                        if (!reader.IsDBNull(3))
                        {
                            s.ApprovedBy = reader.GetInt32(3);
                        }
                        else
                        {
                            s.ApprovedBy = null;
                        }
                        suppliers.Add(s);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return suppliers;

        }


        /// <summary>
        /// Christian Lopez
        /// Created 2017/02/23
        /// 
        /// Retrieve the supplier name by userId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static string RetrieveSupplierName(int userId)
        {
            string name = null;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_supplier_name";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@USER_ID", SqlDbType.Int);
            cmd.Parameters["@USER_ID"].Value = userId;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();

                    name = reader.GetString(0) + " " + reader.GetString(1);
                }
                reader.Close();
            }
            catch (Exception ex)
            {

                throw new ApplicationException("There was an error reaching the database: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return name;
        }
    }
}
