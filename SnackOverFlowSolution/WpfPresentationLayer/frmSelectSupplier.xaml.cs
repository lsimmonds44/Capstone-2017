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

namespace WpfPresentationLayer
{
    /// <summary>
    /// Christian Lopez
    /// 2017/03/29
    /// 
    /// Selects a supplier from the list
    /// Interaction logic for frmSelectSupplier.xaml
    /// </summary>
    public partial class frmSelectSupplier : Window
    {
        private List<Supplier> _suppliers;
        public Supplier selectedSupplier { get; private set; }

        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/21
        /// 
        /// Initialize Select Supplier Window.
        /// Standaridized method.
        /// </summary>
        /// <param name="suppliers"></param>
        public frmSelectSupplier(List<Supplier> suppliers)
        {
            _suppliers = suppliers;
            InitializeComponent();
        }

        /// <summary>
        /// Christian Lopez
        /// 2017/03/29
        /// 
        /// Populates the combobox and sets its initial value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (Supplier s in _suppliers)
            {
                cboSuppliers.Items.Add(s.FarmName);
            }
            cboSuppliers.SelectedIndex = 0;
        }

        /// <summary>
        /// Christian Lopez
        /// 2017/03/29
        /// 
        /// Sets the selected supplier and closes the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            selectedSupplier = _suppliers.Find(s => s.FarmName == cboSuppliers.SelectedItem.ToString());
            this.DialogResult = true;
        }

    }
}
