using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfPresentationLayer
{
    class ErrorAlert
    {
        internal static void ShowDatabaseError()
        {
            MessageBox.Show("Database access failed");
        }
    }
}
