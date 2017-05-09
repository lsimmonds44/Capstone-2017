using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using System.Data.SqlClient;
using System.Data;

namespace DataAccessLayer
{
    /// <summary>
    /// Christian Lopez
    /// 
    /// Created:
    /// 2017/04/13
    /// 
    /// Handles data access for company orders and lines
    /// </summary>
    public class CompanyOrderAccessor
    {
        /// <summary>
        /// Christian Lopez
        /// 
        /// Created:
        /// 2017/04/13
        /// 
        /// Returns a list of all company orders in the DB
        /// </summary>
        /// <returns></returns>
        public static List<CompanyOrder> RetrieveAllCompanyOrders()
        {
            List<CompanyOrder> orders = new List<CompanyOrder>();
            
            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_company_order_list";
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
                        CompanyOrder order = new CompanyOrder()
                        {
                            CompanyOrderID = reader.GetInt32(0),
                            EmployeeId = reader.GetInt32(1),
                            SupplierId = reader.GetInt32(2),
                            Amount = reader.GetDecimal(3),
                            OrderDate = reader.GetDateTime(4),
                            HasArrived = reader.GetBoolean(5),
                            Active = reader.GetBoolean(6),
                            SupplierName = reader.GetString(7)
                        };
                        orders.Add(order);
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


            return orders;
        }

        /// <summary>
        /// Christian Lopez
        /// 
        /// Created:
        /// 2017/04/13
        /// 
        /// Returns a list of company orders placed to a specified supplier Id
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        public static List<CompanyOrder> RetrieveCompanyOrdersBySupplierId(int supplierId)
        {
            List<CompanyOrder> orders = new List<CompanyOrder>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_company_order_list_by_supplier_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@SUPPLIER_ID", supplierId);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        CompanyOrder order = new CompanyOrder()
                        {
                            CompanyOrderID = reader.GetInt32(0),
                            EmployeeId = reader.GetInt32(1),
                            SupplierId = reader.GetInt32(2),
                            Amount = reader.GetDecimal(3),
                            OrderDate = reader.GetDateTime(4),
                            HasArrived = reader.GetBoolean(5),
                            Active = reader.GetBoolean(6)
                        };
                        orders.Add(order);
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

            return orders;
        }

        /// <summary>
        /// Christian Lopez
        /// 
        /// Created:
        /// 2017/04/13
        /// 
        /// Retrieves a list of lines associated with a particular order
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public static List<CompanyOrderLine> RetrieveCompanyOrderLinesByOrderId(int orderId)
        {
            List<CompanyOrderLine> lines = new List<CompanyOrderLine>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_company_order_lines_by_order_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@COMPANY_ORDER_ID", orderId);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        CompanyOrderLine line = new CompanyOrderLine()
                        {
                            CompanyOrderID = reader.GetInt32(0),
                            ProductId = reader.GetInt32(1),
                            ProductName = reader.GetString(2),
                            Quantity = reader.GetInt32(3),
                            UnitPrice = reader.GetDecimal(4),
                            TotalPrice = reader.GetDecimal(5)
                        };
                        lines.Add(line);
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

            return lines;
        }

        /// <summary>
        /// Christian Lopez
        /// 
        /// Created:
        /// 2017/04/13
        /// 
        /// Binds the lines to their orders
        /// </summary>
        /// <returns></returns>
        public static List<CompanyOrderWithLines> RetrieveAllCompanyOrdersWithLines()
        {
            List<CompanyOrderWithLines> orders = new List<CompanyOrderWithLines>();

            try
            {
                List<CompanyOrder> data = RetrieveAllCompanyOrders();
                foreach (CompanyOrder c in data)
                {
                    CompanyOrderWithLines newOrder = new CompanyOrderWithLines()
                    {
                        CompanyOrderID = c.CompanyOrderID,
                        EmployeeId = c.EmployeeId,
                        SupplierId = c.SupplierId,
                        OrderDate = c.OrderDate,
                        Amount = c.Amount,
                        HasArrived = c.HasArrived,
                        Active = c.Active,
                        OrderLines = RetrieveCompanyOrderLinesByOrderId(c.CompanyOrderID)
                    };
                    orders.Add(newOrder);
                }
            }
            catch (Exception)
            {
                
                throw;
            }

            return orders;
        }

        /// <summary>
        /// Christian Lopez
        /// 
        /// Created:
        /// 2017/04/13
        /// 
        /// Retrieves bundled order and lines for a supplier id
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        public static List<CompanyOrderWithLines> RetrieveCompanyOrdersWithLinesBySupplierId(int supplierId)
        {
            List<CompanyOrderWithLines> orders = new List<CompanyOrderWithLines>();

            try
            {
                List<CompanyOrder> data = RetrieveCompanyOrdersBySupplierId(supplierId);
                foreach (CompanyOrder c in data)
                {
                    CompanyOrderWithLines newOrder = new CompanyOrderWithLines()
                    {
                        CompanyOrderID = c.CompanyOrderID,
                        EmployeeId = c.EmployeeId,
                        SupplierId = c.SupplierId,
                        OrderDate = c.OrderDate,
                        Amount = c.Amount,
                        HasArrived = c.HasArrived,
                        Active = c.Active,
                        OrderLines = RetrieveCompanyOrderLinesByOrderId(c.CompanyOrderID)
                    };
                    orders.Add(newOrder);
                }
            }
            catch (Exception)
            {
                
                throw;
            }


            return orders;
        }

        /// <summary>
        /// Christian Lopez
        /// 
        /// Created:
        /// 2017/04/13
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public static CompanyOrder RetrieveOrderByOrderId(int orderId)
        {
            CompanyOrder order = null;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_company_order_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@COMPANY_ORDER_ID", orderId);

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    order = new CompanyOrder()
                    {
                        CompanyOrderID = reader.GetInt32(0),
                        EmployeeId = reader.GetInt32(1),
                        SupplierId = reader.GetInt32(2),
                        Amount = reader.GetDecimal(3),
                        OrderDate = reader.GetDateTime(4),
                        HasArrived = reader.GetBoolean(5),
                        Active = reader.GetBoolean(6)
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

            return order;
        }

        /// <summary>
        /// Christian Lopez
        /// 
        /// Created:
        /// 2017/04/13
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public static CompanyOrderWithLines RetrieveOrderWithLinesByOrderId(int orderId)
        {
            CompanyOrderWithLines order = null;

            try
            {
                CompanyOrder data = RetrieveOrderByOrderId(orderId);
                order = new CompanyOrderWithLines()
                {
                    CompanyOrderID = data.CompanyOrderID,
                    EmployeeId = data.EmployeeId,
                    SupplierId = data.SupplierId,
                    Amount = data.Amount,
                    HasArrived = data.HasArrived,
                    OrderDate = data.OrderDate,
                    Active = data.Active,
                    OrderLines = RetrieveCompanyOrderLinesByOrderId(orderId)
                };
            }
            catch (Exception)
            {
                
                throw;
            }

            return order;
        }


        /// <summary>
        /// Robert Forbes
        /// 
        /// Created:
        /// 2017/04/30
        /// 
        /// Updates the has arrived bit field in the company order table
        /// </summary>
        /// <param name="companyOrderId">The company order to update</param>
        /// <param name="oldHasArrived">The current value of the has arrived bit field</param>
        /// <param name="newHasArrived">The value to change the has arrived bit field to</param>
        /// <returns>The number of rows affected</returns>
        public static int UpdateCompanyOrderHasArrived(int companyOrderId, bool oldHasArrived, bool newHasArrived)
        {
            int result = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_update_company_order_has_arrived";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@COMPANY_ORDER_ID", companyOrderId);
            cmd.Parameters.AddWithValue("@old_HAS_ARRIVED", oldHasArrived);
            cmd.Parameters.AddWithValue("@new_HAS_ARRIVED", newHasArrived);

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }

            return result;
        }
    }
}
