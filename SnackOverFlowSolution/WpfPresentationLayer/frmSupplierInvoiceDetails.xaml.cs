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
            lblInvoiceId.Content = _supplierInvoice.SupplierInvoiceId;
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

                ErrorMessage(ex);
            }

            try
            {
                _invoiceLines = _supplierInvoiceManager.RetrieveSupplierInvoiceLinesByInvoiceId(_supplierInvoice.SupplierInvoiceId);
            }
            catch (Exception ex)
            {

                ErrorMessage(ex);
            }

            dgSupplierInvoiceLines.ItemsSource = _invoiceLines;
            
        }

        private static void ErrorMessage(Exception ex)
        {
            MessageBox.Show("Error: " + ex.Message);
        }
    }
}
