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
    /// Lógica de interacción para LoadDates.xaml
    /// </summary>
    public partial class LoadDates : Window
    {
        public LoadDates()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        public LoadDates(string name)
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            label1.Content = name;
        }

        private void checkBox1_TargetUpdated(object sender, DataTransferEventArgs e)
        {
            if (this.checkBox1.IsChecked == true)
            {
                this.Close();
            }
        }
    }
}
