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
    /// Interaction logic for frmAddNewVehicle.xaml
    /// </summary>
    public partial class frmAddNewVehicle : Window
    {
        VehicleManager _myVehicleManager = new VehicleManager();
        VehicleTypeManager _myVehicleTypeManager = new VehicleTypeManager();
        List<VehicleType> currentVehicleTypes;
        public frmAddNewVehicle()
        {
            InitializeComponent();
            ddlType.ItemsSource = getCurrentVehicleTypes();
            ddlType.DisplayMemberPath = "VehicleTypeID";
            ddlType.SelectedValuePath = "VehicleTypeID";
        }

        /// <summary>
        /// Mason Allen
        /// Created 03/01/2017
        /// Retrieves a list of current vehicle types
        /// </summary>
        /// <returns></returns>
        public List<VehicleType> getCurrentVehicleTypes()
        {
            try
            {
                currentVehicleTypes = _myVehicleTypeManager.retreiveVehicleTypeList();
            }
            catch (Exception)
            {
                MessageBox.Show("There was a problem retrieving the current vehicle types");
            }
            return currentVehicleTypes;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Mason Allen
        /// Created 03/01/2017
        /// Saves a new vehicle record to the db based on information entered into text fields
        /// </summary>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool active = (radYes.IsChecked == true);
                _myVehicleManager.CreateVehicle(new Vehicle
                {
                    VIN = txtVIN.Text,
                    Make = txtMake.Text,
                    Model = txtModel.Text,
                    Mileage = Convert.ToInt32(txtMileage.Text),
                    Year = txtYear.Text,
                    Color = txtColor.Text,
                    Active = active,
                    VehicleTypeID = ((VehicleType)ddlType.SelectedItem).VehicleTypeID.ToString()
                });
                MessageBox.Show("Vehicle saved successfully");
                this.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("There was a problem saving this vehicle");
            }
        }
    }
}
