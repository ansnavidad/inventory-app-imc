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

namespace InventoryApp.View
{
    /// <summary>
    /// Lógica de interacción para AltaTipoCotizacion.xaml
    /// </summary>
    public partial class AltaTipoCotizacion : Window
    {
        public AltaTipoCotizacion()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
