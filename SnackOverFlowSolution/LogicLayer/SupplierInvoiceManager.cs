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

        /// <summary>
        /// Christian Lopez
        /// 2017/03/23
        /// 
        /// Attempts to approve the invoice with the given invoice id
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        public bool ApproveSupplierInvoice(int invoiceId)
        {
            bool result = false;

            try
            {
                if (1 == SupplierInvoiceAccessor.UpdateApproveSupplierInvoice(invoiceId))
                {
                    result = true;
                }
            }
            catch (Exception)
            {
                
                throw;
            }

            return result;
        }

        /// <summary>
        /// Christian Lopez
        /// 2017/03/29
        /// 
        /// Creates an invoice and returns the id associated
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>
        public int CreateSupplierInvoice(SupplierInvoice invoice)
        {
            int id = 1;
            try
            {
                id = SupplierInvoiceAccessor.CreateSupplierInvoice(invoice);
                if (id == 0)
                {
                    // We were not able to capture the new id
                    throw new ApplicationException("Unable to retrieve the new id number.");
                }
            }
            catch (Exception)
            {
                
                throw;
            }
            return id;
        }

        /// <summary>
        /// Christian Lopez
        /// 2017/03/29
        /// 
        /// Adds a supplier invoice line
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public bool CreateSupplierInvoiceLine(SupplierInvoiceLine line)
        {
            try
            {
                return (1 == SupplierInvoiceAccessor.CreateSupplierInvoiceLine(line));
            }
            catch (Exception)
            {
                
                throw;
            }
        }



        /// <summary>
        /// Robert Forbes
        /// 2017/03/30
        /// 
        /// Updates the old invoice in the database so that it has the values of the new invoice
        /// </summary>
        /// <param name="oldInvoice">The original invoice</param>
        /// <param name="newInvoice">The invoice with updated values</param>
        /// <returns></returns>
        public bool UpdateSupplierInvoice(SupplierInvoice oldInvoice, SupplierInvoice newInvoice)
        {
            try
            {
                return (1 == SupplierInvoiceAccessor.UpdateSupplierInvoice(oldInvoice, newInvoice));
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
