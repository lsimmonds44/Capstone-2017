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
    /// Interaction logic for frmSubmitSupplierInvoice.xaml
    /// </summary>
    public partial class frmSubmitSupplierInvoice : Window
    {
        private Supplier _supplier;
        private IProductLotManager _productLotManager;
        private ISupplierInvoiceManager _supplierInvoiceManager;
        private List<ProductLot> _productLots = new List<ProductLot>();
        private List<SupplierInvoiceLine> _invoiceLines = new List<SupplierInvoiceLine>();

        private decimal invoiceSubtotalCost = 0.0m;
        private const double taxRate = .07;
        private decimal invoiceTax = 0.0m;
        private decimal invoiceTotalCost = 0.0m;

        public frmSubmitSupplierInvoice(Supplier supplier, IProductLotManager productLotManager, ISupplierInvoiceManager supplierInvoiceManager)
        {
            _supplier = supplier;
            _productLotManager = productLotManager;
            _supplierInvoiceManager = supplierInvoiceManager;
            InitializeComponent();
        }

        /// <summary>
        /// Christian Lopez
        /// 2017/03/29
        /// Limits the text to only be characters and '.'
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPriceEach_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            forceDigit(e);
        }

        /// <summary>
        /// Christian Lopez
        /// 2017/03/29
        /// 
        /// Force digit or '.'
        /// </summary>
        /// <param name="e"></param>
        private static void forceDigit(TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1) && !(e.Text.Equals(".")))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// Christian Lopez
        /// 2017/03/29
        /// 
        /// Quick validation to make sure the input is good
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPriceEach_LostFocus(object sender, RoutedEventArgs e)
        {
            enforceTextValidation(txtPriceEach);
        }

        /// <summary>
        /// Christian Lopez
        /// 2017/03/29
        /// 
        /// Quick validation to make sure the input is good
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDiscount_LostFocus(object sender, RoutedEventArgs e)
        {
            enforceTextValidation(txtDiscount);
        }

        /// <summary>
        /// Christian Lopez
        /// 2017/03/29
        /// 
        /// Helper method to validate monetary values in textboxes
        /// </summary>
        /// <param name="txtBox"></param>
        private void enforceTextValidation(TextBox txtBox)
        {
            if (txtBox.Text != "")
            {
                decimal price;
                try
                {
                    // convert to decimal
                    price = Decimal.Parse(txtBox.Text);

                    // roudn to 2 decimal places
                    price = Decimal.Round(price, 2, MidpointRounding.AwayFromZero);

                    // use comma notation and force two decimals
                    txtBox.Text = price.ToString("0,0.00");

                    if (txtBox.Text[0].Equals("0"))
                    {
                        txtBox.Text.Remove(0, 1);
                    }
                }
                catch (Exception)
                {

                    txtBox.Text = "0.00";
                    MessageBox.Show("Invalid input");
                }
            }
        }

        /// <summary>
        /// Christian Lopez
        /// 2017/03/29
        /// 
        /// Enforce what user's can and cannot enter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDiscount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            forceDigit(e);
        }

        /// <summary>
        /// Christian Lopez
        /// 2017/03/29
        /// 
        /// Handles the set up for the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _productLots = _productLotManager.RetrieveProductLotsBySupplier(_supplier);
                dgSupplierProductLots.ItemsSource = _productLots;
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

        /// <summary>
        /// Christian Lopez
        /// 2017/03/29
        /// 
        /// Move an item from the product lot table to the invoice line table
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddToInvoice_Click(object sender, RoutedEventArgs e)
        {
            if (dgSupplierProductLots.SelectedIndex >= 0)
            {
                ProductLot productLot = (ProductLot)dgSupplierProductLots.SelectedItem;
                SupplierInvoiceLine invoiceLine = new SupplierInvoiceLine()
                {
                    ProductLotId = (int)productLot.ProductLotId,
                    QuantitySold = (int)productLot.Quantity
                };
                try
                {
                    invoiceLine.PriceEach = Decimal.Parse(txtPriceEach.Text);
                    invoiceLine.ItemDiscount = Decimal.Parse(txtDiscount.Text);
                }
                catch (Exception)
                {
                    
                    MessageBox.Show("Unable to convert to decimal");
                }
                invoiceLine.ItemTotal = (invoiceLine.PriceEach * invoiceLine.QuantitySold - invoiceLine.ItemDiscount);
                _invoiceLines.Add(invoiceLine);
                _productLots.Remove(productLot);
                updateDataGrids();
                txtDiscount.Text = "0.00";
                txtPriceEach.Text = "0.00";
                invoiceSubtotalCost += invoiceLine.ItemTotal;
                invoiceTax = Decimal.Round(invoiceSubtotalCost * (decimal)taxRate, 2, MidpointRounding.AwayFromZero);
                invoiceTotalCost = invoiceSubtotalCost + invoiceTax;
                lblSubtotalValue.Content = invoiceSubtotalCost.ToString();
                lblTaxAmount.Content = invoiceTax.ToString();
                lblTotalAmount.Content = invoiceTotalCost.ToString();
            }
        }

        /// <summary>
        /// Christian Lopez
        /// 2017/03/29
        /// 
        /// Update the datagrids
        /// </summary>
        private void updateDataGrids()
        {
            dgSupplierInvoiceLots.ItemsSource = null;
            dgSupplierInvoiceLots.ItemsSource = _invoiceLines;
            dgSupplierProductLots.ItemsSource = null;
            dgSupplierProductLots.ItemsSource = _productLots;
        }

        /// <summary>
        /// Christian Lopez
        /// 2017/03/29
        /// 
        /// Remove the selected item from the invoice line back to the product lots
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemoveFromInvoice_Click(object sender, RoutedEventArgs e)
        {
            if (dgSupplierInvoiceLots.SelectedIndex >= 0)
            {
                SupplierInvoiceLine invoiceLine = (SupplierInvoiceLine)dgSupplierInvoiceLots.SelectedItem;
                try
                {
                    _productLots.Add(_productLotManager.RetrieveProductLotById(invoiceLine.ProductLotId));
                    _invoiceLines.Remove(invoiceLine);
                    updateDataGrids();
                    invoiceSubtotalCost -= invoiceLine.ItemTotal;
                    invoiceTax = Decimal.Round(invoiceSubtotalCost * (decimal)taxRate, 2, MidpointRounding.AwayFromZero);
                    invoiceTotalCost = invoiceSubtotalCost + invoiceTax;
                    lblSubtotalValue.Content = invoiceSubtotalCost.ToString();
                    lblTaxAmount.Content = invoiceTax.ToString();
                    lblTotalAmount.Content = invoiceTotalCost.ToString();
                }
                catch (Exception ex)
                {
                    if (null != ex.InnerException)
                    {
                        MessageBox.Show("Unable to remove the invoice line at this time. Error: " + ex.Message + "\n\n" + ex.InnerException.Message);
                    }
                    else
                    {
                        MessageBox.Show("Unable to remove the invoice line at this time. Error: " + ex.Message);
                    }
                    
                }
            }
            
        }

        /// <summary>
        /// Christian Lopez
        /// 2017/03/29
        /// 
        /// Creates the supplier invoice and the invoice lines
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (_invoiceLines.Count > 0)
            {
                SupplierInvoice invoice = new SupplierInvoice()
                {
                    SupplierId = _supplier.SupplierID,
                    InvoiceDate = DateTime.Now,
                    SubTotal = invoiceSubtotalCost,
                    TaxAmount = invoiceTax,
                    Total = invoiceTotalCost,
                    AmountPaid = 0
                };
                int invoiceID;
                try
                {
                    invoiceID = _supplierInvoiceManager.CreateSupplierInvoice(invoice);
                    foreach (SupplierInvoiceLine item in _invoiceLines)
                    {
                        item.SupplierInvoiceId = invoiceID;
                        if (!_supplierInvoiceManager.CreateSupplierInvoiceLine(item))
                        {
                            MessageBox.Show("Error adding invoice");
                            break;
                        }
                    }
                    this.DialogResult = true;
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
                MessageBox.Show("Please add invoice items to submit.");
            }
        }
    }
}
