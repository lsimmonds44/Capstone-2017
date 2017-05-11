using DataAccessLayer;
using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
    
{
    /// <summary>
    /// Ariel Sigo
    /// Updated;
    /// 2017/04/29
    /// 
    /// Commercial Invoice Manager class
    /// </summary>
    public class CommercialInvoiceManager : ICommercialInvoiceManager
    {
        /// <summary>
        /// Ariel Sigo
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public List<CommercialInvoice> RetrieveCommercialInvoiceByUserName(string userName)
        {
            try
            {
                return CommercialInvoiceAccessor.RetrieveCommercialInvoicesByUserName(userName);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {

                throw new ApplicationException("There was a database error.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was an unknown error.", ex);
            }
        }


        public List<CommercialInvoiceLine> RetrieveCommercialInvoiceLinesByInvoiceId(int invoiceId)
        {
            try
            {
                return CommercialInvoiceAccessor.RetrieveInvoiceLinesByInvoiceId(invoiceId);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {

                throw new ApplicationException("There was a database error.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was an unknown error.", ex);
            }
        }


        public CommercialInvoice RetrieveCommercialInvoiceByInvoiceID(int invoiceId)
        {
            try
            {
                return CommercialInvoiceAccessor.RetrieveCommercialInvoicesByInvoiceID(invoiceId);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {

                throw new ApplicationException("There was a database error.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("There was an unknown error.", ex);
            }
        }


        public int CreateCustomerInvoice(int orderId, decimal taxRate)
        {
            try
            {
                return CommercialInvoiceAccessor.CreateCustomerInvoice(orderId, taxRate);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occured", ex);
            }
        }
    }
}
