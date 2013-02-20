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
using System.Windows.Navigation;
using System.Windows.Shapes;
using InventoryApp.ViewModel.CatalogSeguridad;

namespace InventoryApp.View.CatalogSeguridad
{
    /// <summary>
    /// Lógica de interacción para SeguridadUserControl.xaml
    /// </summary>
    public partial class SeguridadUserControl : UserControl
    {
        public SeguridadUserControl()
        {
            InitializeComponent();
        }

        private CatalogSeguridadViewModel ConvertDataContext(object dataSource)
        {
            CatalogSeguridadViewModel viewModel = dataSource as CatalogSeguridadViewModel;
            return viewModel;
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            AltaSeguridad View = new AltaSeguridad();
            CatalogSeguridadViewModel viewModel = this.ConvertDataContext(this.DataContext);
            View.DataContext = viewModel.CreateAddSeguridadViewModel();
            View.ShowDialog();
        }

        private void dtGridSeguridad_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
