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

namespace InventoryApp.View.CatalogServicio
{
    /// <summary>
    /// Lógica de interacción para AltaServicio.xaml
    /// </summary>
    public partial class AltaServicio : Window
    {
        public AltaServicio()
        {
            InitializeComponent();
            FocusManager.SetFocusedElement(this, this.txtNomreServicio);
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
