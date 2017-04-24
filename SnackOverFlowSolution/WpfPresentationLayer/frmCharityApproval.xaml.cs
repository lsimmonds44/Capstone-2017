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
    /// Daniel Brown
    /// Created: 2017/03/15
    /// 
    /// Allows employees to approve or deny charity applications
    /// Interaction logic for frmCharityApproval.xaml
    /// </summary>
    /// <summary>
    /// Alissa Duffy
    /// Updated: 2017/04/24
    /// 
    /// Standaridized comments.
    /// Standaridized methods.
    /// </summary>
    public partial class frmCharityApproval : Window
    {
        Charity _charity;
        ICharityManager _charityMgr;

        /// <summary>
        /// Alissa Duffy
        /// Updated: 2017/04/24
        /// 
        /// Initialize Charity Approval form.
        /// Standaridized methods.
        /// </summary>
        /// <param name="charityMgr"></param>
        /// <param name="charity"></param>
        public frmCharityApproval(ICharityManager charityMgr, Charity charity)
        {
            _charity = charity;
            _charityMgr = charityMgr;
            InitializeComponent();

            txtCharityID.Text = _charity.CharityID.ToString();
            txtCharityName.Text = _charity.CharityName;

        }

        /// <summary>
        /// Daniel Brown
        /// Created: 2017/03/15
        /// 
        /// Denies the current selected charity application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeny_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_charityMgr.DenyCharity(_charity))
                {
                    MessageBox.Show("Charity Denied.", "System Updated", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    MessageBox.Show("Charity record not altered", "Oops, no record was modified", MessageBoxButton.OK, MessageBoxImage.Hand);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error approving this record. Please try again later" + Environment.NewLine + ex, "Oops, something went wrong", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            MessageBox.Show("Charity not approved.", "System Updated", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            this.Close();
        }

        /// <summary>
        /// Daniel Brown
        /// Created: 2017/03/15
        /// 
        /// Approves the current selected charity application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnApprove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_charityMgr.ApproveCharity(_charity))
                {
                     MessageBox.Show("Charity approved.", "System Updated", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    MessageBox.Show("Charity record not altered", "Oops, no record was modified", MessageBoxButton.OK, MessageBoxImage.Hand);
                }
            }catch(Exception ex){
                MessageBox.Show("There was an error approving this record. Please try again later", "Oops, something went wrong",MessageBoxButton.OK,MessageBoxImage.Error);
            }
            
           
            this.Close();
        }
    } // End of class
} // End of namespace
