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
using InventoryApp.ViewModel.CatalogCliente;

namespace InventoryApp.View.CatalogCliente
{
    /// <summary>
    /// Lógica de interacción para ClienteUserControl.xaml
    /// </summary>
    public partial class ClienteUserControl : UserControl
    {
        public ClienteUserControl()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            AltaCliente alta = new AltaCliente();
            try
            {
                CatalogClienteViewModel clienteViewModel = (this.DataContext as ObjectDataProvider).Data as CatalogClienteViewModel;
                alta.DataContext = clienteViewModel.CreateAddClienteViewModel();
                alta.ShowDialog();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dtGridCliente_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGrid dg = sender as DataGrid;
                if (dg != null && dg.SelectedItems != null && dg.SelectedItems.Count == 1)
                {
                    ModifyClienteView dlgModify = new ModifyClienteView();
                    try
                    {
                        CatalogClienteViewModel clienteViewModel = (this.DataContext as ObjectDataProvider).Data as CatalogClienteViewModel;
                        dlgModify.DataContext = clienteViewModel.CreateModifyClienteViewModel();
                        dlgModify.ShowDialog();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }
    }
}
