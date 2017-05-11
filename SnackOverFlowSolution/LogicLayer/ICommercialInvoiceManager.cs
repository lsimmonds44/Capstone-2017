using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    public interface ICommercialInvoiceManager
    {

        /// <summary>
        /// William Flood
        /// 2017/04/19
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        List<CommercialInvoice> RetrieveCommercialInvoiceByUserName(string userName);

        /// <summary>
        /// William Flood
        /// 2017/04/19
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        CommercialInvoice RetrieveCommercialInvoiceByInvoiceID(int invoiceId);

        /// <summary>
        /// William Flood
        /// 2017/04/20
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        List<CommercialInvoiceLine> RetrieveCommercialInvoiceLinesByInvoiceId(int invoiceId);

        /// <summary>
        /// William Flood
        /// 2017/04/20
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="taxRate"></param>
        /// <returns></returns>
        int CreateCustomerInvoice(int orderId, decimal taxRate);

    }
}
