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

namespace InventoryApp.View.CatalogCliente
{
    /// <summary>
    /// Lógica de interacción para ModifyClienteView.xaml
    /// </summary>
    public partial class ModifyClienteView : Window
    {
        public ModifyClienteView()
        {
            InitializeComponent();
            FocusManager.SetFocusedElement(this, this.txtNomreCliente);
            this.txtNomreCliente.SelectAll();
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
