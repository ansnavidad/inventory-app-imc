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
using InventoryApp.ViewModel.CatalogTipoPedimento;

namespace InventoryApp.View.CatalogTipoPedimento
{
    /// <summary>
    /// Lógica de interacción para TipoPedimentoUserControl.xaml
    /// </summary>
    public partial class TipoPedimentoUserControl : UserControl
    {
        public TipoPedimentoUserControl()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            AltaModificarTipoPedimento alta = new AltaModificarTipoPedimento();
            try
            {
                CatalogTipoPedimentoViewModel tipoPedimentoViewModel = (this.DataContext as ObjectDataProvider).Data as CatalogTipoPedimentoViewModel;
                alta.DataContext = tipoPedimentoViewModel.CreateAddTipoPedimentoViewModel();
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
                    ModifyTipoPedimentoView dlgModify = new ModifyTipoPedimentoView();
                    try
                    {
                        CatalogTipoPedimentoViewModel tipoPedimentoViewModel = (this.DataContext as ObjectDataProvider).Data as CatalogTipoPedimentoViewModel;
                        dlgModify.DataContext = tipoPedimentoViewModel.CreateModifyTipoPedimentoViewModel();
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
