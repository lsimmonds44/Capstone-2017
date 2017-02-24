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
    /// Christian Lopez
    /// Created on 2017/02/10
    /// 
    /// Interaction logic for frmAddInspection.xaml
    /// </summary>
    public partial class frmAddInspection : Window
    {
        private IGradeManager _gradeManager;
        private ProductLot _productLot;
        private Employee _currentEmp;
        private IProductManager _productManager;
        private ISupplierManager _supplierManager;
        private IInspectionManager _inspectionManager;
        public frmAddInspection(ProductLot productLot, IGradeManager gradeManager, 
            Employee currentEmp, IProductManager productManager, ISupplierManager supplierManager,
            IInspectionManager InspectionManager)
        {
            _gradeManager = gradeManager;
            _productLot = productLot;
            _currentEmp = currentEmp;
            _productManager = productManager;
            _supplierManager = supplierManager;
            _inspectionManager = InspectionManager;
            InitializeComponent();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            try
            {
                string[] grades = _gradeManager.RetrieveGradeList();
                cboGradeSelect.ItemsSource = grades;
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error: " + ex.Message);
            }

            try
            {
                txtProduct.Text = _productManager.retrieveProductById(_productLot.ProductId).Name;
                Supplier s = _supplierManager.RetrieveSupplierBySupplierID(_productLot.SupplierId);
                txtSupplier.Text = _supplierManager.RetrieveSupplierName(s.UserId);
                txtFarm.Text = s.FarmName;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// Christian Lopez
        /// Created on 2017/02/16
        /// 
        /// Passes the information to the InpsectionManager
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                validateInputs();
                if (_inspectionManager.CreateInspection(_currentEmp.EmployeeId, _productLot.ProductLotId, cboGradeSelect.Text, DateTime.Now, DateTime.Now.AddDays(7.0)))
                {
                    this.DialogResult = true;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void validateInputs()
        {
            if (cboGradeSelect.Text == "")
            {
                throw new ApplicationException("A grade must be selected.");
            }
        }
    }
}
