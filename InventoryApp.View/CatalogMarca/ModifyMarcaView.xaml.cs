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
using InventoryApp.View.Historial;
using InventoryApp.ViewModel.CatalogMarca;

namespace InventoryApp.View.CatalogMarca
{
    /// <summary>
    /// Lógica de interacción para ModifyMarcaView.xaml
    /// </summary>
    public partial class ModifyMarcaView : Window
    {
        public ModifyMarcaView()
        {
            InitializeComponent();
            FocusManager.SetFocusedElement(this, this.txtNomreStatus);
            this.txtNomreStatus.SelectAll();
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Historal_Click(object sender, RoutedEventArgs e)
        {
            HistorialView addFactura = new HistorialView();
            ModifyMarcaViewModel viewModel = this.ConvertDataContext(this.DataContext);
            addFactura.DataContext = viewModel.CreateHistorialViewModel();
            addFactura.ShowDialog();
        }

        private ModifyMarcaViewModel ConvertDataContext(object dataSource)
        {
            ModifyMarcaViewModel viewModel = dataSource as ModifyMarcaViewModel;
            return viewModel;
        }
    }
}
