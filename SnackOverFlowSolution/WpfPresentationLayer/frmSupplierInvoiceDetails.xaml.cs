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

        public frmSupplierInvoiceDetails(SupplierInvoice supplierInvoice, ISupplierInvoiceManager supplierInvoiceManager, ISupplierManager supplierManager)
        {
            _supplierInvoice = supplierInvoice;
            _supplierInvoiceManager = supplierInvoiceManager;
            _supplierManager = supplierManager;
            _invoiceLines = new List<SupplierInvoiceLine>();
            InitializeComponent();
        }

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

                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }

            try
            {
                _invoiceLines = _supplierInvoiceManager.RetrieveSupplierInvoiceLinesByInvoiceId(_supplierInvoice.SupplierInvoiceId);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
            }

            dgSupplierInvoiceLines.ItemsSource = _invoiceLines;
            
        }


        /// <summary>
        /// Christian Lopez
        /// 2017/03/23
        /// 
        /// Approve the current invoice, and return to the selection screen
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

                    MessageBox.Show(ex.Message + "\n\n" + ex.InnerException.Message);
                }
            }
            else
            {
                MessageBox.Show("The invoice is already approved.");
            }
            
        }
    }
}
