using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;
using DataAccessLayer;

namespace LogicLayer
{
    /// <summary>
    /// Christian Lopez
    /// 2017/03/22
    /// </summary>
    public class SupplierInvoiceManager : ISupplierInvoiceManager
    {

        /// <summary>
        /// Christian Lopez
        /// 2017/03/22
        /// 
        /// Retrieves a list of all supplier invoices, regardless of status
        /// </summary>
        /// <returns></returns>
        public List<SupplierInvoice> RetrieveAllSupplierInvoices()
        {
            try
            {
                return SupplierInvoiceAccessor.RetrieveSupplierInvoices();
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        /// <summary>
        /// Christian Lopez
        /// 2017/03/22
        /// 
        /// Attempts to retrieve a list of invoice lines by the given id
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        public List<SupplierInvoiceLine> RetrieveSupplierInvoiceLinesByInvoiceId(int invoiceId)
        {
            try
            {
                return SupplierInvoiceAccessor.RetrieveInvoiceLinesByInvoiceId(invoiceId);
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
