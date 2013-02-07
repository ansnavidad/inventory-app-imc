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

namespace InventoryApp.View.Recibo
{
    /// <summary>
    /// Lógica de interacción para FacturaCatalogAgregarItem.xaml
    /// </summary>
    public partial class FacturaCatalogAgregarItem : Window
    {
        public FacturaCatalogAgregarItem()
        {
            InitializeComponent();
        }        

        private void dtGridFacturas_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void checkBox1_Checked(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
