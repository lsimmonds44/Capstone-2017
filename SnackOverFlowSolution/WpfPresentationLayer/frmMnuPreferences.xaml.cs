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
    /// Created by Michael Takrama
    /// 24/03/2017
    /// 
    /// Interaction logic for frmMnuPreferences.xaml
    /// </summary>
    public partial class frmMnuPreferences : Window
    {
        private readonly IPreferenceManager _preferenceManager;

        public frmMnuPreferences(IPreferenceManager preferenceManager)
        {
            _preferenceManager = preferenceManager;
            InitializeComponent();
        }

        /// <summary>
        /// Created by Michael Takrama
        /// 24/03/2017
        /// 
        /// Saves Preferences
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSave_OnClick(object sender, RoutedEventArgs e)
        {
            var expiringSoonDuration = new Preferences();

            if (1 == ValidateInput())
            {
                try
                {

                    expiringSoonDuration.ExpiringSoonDuration = int.Parse(TxtPreference.Text);
                    _preferenceManager.UpdatePreferenceSettings(expiringSoonDuration); 
                    MessageBox.Show("Preferences Saved");
                }
                catch ( Exception ex)
                {
                    MessageBox.Show(ex.Message);
                } 
            }   
            else
            {
                MessageBox.Show("Kindly review input");
                return; 
            }    
            
        }

        /// <summary>
        /// Created by Michael Takrama
        /// 24/03/2017
        /// 
        /// Closes dialog box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCancel_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Created by Michael Takrama
        /// 24/03/2017
        /// 
        /// Validates User Input
        /// </summary>
        /// <returns></returns>
        private int ValidateInput()
        {
            int signal = 1;

            if ("" == TxtPreference.Text)
            {
                TxtPreference.BorderBrush = System.Windows.Media.Brushes.Red;
                signal = 0;
            }

            if ("" != TxtPreference.Text)
            {
                try
                {
                    Int32.Parse(TxtPreference.Text);
                }
                catch (FormatException fe)
                {
                    TxtPreference.BorderBrush = System.Windows.Media.Brushes.Red;
                    signal = 0;
                }
            }

            return signal;

        }
    }
}
