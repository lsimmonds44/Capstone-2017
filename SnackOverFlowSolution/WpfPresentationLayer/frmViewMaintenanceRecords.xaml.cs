using DataObjects;
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
    /// Interaction logic for frmViewMaintenanceRecords.xaml
    /// </summary>
    public partial class frmViewMaintenanceRecords : Window
    {
        List<Repair> _repairList;
        List<RepairLine> _repairLineList;

        /// <summary>
        /// Robert Forbes
        /// 2017/03/24
        /// 
        /// Initializer for the window, takes in a list of repairs to display
        /// </summary>
        /// <param name="repairList"></param>
        public frmViewMaintenanceRecords(List<Repair> repairList)
        {
            _repairList = repairList;
            _repairLineList = new List<RepairLine>();
            InitializeComponent();
        }


        /// <summary>
        /// Robert Forbes
        /// 2017/03/24
        /// 
        /// Closes the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Robert Forbes
        /// 2017/03/24
        /// 
        /// Sets the list of repair record when the window loads
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmMaintenanceRecords_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (Repair r in _repairList)
            {
                foreach(RepairLine rl in r.RepairLineList){
                    _repairLineList.Add(rl);
                }
            }
            lstMaintenanceRecords.ItemsSource = null;
            lstMaintenanceRecords.ItemsSource = _repairLineList;
        }
    }
}
