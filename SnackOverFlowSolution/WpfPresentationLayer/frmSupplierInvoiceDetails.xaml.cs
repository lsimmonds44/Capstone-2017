using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DataObjects;
using LogicLayer;

namespace WpfPresentationLayer
{
    /// <summary>
    /// Interaction logic for frmSupplierInvoiceDetails.xaml
    /// </summary>
    public partial class frmSupplierInvoiceDetails : Window
    {
        ISupplierInvoiceManager _supplierInvoiceManager;
        ISupplierManager _supplierManager;
        SupplierInvoice _supplierInvoice;
        List<SupplierInvoiceLine> _invoiceLines;

       /// <summary>
       /// Alissa Duffy
       /// Updated: 2017/04/21
       /// 
       /// Initialize the Supplier Invoice Details Window.
       /// Standaridized method.  
       /// </summary>
       /// </summary>
       /// <param name="supplierInvoice"></param>
       /// <param name="supplierInvoiceManager"></param>
       /// <param name="supplierManager"></param>
        public frmSupplierInvoiceDetails(SupplierInvoice supplierInvoice, ISupplierInvoiceManager supplierInvoiceManager, ISupplierManager supplierManager)
        {
            _supplierInvoice = supplierInvoice;
            _supplierInvoiceManager = supplierInvoiceManager;
            _supplierManager = supplierManager;
            _invoiceLines = new List<SupplierInvoiceLine>();
            InitializeComponent();
        }

        /// <summary>
        /// Bobby Thorne
        /// 2017/04/14
        /// 
        /// OverLoad method to view user's own invoices
        /// </summary>
        /// <param name="supplierInvoice"></param>
        /// <param name="supplierInvoiceManager"></param>
        /// <param name="supplierManager"></param>
        /// <param name="purpose"></param>
        public frmSupplierInvoiceDetails(SupplierInvoice supplierInvoice, ISupplierInvoiceManager supplierInvoiceManager, ISupplierManager supplierManager, string purpose)
        {
            // TODO: Complete member initialization
            _supplierInvoice = supplierInvoice;
            _supplierInvoiceManager = supplierInvoiceManager;
            _supplierManager = supplierManager;
            _invoiceLines = new List<SupplierInvoiceLine>();
            InitializeComponent();
            if (purpose == "ReadOnly")
            {
                btnApprove.Visibility = Visibility.Hidden;
            }
        }
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/21
        /// 
        /// Loads the Supplier Invoice Details Window.
        /// Standaridized method.  
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lblInvoiceId.Content = "Invoice " + _supplierInvoice.SupplierInvoiceId;
            lblTotalAmount.Content = _supplierInvoice.Total.ToString("c");
            lblAmountPaidAmount.Content = _supplierInvoice.AmountPaid.ToString("c");
            Supplier supplierAssociated = null;
            try
            {
                supplierAssociated = _supplierManager.RetrieveSupplierBySupplierID(_supplierInvoice.SupplierId);
                if (null != supplierAssociated)
                {
                    lblSupplierName.Content = _supplierManager.RetrieveSupplierName(supplierAssociated.UserId);
                    lblFarmName.Content = supplierAssociated.FarmName;
                }
            }
            catch (Exception ex)
            {

                if (null != ex.InnerException)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }
                else
                {
                    MessageBox.Show(ex.Message);
                }
            }

            try
            {
                _invoiceLines = _supplierInvoiceManager.RetrieveSupplierInvoiceLinesByInvoiceId(_supplierInvoice.SupplierInvoiceId);
            }
            catch (Exception ex)
            {

                if (null != ex.InnerException)
                {
                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }
                else
                {
                    MessageBox.Show(ex.Message);
                }
            }

            dgSupplierInvoiceLines.ItemsSource = _invoiceLines;
            
        }


        /// <summary>
        /// Christian Lopez
        /// 2017/03/23
        /// 
        /// Approve the current invoice, and return to the selection screen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnApprove_Click(object sender, RoutedEventArgs e)
        {
            if (!_supplierInvoice.Approved)
            {
                try
                {
                    if (_supplierInvoiceManager.ApproveSupplierInvoice(_supplierInvoice.SupplierId))
                    {
                        MessageBox.Show("Approved Supplier Invoice.");
                        this.DialogResult = true;
                    }
                    else
                    {
                        MessageBox.Show("Unable to approve the invoice.");
                    }
                }
                catch (Exception ex)
                {

                    if (null != ex.InnerException)
                    {
                        MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                    }
                    else
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("The invoice is already approved.");
            }
            
        }
    }
}
