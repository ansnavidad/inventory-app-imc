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
using InventoryApp.ViewModel.CatalogArticulo;

namespace InventoryApp.View.CatalogArticulo
{
    /// <summary>
    /// Lógica de interacción para ModifyArticuloView.xaml
    /// </summary>
    public partial class ModifyArticuloView : Window
    {
        public ModifyArticuloView()
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

        private void buttonNCat_Click(object sender, RoutedEventArgs e)
        {
            AltaCategoria view = new AltaCategoria();
            AddCategoriaViewModel viewModel = new AddCategoriaViewModel(new CatalogCategoriaViewModel(), (ModifyArticuloViewModel)this.DataContext);
            view.DataContext = viewModel;
            view.ShowDialog();
        }

        private void buttonNEquipo_Click(object sender, RoutedEventArgs e)
        {
            AddEquipoView view = new AddEquipoView();
            AddEquipoViewModel viewModel = new AddEquipoViewModel(new CatalogEquipoViewModel(), (ModifyArticuloViewModel)this.DataContext);
            view.DataContext = viewModel;
            view.ShowDialog();
        }

        private void buttonNModelo_Click(object sender, RoutedEventArgs e)
        {
            AddModeloView view = new AddModeloView();
            AddModeloViewModel viewModel = new AddModeloViewModel(new CatalogModeloViewModel(), (ModifyArticuloViewModel)this.DataContext);
            view.DataContext = viewModel;
            view.ShowDialog();
        }

        private void buttonNMarca_Click(object sender, RoutedEventArgs e)
        {
            AddMarca view = new AddMarca();
            AddMarcaViewModel viewModel = new AddMarcaViewModel(new CatalogMarcaViewModel(), (ModifyArticuloViewModel)this.DataContext);
            view.DataContext = viewModel;
            view.ShowDialog();
        }      
    }
}
