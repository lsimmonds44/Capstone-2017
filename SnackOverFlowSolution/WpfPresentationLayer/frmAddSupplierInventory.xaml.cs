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
using System.Data.SqlClient;

namespace WpfPresentationLayer
{
    /// <summary>
    /// Created 2017-03-30 by William Flood
    /// Interaction logic for frmAddSupplierInventory.xaml
    /// </summary>
    public partial class frmAddSupplierInventory : Window
    {
        ISupplierInventoryManager _supplyInventorymanager;
        IAgreementManager _agreementManager;
        List<Agreement> _agreementList;

        /// <summary>
        /// Created 2017-03-30 by William Flood
        /// </summary>
        /// <param name="_supplyInventorymanager">Handles business logic related to supplier inventory</param>
        /// <param name="_agreementList">A list of agreements associated with the user</param>
        public frmAddSupplierInventory(ISupplierInventoryManager _supplyInventorymanager, List<Agreement> _agreementList)
        {
            this._supplyInventorymanager = _supplyInventorymanager;
            InitializeComponent();
            this._agreementList = _agreementList;
            this.cbxAgreement.ItemsSource = _agreementList;
            this.dateAdded.SelectedDate = DateTime.Now;
        }

        /// <summary>
        /// Created 2017-03-30 by William Flood
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPost_Click(object sender, RoutedEventArgs e)
        {
            int? agreementId = null;
            int? quantity = null;
            int parsedQuantity;
            if(Int32.TryParse(txtQuantity.Text,out parsedQuantity))
            {
                quantity = parsedQuantity;
            } else
            {
                MessageBox.Show("Quantity needs an integer");
                return;
            }
            if(cbxAgreement.SelectedIndex >=0)
            {
                agreementId = _agreementList[cbxAgreement.SelectedIndex].AgreementId;
            } else
            {
                MessageBox.Show("Select an agreement");
                return;
            }
            if(null==dateAdded.SelectedDate)
            {
                MessageBox.Show("Select a date");
                return;
            }
            try
            {
                var toAdd = new SupplierInventory()
                {
                    AgreementID = agreementId,
                    DateAdded = dateAdded.SelectedDate,
                    Quantity = quantity
                };
                _supplyInventorymanager.CreateSupplierInventory(toAdd);
                MessageBox.Show("Inventory saved");
                this.Close();
            } catch (Exception ex)
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
    }
}
