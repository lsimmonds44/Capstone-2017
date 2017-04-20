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
        /// Christian Lopez
        /// 2017/03/22
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        List<CommercialInvoice> RetrieveCommercialInvoiceByUserName(string userName);

    }
}
