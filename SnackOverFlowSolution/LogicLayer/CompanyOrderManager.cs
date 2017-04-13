using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayer;
using System.Data.SqlClient;

namespace LogicLayer
{
    /// <summary>
    /// Christian Lopez
    /// 2017/04/13
    /// 
    /// Handles the business logic regarding company orders
    /// </summary>
    public class CompanyOrderManager : ICompanyOrderManager
    {
        /// <summary>
        /// Christian Lopez
        /// 2017/04/13
        /// </summary>
        /// <returns></returns>
        public List<CompanyOrder> RetrieveCompanyOrders()
        {
            try
            {
                return CompanyOrderAccessor.RetrieveAllCompanyOrders();
            }
            catch (SqlException sqlEx)
            {

                throw new ApplicationException("There was a database error.", sqlEx);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was an unknown error.", ex);
            }
        }

        /// <summary>
        /// Christian Lopez
        /// 2017/04/13
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        public List<CompanyOrder> RetrieveCompanyOrdersBySupplierId(int supplierId)
        {
            try
            {
                return CompanyOrderAccessor.RetrieveCompanyOrdersBySupplierId(supplierId);
            }
            catch (SqlException sqlEx)
            {

                throw new ApplicationException("There was a database error.", sqlEx);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was an unknown error.", ex);
            }
        }

        /// <summary>
        /// Christian Lopez
        /// 2017/04/13
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public List<CompanyOrderLine> RetrieveCompanyOrderLinesByOderId(int orderId)
        {
            try
            {
                return CompanyOrderAccessor.RetrieveCompanyOrderLinesByOrderId(orderId);
            }
            catch (SqlException sqlEx)
            {

                throw new ApplicationException("There was a database error.", sqlEx);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was an unknown error.", ex);
            }
        }

        /// <summary>
        /// Christian Lopez
        /// 2017/04/13
        /// </summary>
        /// <returns></returns>
        public List<CompanyOrderWithLines> RetrieveCompanyOrdersWithLines()
        {
            try
            {
                return CompanyOrderAccessor.RetrieveAllCompanyOrdersWithLines();
            }
            catch (SqlException sqlEx)
            {

                throw new ApplicationException("There was a database error.", sqlEx);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was an unknown error.", ex);
            }
        }

        /// <summary>
        /// Christian Lopez
        /// 2017/04/13
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        public List<CompanyOrderWithLines> RetrieveCompanyOrdersWithLinesBySupplierId(int supplierId)
        {
            try
            {
                return CompanyOrderAccessor.RetrieveCompanyOrdersWithLinesBySupplierId(supplierId);
            }
            catch (SqlException sqlEx)
            {

                throw new ApplicationException("There was a database error.", sqlEx);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was an unknown error.", ex);
            }
        }
    }
}
