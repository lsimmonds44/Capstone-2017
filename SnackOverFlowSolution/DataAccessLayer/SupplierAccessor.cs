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
    /// Created: 2017/02/02
    /// 
    /// Class to handle database interactions involving suppliers.
    /// </summary>
    public class SupplierAccessor
    {
        /// <summary>
        /// Christian Lopez
        /// Created: 2017/02/02
        /// 
        /// Access database to create a new supplier with given information
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 2017/04/21
        /// 
        /// Standardized method; changed signature from fields of a Supplier to a Supplier itself.
        /// </remarks>
        /// 
        /// <param name="supplier">The supplier to create.</param>
        /// <returns>Rows affected</returns>
        public static int CreateNewSupplier(Supplier supplier)
        {
            int rows = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_create_supplier";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@USER_ID", supplier.UserId);
            cmd.Parameters.AddWithValue("@IS_APPROVED", supplier.IsApproved);
            cmd.Parameters.AddWithValue("@APPROVED_BY", supplier.ApprovedBy);
            cmd.Parameters.AddWithValue("@FARM_NAME", supplier.FarmName);
            cmd.Parameters.AddWithValue("@FARM_ADDRESS", supplier.FarmAddress);
            cmd.Parameters.AddWithValue("@FARM_CITY", supplier.FarmCity);
            cmd.Parameters.AddWithValue("@FARM_STATE", supplier.FarmState);
            cmd.Parameters.AddWithValue("@FARM_TAX_ID", supplier.FarmTaxID);

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
        /// Created: 2017/02/15
        /// 
        /// Retrieve and return a supplier based on a given user ID
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 2017/04/21
        /// 
        /// Standardized method.
        /// </remarks>
        /// 
        /// <param name="userID">The user id of the supplier.</param>
        /// <returns>The supplier with the given user id.</returns>
        public static Supplier RetrieveSupplierByUserId(int userID)
        {
            Supplier supplier = null;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_supplier_by_user_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@USER_ID", userID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    supplier = new Supplier
                    {
                        SupplierID = reader.GetInt32(0),
                        UserId = reader.GetInt32(1),
                        IsApproved = reader.GetBoolean(2),
                        ApprovedBy = reader.IsDBNull(3) ? (int?)null : reader.GetInt32(3),
                        FarmName = reader.GetString(4),
                        FarmAddress = reader.GetString(5),
                        FarmCity = reader.GetString(6),
                        FarmState = reader.GetString(7),
                        FarmTaxID = reader.GetString(8),
                        Active = reader.GetBoolean(9)
                    };
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

            return supplier;
        }

        /// <summary>
        /// Christian Lopez
        /// Created: 2017/02/22
        /// Retrieves a given supplier from the database.
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 2017/04/21
        /// 
        /// Standardized method.
        /// </remarks>
        /// 
        /// <param name="supplierId">The id of the supplier.</param>
        /// <returns>The supplier.</returns>
        public static Supplier RetrieveSupplier(int supplierID)
        {
            Supplier supplier = null;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_supplier";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SUPPLIER_ID", supplierID);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    supplier = new Supplier
                    {
                        SupplierID = reader.GetInt32(0),
                        UserId = reader.GetInt32(1),
                        IsApproved = reader.GetBoolean(2),
                        ApprovedBy = reader.IsDBNull(3) ? (int?)null : reader.GetInt32(3),
                        FarmName = reader.GetString(4),
                        FarmAddress = reader.GetString(5),
                        FarmCity = reader.GetString(6),
                        FarmState = reader.GetString(7),
                        FarmTaxID = reader.GetString(8),
                        Active = reader.GetBoolean(9)
                    };
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

            return supplier;
        }

        /// <summary>
        /// Christian Lopez
        /// Created: 2017/03/03
        /// 
        /// Get a list of all suppliers, regardless of status and approval.
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 2017/04/21
        /// 
        /// Standardized method.
        /// </remarks>
        /// 
        /// <returns>A list of all suppliers in the database.</returns>
        public static List<Supplier> RetrieveSuppliers()
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
                        suppliers.Add(new Supplier
                        {
                            SupplierID = reader.GetInt32(0),
                            UserId = reader.GetInt32(1),
                            IsApproved = reader.GetBoolean(2),
                            ApprovedBy = reader.IsDBNull(3) ? (int?)null : reader.GetInt32(3),
                            FarmName = reader.GetString(4),
                            FarmAddress = reader.GetString(5),
                            FarmCity = reader.GetString(6),
                            FarmState = reader.GetString(7),
                            FarmTaxID = reader.GetString(8),
                            Active = reader.GetBoolean(9)
                        });
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
        /// Created: 2017/02/23
        /// 
        /// Retrieve the full supplier name by userId
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 2017/04/21
        /// 
        /// Standardized method.
        /// </remarks>
        /// 
        /// <param name="userId">The user id of the supplier.</param>
        /// <returns>The suppliers name.</returns>
        public static string RetrieveSupplierName(int userId)
        {
            string name = null;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_supplier_name";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@USER_ID", userId);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();

                    name = reader.GetString(0) + " " + reader.GetString(1);
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
            var rows = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_update_supplier";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@old_SUPPLIER_ID", oldSupplier.SupplierID);
            cmd.Parameters.AddWithValue("@old_USER_ID", oldSupplier.UserId);
            cmd.Parameters.AddWithValue("@new_USER_ID", newSupplier.UserId);
            cmd.Parameters.AddWithValue("@old_IS_APPROVED", oldSupplier.IsApproved);
            cmd.Parameters.AddWithValue("@new_IS_APPROVED", newSupplier.IsApproved);
            cmd.Parameters.AddWithValue("@old_APPROVED_BY", oldSupplier.ApprovedBy);
            cmd.Parameters.AddWithValue("@new_APPROVED_BY", newSupplier.ApprovedBy);
            cmd.Parameters.AddWithValue("@old_FARM_NAME", oldSupplier.FarmName);
            cmd.Parameters.AddWithValue("@new_FARM_NAME", newSupplier.FarmName);
            cmd.Parameters.AddWithValue("@old_FARM_ADDRESS", oldSupplier.FarmAddress);
            cmd.Parameters.AddWithValue("@new_FARM_ADDRESS", newSupplier.FarmAddress);
            cmd.Parameters.AddWithValue("@old_FARM_CITY", oldSupplier.FarmCity);
            cmd.Parameters.AddWithValue("@new_FARM_CITY", newSupplier.FarmCity);
            cmd.Parameters.AddWithValue("@old_FARM_STATE", oldSupplier.FarmState);
            cmd.Parameters.AddWithValue("@new_FARM_STATE", newSupplier.FarmState);
            cmd.Parameters.AddWithValue("@old_FARM_TAX_ID", oldSupplier.FarmTaxID);
            cmd.Parameters.AddWithValue("@new_FARM_TAX_ID", newSupplier.FarmTaxID);
            cmd.Parameters.AddWithValue("@old_ACTIVE", oldSupplier.Active);
            cmd.Parameters.AddWithValue("@new_ACTIVE", newSupplier.Active);

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
                List<Supplier> suppliers = RetrieveSuppliers();
                foreach (Supplier supplier in suppliers)
                {
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
                        UserId = reader.GetInt32(0),
                        AddressLineOne = reader.IsDBNull(1) ? null : reader.GetString(1),
                        AddressLineTwo = reader.IsDBNull(2) ? null : reader.GetString(2),
                        City = reader.IsDBNull(3) ? null : reader.GetString(3),
                        State = reader.IsDBNull(4) ? null : reader.GetString(4),
                        Zip = reader.IsDBNull(5) ? null : reader.GetString(5)
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

        /// <summary>
        /// Christian Lopez
        /// 2017/04/27
        /// Retrieves the necessary information and bundles it into a SupplierWithAgreemnts
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        public static SupplierWithAgreements RetrieveSupplierWithAggreementsBySupplierId(int supplierId)
        {
            SupplierWithAgreements supplier = null;

            try
            {
                Supplier s = RetrieveSupplier(supplierId);
                if (null == s)
                {
                    throw new ArgumentException("Unable to find supplier");
                }
                supplier = new SupplierWithAgreements()
                {
                    Active = s.Active,
                    ApprovedBy = s.ApprovedBy,
                    FarmAddress = s.FarmAddress,
                    FarmCity = s.FarmCity,
                    FarmName = s.FarmName,
                    FarmState = s.FarmState,
                    FarmTaxID = s.FarmTaxID,
                    SupplierID = s.SupplierID,
                    IsApproved = s.IsApproved,
                    UserId = s.UserId
                };
                List<Agreement> temp = AgreementAccessor.retrieveAgreementsBySupplierId(supplierId);
                List<AgreementWithProductName> agreements = new List<AgreementWithProductName>();
                foreach(Agreement a in temp) 
                {
                    AgreementWithProductName newAgrement = new AgreementWithProductName()
                    {
                        Active = a.Active,
                        AgreementId = a.AgreementId,
                        ApprovedBy = a.ApprovedBy,
                        DateSubmitted = a.DateSubmitted,
                        IsApproved = a.IsApproved,
                        ProductId = a.ProductId,
                        SupplierId = a.ProductId,
                        ProductName = ProductAccessor.RetrieveProduct(a.ProductId).Name
                    };
                    agreements.Add(newAgrement);
                }
                supplier.Agreements = agreements;
                
            }
            catch (Exception)
            {
                
                throw;
            }

            return supplier;
        }

        /// <summary>
        /// Christian Lopez
        /// 2017/04/27
        /// Retrieves the necessary information and bundles it into a SupplierWithAgreemnts
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static SupplierWithAgreements RetrieveSupplierWithAggreementsByUserId(int userId)
        {
            SupplierWithAgreements supplier = null;

            try
            {
                Supplier s = RetrieveSupplierByUserId(userId);
                if (null == s)
                {
                    throw new ArgumentException("Unable to find supplier");
                }
                supplier = new SupplierWithAgreements()
                {
                    Active = s.Active,
                    ApprovedBy = s.ApprovedBy,
                    FarmAddress = s.FarmAddress,
                    FarmCity = s.FarmCity,
                    FarmName = s.FarmName,
                    FarmState = s.FarmState,
                    FarmTaxID = s.FarmTaxID,
                    SupplierID = s.SupplierID,
                    IsApproved = s.IsApproved,
                    UserId = s.UserId
                };
                List<Agreement> temp = AgreementAccessor.retrieveAgreementsBySupplierId(supplier.SupplierID);
                List<AgreementWithProductName> agreements = new List<AgreementWithProductName>();
                foreach (Agreement a in temp)
                {
                    AgreementWithProductName newAgrement = new AgreementWithProductName()
                    {
                        Active = a.Active,
                        AgreementId = a.AgreementId,
                        ApprovedBy = a.ApprovedBy,
                        DateSubmitted = a.DateSubmitted,
                        IsApproved = a.IsApproved,
                        ProductId = a.ProductId,
                        SupplierId = a.ProductId,
                        ProductName = ProductAccessor.RetrieveProduct(a.ProductId).Name
                    };
                    agreements.Add(newAgrement);
                }
                supplier.Agreements = agreements;

            }
            catch (Exception)
            {

                throw;
            }

            return supplier;
        }


    }
}
