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
using InventoryApp.ViewModel.CatalogCategoria;

namespace InventoryApp.View.CatalogCategoria
{
    /// <summary>
    /// Lógica de interacción para ModifyCategoriaView.xaml
    /// </summary>
    public partial class ModifyCategoriaView : Window
    {
        public ModifyCategoriaView()
        {
            InitializeComponent();
            FocusManager.SetFocusedElement(this, this.txtNomreCategoria);
            this.txtNomreCategoria.SelectAll();
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
            ModifyCategoriaViewModel viewModel = this.ConvertDataContext(this.DataContext);
            addFactura.DataContext = viewModel.CreateHistorialViewModel();
            addFactura.ShowDialog();
        }

        private ModifyCategoriaViewModel ConvertDataContext(object dataSource)
        {
            ModifyCategoriaViewModel viewModel = dataSource as ModifyCategoriaViewModel;
            return viewModel;
        }

    }
}
