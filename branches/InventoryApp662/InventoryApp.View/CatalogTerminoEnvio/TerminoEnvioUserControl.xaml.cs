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
using InventoryApp.ViewModel.CatalogTerminoEnvio;

namespace InventoryApp.View.CatalogTerminoEnvio
{
    /// <summary>
    /// Lógica de interacción para TerminoEnvioUserControl.xaml
    /// </summary>
    public partial class TerminoEnvioUserControl : UserControl
    {
        public TerminoEnvioUserControl()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            AltaModificarTerminoEnvio alta = new AltaModificarTerminoEnvio();
            try
            {
                CatalogTerminoEnvioViewModel terminoEnvioViewModel = (this.DataContext as ObjectDataProvider).Data as CatalogTerminoEnvioViewModel;
                alta.DataContext = terminoEnvioViewModel.CreateAddTerminoEnvioViewModel();
                alta.ShowDialog();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void dtGridItemStatus_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGrid dg = sender as DataGrid;
                if (dg != null && dg.SelectedItems != null && dg.SelectedItems.Count == 1)
                {
                    ModifyTerminoEnvioView dlgModify = new ModifyTerminoEnvioView();
                    try
                    {
                        CatalogTerminoEnvioViewModel terminoEnvioViewModel = (this.DataContext as ObjectDataProvider).Data as CatalogTerminoEnvioViewModel;
                        dlgModify.DataContext = terminoEnvioViewModel.CreateModifyTerminoEnvioViewModel();
                        dlgModify.ShowDialog();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
