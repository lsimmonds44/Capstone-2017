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
    public partial class frmAddEditVehicle : Window
    {
        VehicleManager _myVehicleManager = new VehicleManager();
        VehicleTypeManager _myVehicleTypeManager = new VehicleTypeManager();
        List<VehicleType> currentVehicleTypes;
        public frmAddEditVehicle()
        {
            InitializeComponent();
            ddlType.ItemsSource = getCurrentVehicleTypes();
            ddlType.DisplayMemberPath = "VehicleTypeID";
            ddlType.SelectedValuePath = "VehicleTypeID";
        }

        public frmAddEditVehicle(Vehicle vehicle)
        {
            InitializeComponent();
            ddlType.ItemsSource = getCurrentVehicleTypes();
            ddlType.DisplayMemberPath = "VehicleTypeID";
            ddlType.SelectedValuePath = "VehicleTypeID";
            txtVIN.Text = vehicle.VIN;
            txtMake.Text = vehicle.Make;
            txtModel.Text = vehicle.Model;
            txtMileage.Text = vehicle.Mileage.ToString();
            txtYear.Text = vehicle.Year;
            txtColor.Text = vehicle.Color;
            if (vehicle.Active)
            {
                radYes.IsChecked = true;
            }
            else
            {
                radNo.IsChecked = true;
            }
            ddlType.SelectedValue = vehicle.VehicleTypeID;
            btnSave.Content = "Update";

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
            if (btnSave.Content as String == "Save")
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
    } // End of class
} // End of namespace
