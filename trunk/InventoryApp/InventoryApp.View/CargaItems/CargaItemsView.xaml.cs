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
using System.Diagnostics;
using System.Configuration;

namespace InventoryApp.View.CargaItems
{
    /// <summary>
    /// Lógica de interacción para CargaItemsView.xaml
    /// </summary>
    public partial class CargaItemsView : UserControl
    {
        public CargaItemsView()
        {
            InitializeComponent();
        }

        private void btnRuta_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(ConfigurationManager.AppSettings["RutaArchivos"].ToString());
        }

        private void btnCarga_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
