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
using InventoryApp.View.CatalogCategoria;
using InventoryApp.ViewModel.CatalogCategoria;
using InventoryApp.View.CatalogEquipo;
using InventoryApp.ViewModel.CatalogEquipo;
using InventoryApp.View.CatalogModelo;
using InventoryApp.ViewModel.CatalogModelo;
using InventoryApp.View.CatalogMarca;
using InventoryApp.ViewModel.CatalogMarca;

namespace InventoryApp.View.CatalogArticulo
{
    /// <summary>
    /// Lógica de interacción para AddArticuloView.xaml
    /// </summary>
    public partial class AddArticuloView : Window
    {
        public AddArticuloView()
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
