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
        private IGradeManager _gMgr;
        private ProductLot _pl;
        private Employee _currentEmp;
        private IProductManager _pm;
        private ISupplierManager _sm;
        private IInspectionManager _im;
        public frmAddInspection(ProductLot pl, IGradeManager gm, Employee currentEmp, IProductManager pm, ISupplierManager sm, IInspectionManager im)
        {
            _gMgr = gm;
            _pl = pl;
            _currentEmp = currentEmp;
            _pm = pm;
            _sm = sm;
            _im = im;
            InitializeComponent();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            try
            {
                string[] grades = _gMgr.RetrieveGradeList();
                cboGradeSelect.ItemsSource = grades;
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error: " + ex.Message);
            }

            try
            {
                txtProduct.Text = _pm.retrieveProductById(_pl.ProductId).Name;
                Supplier s = _sm.RetrieveSupplierBySupplierID(_pl.SupplierId);
                txtSupplier.Text = _sm.RetrieveSupplierName(s.UserId);
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
                if (_im.CreateInspection(_currentEmp.EmployeeId, _pl.ProductLotId, cboGradeSelect.Text, DateTime.Now, DateTime.Now.AddDays(7.0)))
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
