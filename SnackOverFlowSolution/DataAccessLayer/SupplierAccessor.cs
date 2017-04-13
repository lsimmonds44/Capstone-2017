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
        public static int CreateNewSupplier(int userId, bool isApproved, int approvedBy, string farmName, string farmAddress,
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
            cmd.Parameters.Add("@FARM_ADDRESS", SqlDbType.NVarChar, 300);
            cmd.Parameters.Add("@FARM_CITY", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@FARM_STATE", SqlDbType.NChar, 2);
            cmd.Parameters.Add("@FARM_TAX_ID", SqlDbType.NVarChar, 64);

            cmd.Parameters["@USER_ID"].Value = userId;
            cmd.Parameters["@IS_APPROVED"].Value = isApproved;
            cmd.Parameters["@APPROVED_BY"].Value = approvedBy;
            cmd.Parameters["@FARM_NAME"].Value = farmName;
            cmd.Parameters["@FARM_ADDRESS"].Value = farmAddress;
            cmd.Parameters["@FARM_CITY"].Value = farmCity;
            cmd.Parameters["@FARM_STATE"].Value = farmState;
            cmd.Parameters["@FARM_TAX_ID"].Value = farmTaxId;

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
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
        public static int ApplyForSupplierAccount(Supplier supplier)
        {
            //int userId, bool isApproved, string farmName, string farmAddress,
            //string farmCity, string farmState, string farmTaxId

            int rows = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_create_supplier_not_approved";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@USER_ID", SqlDbType.Int);
            cmd.Parameters.Add("@IS_APPROVED", SqlDbType.Bit);
            cmd.Parameters.Add("@FARM_NAME", SqlDbType.NVarChar, 300);
            cmd.Parameters.Add("@FARM_ADDRESS", SqlDbType.NVarChar, 300);
            cmd.Parameters.Add("@FARM_CITY", SqlDbType.NVarChar, 50);
            cmd.Parameters.Add("@FARM_STATE", SqlDbType.NChar, 2);
            cmd.Parameters.Add("@FARM_TAX_ID", SqlDbType.NVarChar, 64);

            cmd.Parameters["@USER_ID"].Value = supplier.UserId;
            cmd.Parameters["@IS_APPROVED"].Value = supplier.IsApproved;
            cmd.Parameters["@FARM_NAME"].Value = supplier.FarmName;
            cmd.Parameters["@FARM_ADDRESS"].Value = supplier.FarmAddress;
            cmd.Parameters["@FARM_CITY"].Value = supplier.FarmCity;
            cmd.Parameters["@FARM_STATE"].Value = supplier.FarmState;
            cmd.Parameters["@FARM_TAX_ID"].Value = supplier.FarmTaxID;

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                
                throw;
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
                        FarmAddress = reader.GetString(5),
                        FarmCity = reader.GetString(6),
                        FarmState = reader.GetString(7),
                        FarmTaxID = reader.GetString(8),
                        Active = reader.GetBoolean(9)
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
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }

            return s;
        }

        /// <summary>
        /// Christian Lopez
        /// 2017/02/22
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
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
                        FarmAddress = reader.GetString(5),
                        FarmCity = reader.GetString(6),
                        FarmState = reader.GetString(7),
                        FarmTaxID = reader.GetString(8),
                        Active = reader.GetBoolean(9)
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
            catch (Exception)
            {

                throw;
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
                            FarmAddress = reader.GetString(5),
                            FarmCity = reader.GetString(6),
                            FarmState = reader.GetString(7),
                            FarmTaxID = reader.GetString(8),
                            Active = reader.GetBoolean(9)
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
            catch (Exception)
            {

                throw;
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
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }

            return name;
        }
        /// <summary>
        /// Author: Skyler Hiscock
        /// Created: 2017/03/09
        /// </summary>
        /// <param name="oldSupplier"></param>
        /// <param name="newSupplier"></param>
        /// <returns></returns>

        public static int UpdateSupplier(Supplier oldSupplier, Supplier newSupplier)
        {
            var results = 0;
            var conn = DBConnection.GetConnection();
            var cmd = new SqlCommand(@"sp_update_supplier", conn);

            cmd.Parameters.Add("@old_SUPPLIER_ID", SqlDbType.Int);
            cmd.Parameters.Add("@old_USER_ID", SqlDbType.Int);
            cmd.Parameters.Add("@new_USER_ID", SqlDbType.Int);
            cmd.Parameters.Add("@old_IS_APPROVED", SqlDbType.Bit);
            cmd.Parameters.Add("@new_IS_APPROVED", SqlDbType.Bit);
            cmd.Parameters.Add("@old_APPROVED_BY", SqlDbType.Int);
            cmd.Parameters.Add("@new_APPROVED_BY", SqlDbType.Int);
            cmd.Parameters.Add("@old_FARM_NAME", SqlDbType.NVarChar);
            cmd.Parameters.Add("@new_FARM_NAME", SqlDbType.NVarChar);
            cmd.Parameters.Add("@old_FARM_ADDRESS", SqlDbType.NVarChar);
            cmd.Parameters.Add("@new_FARM_ADDRESS", SqlDbType.NVarChar);
            cmd.Parameters.Add("@old_FARM_CITY", SqlDbType.NVarChar);
            cmd.Parameters.Add("@new_FARM_CITY", SqlDbType.NVarChar);
            cmd.Parameters.Add("@old_FARM_STATE", SqlDbType.NChar);
            cmd.Parameters.Add("@new_FARM_STATE", SqlDbType.NChar);
            cmd.Parameters.Add("@old_FARM_TAX_ID", SqlDbType.NVarChar);
            cmd.Parameters.Add("@new_FARM_TAX_ID", SqlDbType.NVarChar);
            cmd.Parameters.Add("@old_ACTIVE", SqlDbType.Bit);
            cmd.Parameters.Add("@new_ACTIVE", SqlDbType.Bit);

            cmd.Parameters["@old_SUPPLIER_ID"].Value = oldSupplier.SupplierID;
            cmd.Parameters["@old_USER_ID"].Value = oldSupplier.UserId;
            cmd.Parameters["@new_USER_ID"].Value = newSupplier.UserId;
            cmd.Parameters["@old_IS_APPROVED"].Value = oldSupplier.IsApproved;
            cmd.Parameters["@new_IS_APPROVED"].Value = newSupplier.IsApproved;
            cmd.Parameters["@old_APPROVED_BY"].Value = oldSupplier.ApprovedBy;
            cmd.Parameters["@new_APPROVED_BY"].Value = newSupplier.ApprovedBy;
            cmd.Parameters["@old_FARM_NAME"].Value = oldSupplier.FarmName;
            cmd.Parameters["@new_FARM_NAME"].Value = newSupplier.FarmName;
            cmd.Parameters["@old_FARM_ADDRESS"].Value = oldSupplier.FarmAddress;
            cmd.Parameters["@new_FARM_ADDRESS"].Value = newSupplier.FarmAddress;
            cmd.Parameters["@old_FARM_CITY"].Value = oldSupplier.FarmCity;
            cmd.Parameters["@new_FARM_CITY"].Value = newSupplier.FarmCity;
            cmd.Parameters["@old_FARM_STATE"].Value = oldSupplier.FarmState;
            cmd.Parameters["@new_FARM_STATE"].Value = newSupplier.FarmState;
            cmd.Parameters["@old_FARM_TAX_ID"].Value = oldSupplier.FarmTaxID;
            cmd.Parameters["@new_FARM_TAX_ID"].Value = newSupplier.FarmTaxID;
            cmd.Parameters["@old_ACTIVE"].Value = oldSupplier.Active;
            cmd.Parameters["@new_ACTIVE"].Value = newSupplier.Active;

            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                conn.Open();
                results = cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            return results;
        }

        /// <summary>
        /// Ryan Spurgetis 
        /// 4/6/2017
        /// 
        /// Retrieves the list of supplier application statuses
        /// </summary>
        /// <returns>Application status list options</returns>
        public static List<string> RetrieveSupplierStatusList()
        {
            List<string> supplierAppStatus = new List<string>();
            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_supplier_application_status_list";
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
                        var appStatus = reader.GetString(0);

                        supplierAppStatus.Add(appStatus);
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

            return supplierAppStatus;
		}

        /// Christian Lopez
        /// 2017/04/06
        /// 
        /// Returns a list of suppliers with their agreements by gettin a list of all suppliers and then finding the 
        /// agreements associated.
        /// </summary>
        /// <returns></returns>
        public static List<SupplierWithAgreements> RetrieveAllSuppliersWithAgreements()
        {
            List<SupplierWithAgreements> suppliersWithAgreements = new List<SupplierWithAgreements>();

            try
            {
                List<Supplier> suppliers = RetrieveAllSuppliers();
                foreach (Supplier supplier in suppliers) {
                    SupplierWithAgreements s = new SupplierWithAgreements()
                    {
                        ID = supplier.SupplierID,
                        FarmAddress = supplier.FarmAddress,
                        FarmCity = supplier.FarmCity,
                        FarmName = supplier.FarmName,
                        FarmState = supplier.FarmState,
                        UserId = supplier.UserId,
                        FarmTaxID = supplier.FarmTaxID,
                        ApprovedBy = supplier.ApprovedBy,
                        IsApproved = supplier.IsApproved,
                        Active = supplier.Active,
                        Agreements = AgreementAccessor.RetrieveAgreementsWithProductNameBySupplierId(supplier.SupplierID)
                    };
                    suppliersWithAgreements.Add(s);
                }
            }
            catch (Exception)
            {
                
                throw;
            }

            return suppliersWithAgreements;
        }

        /// <summary>
        /// Bobby Thorne
        /// 4/7/2017
        /// 
        /// Accessor method to approve supplier and updates who made the change
        /// </summary>
        /// <param name="supplier"></param>
        /// <param name="approvedBy"></param>
        /// <returns></returns>
        public static int ApproveSupplier(Supplier supplier, int approvedBy)
        {
            int rows = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_update_supplier_approval";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("old_SUPPLIER_ID", supplier.SupplierID);
            cmd.Parameters.AddWithValue("approvedBy", approvedBy);
            cmd.Parameters.AddWithValue("isApproved", true);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }

            return rows;
        }

        /// <summary>
        /// Bobby Thorne
        /// 4/7/2017
        /// 
        /// Accessor method to deny supplier and updates who made the change
        /// </summary>
        /// <param name="supplier"></param>
        /// <param name="approvedBy"></param>
        /// <returns></returns>
        public static int DenySupplier(Supplier supplier, int approvedBy)
        {
            int rows = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_update_supplier_approval";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("old_SUPPLIER_ID", supplier.SupplierID);
            cmd.Parameters.AddWithValue("approvedBy", approvedBy);
            cmd.Parameters.AddWithValue("isApproved", false);

            try
            {
                conn.Open();
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }

            return rows;
        }


        /// <summary>
        /// Robert Forbes
        /// 2017/04/13
        /// 
        /// Method to retrieve the address of a supplier
        /// </summary>
        /// <param name="preferredAddressId"></param>
        /// <returns>The UserAddress Object for the given supplier</returns>
        public static UserAddress RetrieveSuppliersUserAddress(int? supplierId)
        {

            UserAddress userAddress = null;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_user_address_from_supplier_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SUPPLIER_ID", supplierId);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    userAddress = new UserAddress()
                    {
                        UserAddressId = reader.GetInt32(0),
                        UserId = reader.GetInt32(1),
                        AddressLineOne = reader.GetString(2),
                        AddressLineTwo = reader.GetString(3),
                        City = reader.GetString(4),
                        State = reader.GetString(5),
                        Zip = reader.GetString(6)
                    };
                }
                reader.Close();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }

            return userAddress;
        }


    }
}
