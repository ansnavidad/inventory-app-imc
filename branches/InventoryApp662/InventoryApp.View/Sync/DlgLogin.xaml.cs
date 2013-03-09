using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace InventoryApp.View.Sync
{
    /// <summary>
    /// Lógica de interacción para DlgLogin.xaml
    /// </summary>
    public partial class DlgLogin : Window
    {
        public DlgLogin()
        {
            InitializeComponent();
        }
        private void chkClose_SourceUpdated(object sender, DataTransferEventArgs e)
        {
            if ((bool)this.chkClose.IsChecked && !(bool)this.chkManualClose.IsChecked)
            {
                this.Close();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!(bool)this.chkClose.IsChecked)
            {
                e.Cancel = true;
            }
        }
    }
}
