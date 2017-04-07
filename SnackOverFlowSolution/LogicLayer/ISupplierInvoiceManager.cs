using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataObjects;

namespace LogicLayer
{
    /// <summary>
    /// Christian Lopez
    /// 2017/03/22
    /// 
    /// Interface for logic regarding supplier invoices and their line entries
    /// </summary>
    public interface ISupplierInvoiceManager
    {
        /// <summary>
        /// Christian Lopez
        /// 2017/03/22
        /// </summary>
        /// <returns></returns>
        List<SupplierInvoice> RetrieveAllSupplierInvoices();

        /// <summary>
        /// Christian Lopez
        /// 2017/03/22
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        List<SupplierInvoiceLine> RetrieveSupplierInvoiceLinesByInvoiceId(int invoiceId);

        /// <summary>
        /// Christian Lopez
        /// 2017/03/23
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        bool ApproveSupplierInvoice(int invoiceId);

        /// <summary>
        /// Christian Lopez
        /// 2017/03/29
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>
        int CreateSupplierInvoice(SupplierInvoice invoice);

        /// <summary>
        /// Christian Lopez
        /// 2017/03/29
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        bool CreateSupplierInvoiceLine(SupplierInvoiceLine line);


        /// <summary>
        /// Robert Forbes
        /// 2017/03/30
        /// 
        /// </summary>
        /// <param name="oldInvoice">The original invoice</param>
        /// <param name="newInvoice">The invoice with updated values</param>
        /// <returns></returns>
        bool UpdateSupplierInvoice(SupplierInvoice oldInvoice, SupplierInvoice newInvoice);

        /// <summary>
        /// Victor Algarin
        /// 2017/04/06
        /// 
        /// </summary>
        /// <param name="invoice">The selected invoice to be deleted</param>
        /// <returns>true (if query from accessor is successful)</returns>
        bool DeleteSupplierInvoice(SupplierInvoice invoice);
    }
}
