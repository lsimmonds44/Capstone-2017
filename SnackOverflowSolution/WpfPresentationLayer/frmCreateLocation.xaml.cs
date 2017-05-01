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
    /// Interaction logic for frmCreateLocation.xaml
    /// </summary>
    public partial class frmCreateLocation : Window
    {
        ILocationManager _locationMgr;

        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/24
        /// 
        /// Initialize Create Location Window.
        /// Standardized Methods.
        /// </summary>
        /// <param name="iLocationManager"></param>
        public frmCreateLocation(ILocationManager iLocationManager)
        {
            _locationMgr = iLocationManager;
            InitializeComponent();
        }

        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/24
        /// 
        /// Adds New Location.
        /// Standardized Methods.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddLocation_Click(object sender, RoutedEventArgs e)
        {
            if (txtDescription.Text.Equals(""))
            {
                MessageBox.Show("Please enter a description!");
                return;
            }

            Location location = new Location() {
                Description = txtDescription.Text,
                IsActive = (bool)chkActive.IsChecked
                };
            try
            {
                if (_locationMgr.CreateLocation(location) == 1)
                {
                    MessageBox.Show("Location successfully created.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to create location. Error Message: " + ex.Message);
            }
        }
    } // End of class
} // End of namespace
