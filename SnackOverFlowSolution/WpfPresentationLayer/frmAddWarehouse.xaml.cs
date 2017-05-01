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
    /// Interaction logic for frmAddWarehouse.xaml
    /// </summary>
    public partial class frmAddWarehouse : Window
    {
        WarehouseManager _myWarehouseManager = new WarehouseManager();
        string[] states = {"AK", "AL", "AR", "AZ", "CA", "CO", "CT", "DE", "FL", "GA", "HI", "IA", "ID", "IL",
                              "IN", "KS", "KY", "LA", "MA", "MD", "ME", "MI", "MN", "MO", "MS", "MT", "NC", "ND",
                              "NE", "NH", "NJ", "NM", "NV", "NY", "OH", "OK", "OR", "PA", "RI", "SC", "SD", "TN",
                              "TX", "UT", "VA", "VT", "WA", "WI", "WV", "WY"};
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/24
        /// 
        /// Initialize Add Warehouse form.
        /// Standardized method.
        /// </summary>
        public frmAddWarehouse()
        {
            InitializeComponent();
            cboState.ItemsSource = states;
        }

        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/24
        /// 
        /// Saves New Warehouse information.
        /// Standardized method. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string address1 = txtAddress1.Text;
            string address2 = txtAddress2.Text;
            string city = txtCity.Text;
            string state = cboState.SelectedItem.ToString();
            string zip = txtZip.Text;
            Warehouse newWarehouse = new Warehouse();
            newWarehouse.AddressOne = address1;
            newWarehouse.AddressTwo = address2;
            newWarehouse.City = city;
            newWarehouse.State = state;
            newWarehouse.Zip = zip;

            try
            {
                _myWarehouseManager.addWarehouse(newWarehouse);
                MessageBox.Show("Warehouse Created Successfully!");
                this.DialogResult = true;
            }
            catch (Exception)
            {
                MessageBox.Show("There was a problem saving this warehouse.");
            }
        }

        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/24
        /// 
        /// Cancel/Closes Add Warehouse form.
        /// Standardized method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    } // End of class
} // End of namespace
