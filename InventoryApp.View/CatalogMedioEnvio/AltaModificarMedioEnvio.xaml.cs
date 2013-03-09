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

namespace InventoryApp.View.CatalogMedioEnvio
{
    /// <summary>
    /// Lógica de interacción para AltaModificarMedioEnvio.xaml
    /// </summary>
    public partial class AltaModificarMedioEnvio : Window
    {
        public AltaModificarMedioEnvio()
        {
            InitializeComponent();
            FocusManager.SetFocusedElement(this, this.txtNombreMedioEnvio);
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
