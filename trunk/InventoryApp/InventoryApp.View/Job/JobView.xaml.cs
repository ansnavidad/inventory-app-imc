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
using System.Windows.Navigation;
using System.Windows.Shapes;
using InventoryApp.ViewModel.Job;
using System.Diagnostics;
using System.Configuration;

namespace InventoryApp.View.Job
{
    /// <summary>
    /// Lógica de interacción para JobView.xaml
    /// </summary>
    public partial class JobView : UserControl
    {
        public JobView()
        {
            InitializeComponent();
        }

        private void btnCarga_Click(object sender, RoutedEventArgs e)
        {
            JobViewModel job = new JobViewModel();

            job.CallServiceGetExecuteJob();

            lblCargaItems.Visibility = Visibility.Hidden;

        }

        private void btnRuta_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(ConfigurationManager.AppSettings["RutaArchivos"].ToString());
        }
    }
}
