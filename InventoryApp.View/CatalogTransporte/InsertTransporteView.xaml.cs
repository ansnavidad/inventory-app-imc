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

namespace InventoryApp.View.CatalogTransporte
{
    /// <summary>
    /// Lógica de interacción para InsertTransporteView.xaml
    /// </summary>
    public partial class InsertTransporteView : Window
    {
        public InsertTransporteView()
        {
            InitializeComponent();
            FocusManager.SetFocusedElement(this, this.textBox2);
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
