using DataObjects;
using LogicLayer;
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

namespace WpfPresentationLayer
{
    /// <summary>
    /// Interaction logic for frmUpdateSupplierInvoice.xaml
    /// </summary>
    public partial class frmUpdateSupplierInvoice : Window
    {
        SupplierInvoice _invoice;
        ISupplierInvoiceManager _supplierInvoiceManager = new SupplierInvoiceManager();
        ISupplierManager _supplierManager = new SupplierManager();
        List<Supplier> _supplierList;

        /// <summary>
        /// Robert Forbes
        /// 2017/03/30
        /// 
        /// Initialize Update Supplier Invoice Window.
        /// </summary>
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/21
        /// 
        /// Standaridized method.
        /// </summary>
        /// <param name="invoice">The invoice to edit</param>
        public frmUpdateSupplierInvoice(SupplierInvoice invoice)
        {
            _invoice = invoice;
            InitializeComponent();
        }

        /// <summary>
        /// Robert Forbes
        /// 2017/03/30
        /// 
        /// Closes the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        /// <summary>
        /// Robert Forbes
        /// 2017/03/30
        /// 
        /// Attempts to update the invoice to use the newly input values.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {

            if (Validate() == true)
            {
                try
                {
                    SupplierInvoice newInvoice = new SupplierInvoice
                    {
                        SupplierInvoiceId = _invoice.SupplierInvoiceId,
                        SupplierId = ((Supplier)cboSupplier.SelectedItem).SupplierID,
                        InvoiceDate = (DateTime)dpInvoiceDate.SelectedDate,
                        SubTotal = decimal.Parse(txtSubTotal.Text),
                        TaxAmount = decimal.Parse(txtTaxAmount.Text),
                        Total = decimal.Parse(txtTotal.Text),
                        AmountPaid = decimal.Parse(txtAmountPaid.Text),
                        Approved = _invoice.Approved,
                        Active = _invoice.Active
                    };

                    if (_supplierInvoiceManager.UpdateSupplierInvoice(_invoice, newInvoice))
                    {
                        MessageBox.Show("Invoice Updated Successfully!");
                        this.Close();
                    }
                        
                        
                }
                catch
                {
                    MessageBox.Show("There was a problem communicating with the database");
                }
            }
            else
            {
                MessageBox.Show("One or more of the values you have entered are not valid");
            }

        }


        /// <summary>
        /// Robert Forbes
        /// 2017/03/30
        /// 
        /// Checks if the values entered into the fields are valid and adds error messages if they are not.
        /// </summary>
        /// <returns></returns>
        private bool Validate()
        {
            bool valid = true;
            //Supplier validation
            if(cboSupplier.SelectedItem == null){
                cboSupplier.BorderBrush = System.Windows.Media.Brushes.Red;
                cboSupplier.ToolTip = "You must select a supplier";
                valid = false;
            }
            else
            {
                cboSupplier.BorderBrush = System.Windows.Media.Brushes.Gray;
                cboSupplier.ToolTip = "";
            }

            //Invoice Date Validation
            if(dpInvoiceDate.SelectedDate == null){
                dpInvoiceDate.BorderBrush = System.Windows.Media.Brushes.Red;
                dpInvoiceDate.ToolTip = "You must select a valid date";
                valid = false;
            }
            else
            {
                dpInvoiceDate.BorderBrush = System.Windows.Media.Brushes.Gray;
                dpInvoiceDate.ToolTip = "";
            }


            //Decimal variable to hold values in try parse since it cant be used without outputting a value.
            decimal tempValueHolder;

            //Sub total validation.
            if(txtSubTotal.Text == ""){
                txtSubTotal.BorderBrush = System.Windows.Media.Brushes.Red;
                txtSubTotal.ToolTip = "You must enter a value for the sub total";
                valid = false;
            }else if(!decimal.TryParse(txtSubTotal.Text, out tempValueHolder)){
                txtSubTotal.BorderBrush = System.Windows.Media.Brushes.Red;
                txtSubTotal.ToolTip = "You must enter a valid value for the sub total";
                valid = false;
            }
            else
            {
                txtSubTotal.BorderBrush = System.Windows.Media.Brushes.Gray;
                txtSubTotal.ToolTip = "";
            }

            //Tax Amount Validation.
            if (txtTaxAmount.Text == "")
            {
                txtTaxAmount.BorderBrush = System.Windows.Media.Brushes.Red;
                txtTaxAmount.ToolTip = "You must enter a value for the Tax Amount";
                valid = false;
            }
            else if (!decimal.TryParse(txtTaxAmount.Text, out tempValueHolder))
            {
                txtTaxAmount.BorderBrush = System.Windows.Media.Brushes.Red;
                txtTaxAmount.ToolTip = "You must enter a valid value for the Tax Amount";
                valid = false;
            }
            else
            {
                txtTaxAmount.BorderBrush = System.Windows.Media.Brushes.Gray;
                txtTaxAmount.ToolTip = "";
            }

            //Total Validation.
            if (txtTotal.Text == "")
            {
                txtTotal.BorderBrush = System.Windows.Media.Brushes.Red;
                txtTotal.ToolTip = "You must enter a value for the Total";
                valid = false;
            }
            else if (!decimal.TryParse(txtTotal.Text, out tempValueHolder))
            {
                txtTotal.BorderBrush = System.Windows.Media.Brushes.Red;
                txtTotal.ToolTip = "You must enter a valid value for the Total";
                valid = false;
            }
            else
            {
                txtTotal.BorderBrush = System.Windows.Media.Brushes.Gray;
                txtTotal.ToolTip = "";
            }

            //Amount Paid Validation.
            if (txtAmountPaid.Text == "")
            {
                txtAmountPaid.BorderBrush = System.Windows.Media.Brushes.Red;
                txtAmountPaid.ToolTip = "You must enter a value for the Amount Paid";
                valid = false;
            }
            else if (!decimal.TryParse(txtAmountPaid.Text, out tempValueHolder))
            {
                txtAmountPaid.BorderBrush = System.Windows.Media.Brushes.Red;
                txtAmountPaid.ToolTip = "You must enter a valid value for the Amount Paid";
                valid = false;
            }
            else
            {
                txtAmountPaid.BorderBrush = System.Windows.Media.Brushes.Gray;
                txtAmountPaid.ToolTip = "";
            }

            return valid;
        }

        /// <summary>
        /// Robert Forbes
        /// 2017/03/30
        /// 
        /// Sets the default values for the fields in the window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmUpdateSuplierInvoice_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {

                _supplierList = _supplierManager.ListSuppliers();
                cboSupplier.ItemsSource = _supplierList;

                Supplier oldSupplier = _supplierList.Find(x => x.SupplierID.Equals(_invoice.SupplierId));
                cboSupplier.SelectedIndex = cboSupplier.Items.IndexOf(oldSupplier);
                txtTotal.Text = _invoice.Total.ToString();
                txtAmountPaid.Text = _invoice.AmountPaid.ToString();
                txtSubTotal.Text = _invoice.SubTotal.ToString();
                txtTaxAmount.Text = _invoice.TaxAmount.ToString();
                dpInvoiceDate.SelectedDate = _invoice.InvoiceDate;
            }
            catch
            {
                MessageBox.Show("There was a problem communicating with the database");
            }
        }
    }
}
