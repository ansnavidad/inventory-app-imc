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
using InventoryApp.ViewModel.CatalogServicio;

namespace InventoryApp.View.CatalogServicio
{
    /// <summary>
    /// Lógica de interacción para ModifyServicioView.xaml
    /// </summary>
    public partial class ModifyServicioView : Window
    {
        public ModifyServicioView()
        {
            InitializeComponent();
            FocusManager.SetFocusedElement(this, this.txtNomreServicio);
            this.txtNomreServicio.SelectAll();
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
            ModifyServicioViewModel viewModel = this.ConvertDataContext(this.DataContext);
            addFactura.DataContext = viewModel.CreateHistorialViewModel();
            addFactura.ShowDialog();
        }

        private ModifyServicioViewModel ConvertDataContext(object dataSource)
        {
            ModifyServicioViewModel viewModel = dataSource as ModifyServicioViewModel;
            return viewModel;
        }
    }
}
