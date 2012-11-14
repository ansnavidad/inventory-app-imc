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
using InventoryApp.ViewModel.CatalogTipoMovimiento;

namespace InventoryApp.View.CatalogTipoMovimiento
{
    /// <summary>
    /// Lógica de interacción para TipoMovimientoUserControl.xaml
    /// </summary>
    public partial class TipoMovimientoUserControl : UserControl
    {
        public TipoMovimientoUserControl()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            AltaTipoMovimiento alta = new AltaTipoMovimiento();
            try
            {
                CatalogTipoMovimientoViewModel tipoMovimientoViewModel = (this.DataContext as ObjectDataProvider).Data as CatalogTipoMovimientoViewModel;
                alta.DataContext = tipoMovimientoViewModel.CreateAddTipoMovimientoViewModel();
                alta.ShowDialog();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void dtGridMovimiento_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGrid dg = sender as DataGrid;
                if (dg != null && dg.SelectedItems != null && dg.SelectedItems.Count == 1)
                {
                    ModifyTipoMovimientoView dlgModify = new ModifyTipoMovimientoView();
                    try
                    {
                        CatalogTipoMovimientoViewModel tipoMovimientoViewModel = (this.DataContext as ObjectDataProvider).Data as CatalogTipoMovimientoViewModel;
                        dlgModify.DataContext = tipoMovimientoViewModel.CreateModifyTipoMovimientoViewModel();
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
