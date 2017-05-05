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
    /// Interaction logic for frmApproval.xaml
    /// 
    /// Need to add overload statements for frm 
    /// </summary>
    public partial class frmApproval : Window
    {
        Charity _charity;
        ICharityManager _charityMgr;

        Supplier _supplier;
        ISupplierManager _supplierMgr;

        CommercialCustomer _commercialCustomer;
        ICustomerManager _customerMgr;

        int _userid;

        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/24
        /// 
        /// Initialize Approval form Window for a supplier manager.
        /// Standardized methods.
        /// </summary>
        /// <param name="supplierMrg"></param>
        /// <param name="supplier"></param>
        /// <param name="userid"></param>
        public frmApproval(ISupplierManager supplierMrg, Supplier supplier, int userid)
        {
            _supplier = supplier;
            _supplierMgr = supplierMrg;
            InitializeComponent();
            txtID.Text = _supplier.SupplierID.ToString();
            txtName.Text = _supplier.FarmName.ToString();
            _userid = userid;
        }

        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/24
        /// 
        /// Initialize Approval form Window for a customer manager.
        /// Standardized methods.
        /// </summary>
        /// </summary>
        /// <param name="customerMrg"></param>
        /// <param name="commercialCustomer"></param>
        /// <param name="userid"></param>
        public frmApproval(ICustomerManager customerMrg, CommercialCustomer commercialCustomer, int userid)
        {
            _commercialCustomer = commercialCustomer;
            _customerMgr = customerMrg;
            InitializeComponent();
            txtID.Text = _commercialCustomer.CommercialId.ToString();
            lblName.Content = "UserId";
            txtName.Text = _commercialCustomer.UserId.ToString();
            _userid = userid;
        }

        private void btnApprove_Click(object sender, RoutedEventArgs e)
        {
            
            if (_supplierMgr != null)
            {
                if (_supplier.IsApproved)
                {
                    MessageBox.Show("Cannot re-add supplier, Supplier already approved", "Supplier already approved", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    this.Close();
                }
                else
                {
                    try
                    {
                        if (_supplierMgr.ApproveSupplier(_supplier, _userid))
                        {
                            MessageBox.Show("Supplier approved.", "System Updated", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        }
                        else
                        {
                            MessageBox.Show("Supplier record not altered", "Oops, no record was modified", MessageBoxButton.OK, MessageBoxImage.Hand);
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("There was an error approving this record. Please try again later", "Oops, something went wrong", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    try
                    {
                        ((MainWindow)Owner).createAgreementForApprovedSupplier(_supplier);
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("An error occurred on creating agreement for new supplier. " + ex.Message);
                    }
                }
                
            }

            if (_customerMgr != null)
            {
                try
                {
                    if (_customerMgr.ApproveCommercialCustomer(_commercialCustomer, _userid))
                    {
                        MessageBox.Show("Commercial Customer approved.", "System Updated", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                    else
                    {
                        MessageBox.Show("Commercial Customer record not altered", "Oops, no record was modified", MessageBoxButton.OK, MessageBoxImage.Hand);
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("There was an error approving this record. Please try again later", "Oops, something went wrong", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            this.Close();
        }
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/24
        /// 
        /// Deny Approval.
        /// Standardized methods.
        /// </summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeny_Click(object sender, RoutedEventArgs e)
        {
            if (_supplier != null)
            {
                try
                {
                    if (_supplierMgr.DenySupplier(_supplier, _userid))
                    {
                        MessageBox.Show("Supplier Denied.", "System Updated", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                    else
                    {
                        MessageBox.Show("Supplier record not altered", "Oops, no record was modified", MessageBoxButton.OK, MessageBoxImage.Hand);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("There was an error approving this record. Please try again later" + Environment.NewLine + ex, "Oops, something went wrong", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                MessageBox.Show("Supplier not approved.", "System Updated", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                this.Close();
            }

            if (_commercialCustomer != null)
            {
                try
                {
                    if (_customerMgr.DenyCommercialCustomer(_commercialCustomer, _userid))
                    {
                        MessageBox.Show("Commercial Customer Denied.", "System Updated", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                    else
                    {
                        MessageBox.Show("Commercial Customer record not altered", "Oops, no record was modified", MessageBoxButton.OK, MessageBoxImage.Hand);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("There was an error approving this record. Please try again later" + Environment.NewLine + ex, "Oops, something went wrong", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                MessageBox.Show("Commercial Customer not approved.", "System Updated", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                this.Close();
            }

        }
    } // End of class
} // End of namespace
