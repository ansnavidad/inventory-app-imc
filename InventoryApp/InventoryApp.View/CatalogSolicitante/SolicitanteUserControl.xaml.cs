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
using InventoryApp.ViewModel.CatalogSolicitante;

namespace InventoryApp.View.CatalogSolicitante
{
    /// <summary>
    /// Lógica de interacción para SolicitanteUserControl.xaml
    /// </summary>
    public partial class SolicitanteUserControl : UserControl
    {
        public SolicitanteUserControl()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            AltaSolicitante alta = new AltaSolicitante();
            try
            {
                CatalogSolicitanteViewModel solicitanteViewModel = (this.DataContext as ObjectDataProvider).Data as CatalogSolicitanteViewModel;
                alta.DataContext = solicitanteViewModel.CreateAddSolicitanteViewModel();
                alta.ShowDialog();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void dtGridSolicitante_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGrid dg = sender as DataGrid;
                if (dg != null && dg.SelectedItems != null && dg.SelectedItems.Count == 1)
                {
                    ModifySolicitanteView dlgModify = new ModifySolicitanteView();
                    try
                    {
                        CatalogSolicitanteViewModel solicitanteViewModel = (this.DataContext as ObjectDataProvider).Data as CatalogSolicitanteViewModel;
                        dlgModify.DataContext = solicitanteViewModel.CreateModifySolicitanteViewModel();
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
