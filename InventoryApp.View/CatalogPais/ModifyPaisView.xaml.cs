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
using InventoryApp.ViewModel.CatalogPais;

namespace InventoryApp.View.CatalogPais
{
    /// <summary>
    /// Lógica de interacción para ModifyPaisView.xaml
    /// </summary>
    public partial class ModifyPaisView : Window
    {
        public ModifyPaisView()
        {
            InitializeComponent();
            FocusManager.SetFocusedElement(this, this.txtNombrePais);
            this.txtNombrePais.SelectAll();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Historal_Click(object sender, RoutedEventArgs e)
        {
            HistorialView addFactura = new HistorialView();
            ModifyPaisViewModel viewModel = this.ConvertDataContext(this.DataContext);
            addFactura.DataContext = viewModel.CreateHistorialViewModel();
            addFactura.ShowDialog();
        }

        private ModifyPaisViewModel ConvertDataContext(object dataSource)
        {
            ModifyPaisViewModel viewModel = dataSource as ModifyPaisViewModel;
            return viewModel;
        }

    }
}
