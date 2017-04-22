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
    /// Ethan Jorgensen
    /// 2017/03/23
    /// 
    /// Interaction logic for frmSplitProductLot.xaml
    /// </summary>
    public partial class frmSplitProductLot : Window
    {
        public int OldQty { get; set; }
        public int NewQty { get; set; }
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/21
        /// 
        /// Initialize the Split Product Lot Window.
        /// Standaridized method.
        /// </summary>
        /// <param name="selectedItem"></param>
        public frmSplitProductLot(ProductLot selectedItem)
        {
            InitializeComponent();
            OldQty = selectedItem.Quantity ?? 0;
            NewQty = 0;
            txtOld.Text = OldQty.ToString();
            txtNew.Text = NewQty.ToString();
        }
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/21
        /// 
        /// Saves the changes to Split Product Lot.
        /// Standaridized method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/21
        /// 
        /// Cancels Changes made to Split Product Lot.
        /// Standaridized method. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/21
        /// 
        /// Changes New Text.
        /// Standaridized method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNew_TextChanged(object sender, TextChangedEventArgs e)
        {
            int value;
            // Already validated
            Int32.TryParse(txtNew.Text, out value);
            int sum = OldQty + NewQty;
            if (value <= sum && value >= 1)
            {
                NewQty = value;
                OldQty = sum - value;

                // detach event handler to avoid call to txtOld_TextChanged
                txtOld.TextChanged -= txtOld_TextChanged;
                txtOld.Text = OldQty.ToString();
                txtOld.TextChanged += txtOld_TextChanged;
            }
            // Make sure that the text box does not show data which did not pass validation

            txtNew.Text = NewQty.ToString();

        }
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/21
        /// 
        /// Inputs Text Preview for New text.
        /// Standaridized method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNew_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/21
        /// 
        /// Changes Old Text.
        /// Standaridized method. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtOld_TextChanged(object sender, TextChangedEventArgs e)
        {
            int value;
            // Already validated
            Int32.TryParse(txtOld.Text, out value);
            int sum = OldQty + NewQty;
            if (value <= sum && value >= 1)
            {
                OldQty = value;
                NewQty = sum - value;

                // detach event handler to avoid call to txtNew_TextChanged
                txtNew.TextChanged -= txtNew_TextChanged;
                txtNew.Text = NewQty.ToString();
                txtNew.TextChanged += txtNew_TextChanged;
            }
            // Make sure that the text box does not show data which did not pass validation
            txtOld.Text = OldQty.ToString();
        }
        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/21
        /// 
        /// Inputs Text Preview for Old text.
        /// Standaridized method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtOld_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!char.IsDigit(e.Text, e.Text.Length - 1))
                e.Handled = true;
        }
    }
}
