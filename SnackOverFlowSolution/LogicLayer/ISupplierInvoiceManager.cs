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
    }
}
