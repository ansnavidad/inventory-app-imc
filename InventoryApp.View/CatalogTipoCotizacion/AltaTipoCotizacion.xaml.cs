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

namespace InventoryApp.View.CatalogTipoCotizacion
{
    /// <summary>
    /// Lógica de interacción para AltaTipoCotizacion.xaml
    /// </summary>
    public partial class AltaTipoCotizacion : Window
    {
        public AltaTipoCotizacion()
        {
            InitializeComponent();
            FocusManager.SetFocusedElement(this, this.txtTipoCotizacion);
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
