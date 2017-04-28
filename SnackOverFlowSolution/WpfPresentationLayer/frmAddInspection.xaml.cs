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
    /// Created: 2017/02/10
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
        private IProductLotManager _productLotManager;
        private decimal inspProdPrice = 0;

        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/24
        /// 
        /// Initialize the Add Inspection form.
        /// Standaridized method.
        /// </summary>
        /// <param name="productLot"></param>
        /// <param name="gradeManager"></param>
        /// <param name="currentEmp"></param>
        /// <param name="productManager"></param>
        /// <param name="supplierManager"></param>
        /// <param name="InspectionManager"></param>
        /// <param name="productLotManager"></param>
        public frmAddInspection(ProductLot productLot, IGradeManager gradeManager, 
            Employee currentEmp, IProductManager productManager, ISupplierManager supplierManager,
            IInspectionManager InspectionManager, IProductLotManager productLotManager)
        {
            _gradeManager = gradeManager;
            _productLot = productLot;
            _currentEmp = currentEmp;
            _productManager = productManager;
            _supplierManager = supplierManager;
            _inspectionManager = InspectionManager;
            _productLotManager = productLotManager;
            InitializeComponent();
        }

        /// <summary>
        /// Christian Lopez
        /// Created: 2017/02/22
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Updated: 2017/04/07
        /// 
        /// Changed string array of grades to list of string of grades.
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Initialized(object sender, EventArgs e)
        {
            try
            {
                var grades = _gradeManager.RetrieveGradeList();
                cboGradeSelect.ItemsSource = grades;
            }
            catch (Exception ex)
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

            try
            {
                txtProduct.Text = _productManager.RetrieveProductById((int)_productLot.ProductId).Name;
                Supplier s = _supplierManager.RetrieveSupplierBySupplierID((int)_productLot.SupplierId);
                txtSupplier.Text = _supplierManager.RetrieveSupplierName(s.UserId);
                txtFarm.Text = s.FarmName;
            }
            catch (Exception ex)
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

        /// <summary>
        /// Christian Lopez
        /// Created: 2017/02/16
        /// 
        /// Passes the information to the InspectionManager
        /// </summary>
        /// 
        /// <remarks>
        /// Aaron Usher
        /// Created: 2017/04/07
        /// 
        /// Wrapped information being passed in an Inspection.
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                validateInputs();
                var inspection = new Inspection()
                {
                    EmployeeId = _currentEmp.EmployeeId.Value,
                    ProductLotId = _productLot.ProductLotId.Value,
                    GradeId = cboGradeSelect.Text,
                    DatePerformed = DateTime.Now,
                    ExpirationDate = DateTime.Now.AddDays(7.0)
                };
                if (_inspectionManager.CreateInspection(inspection))
                {
                    this.DialogResult = true;
                }
                try
                {
                    if (cboGradeSelect.Text == "Reject")
                    {
                        frmConfirm confirmReject = new frmConfirm(_productLotManager, _productLot);
                        confirmReject.Show();
                        Close();
                    }
                    else if (_productLotManager.UpdateProductLotPrice(_productLot, inspProdPrice) == 1)
                    {
                        MessageBox.Show("Product lot inspection entered.");
                        Close();
                    }
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message + ex.StackTrace);
                }
            }
            catch (Exception ex)
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
        
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/24
        /// 
        /// Validates User Inputs.
        /// Standaridized method.
        /// </summary>
        private void validateInputs()
        {
            if (cboGradeSelect.Text == "")
            {
                throw new ApplicationException("A grade must be selected.");
            }
            if (txtInspectionProductPrice.Text == "")
            {
                if(cboGradeSelect.Text == "Reject")
                {
                    txtInspectionProductPrice.Text = "0.0";
                }
                else
                {
                    throw new ApplicationException("A price must be entered.");
                }
            }
            else
            {
                bool canConvert = decimal.TryParse(txtInspectionProductPrice.Text, out inspProdPrice);
                if (canConvert == false)
                {
                    txtInspectionProductPrice.Clear();
                    throw new ApplicationException("A valid price must be entered.");
                }
            }
        }
    } // End of class
} // End of namespace
