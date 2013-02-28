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
using InventoryApp.ViewModel.Salidas;

namespace InventoryApp.View.Salidas
{
    /// <summary>
    /// Lógica de interacción para SalidaBaja.xaml
    /// </summary>
    public partial class SalidaBaja : Window
    {
        public SalidaBaja()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonAgregarItems_Click(object sender, RoutedEventArgs e)
        {
            AddItem it = new AddItem();
            SalidaBajaViewModel salida = this.DataContext as SalidaBajaViewModel;
            it.DataContext = salida.CreateCatalogItemViewModel();
            it.ShowDialog();
        }
    }
}
