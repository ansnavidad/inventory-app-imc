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

namespace InventoryApp.View.Reportes
{
    /// <summary>
    /// Lógica de interacción para ReportesMain.xaml
    /// </summary>
    public partial class ReportesMain : UserControl
    {
        public ReportesMain()
        {
            InitializeComponent();  
        
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            webBrowser1.Navigate(new Uri("http://10.50.0.131/Reports/Pages/Report.aspx?ItemPath=%2fElara%2fInventario-Almacen2", UriKind.RelativeOrAbsolute));
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            webBrowser1.Navigate(new Uri("http://10.50.0.131/Reports/Pages/Report.aspx?ItemPath=%2fElara%2fInventario-Kardex", UriKind.RelativeOrAbsolute));
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            webBrowser1.Navigate(new Uri("http://10.50.0.131/Reports/Pages/Report.aspx?ItemPath=%2fElara%2fInventario-Rotacion", UriKind.RelativeOrAbsolute));
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            webBrowser1.Navigate(new Uri("http://10.50.0.131/Reports/Pages/Report.aspx?ItemPath=%2fElara%2fRecibo", UriKind.RelativeOrAbsolute));
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            webBrowser1.Navigate(new Uri("http://10.50.0.131/Reports/Pages/Report.aspx?ItemPath=%2fElara%2fInventario-Adhoc", UriKind.RelativeOrAbsolute));
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
