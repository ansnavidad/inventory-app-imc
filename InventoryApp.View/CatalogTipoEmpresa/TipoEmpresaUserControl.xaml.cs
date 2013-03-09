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
using InventoryApp.ViewModel.CatalogTipoEmpresa;

namespace InventoryApp.View.CatalogTipoEmpresa
{
    /// <summary>
    /// Lógica de interacción para TipoEmpresaUserControl.xaml
    /// </summary>
    public partial class TipoEmpresaUserControl : UserControl
    {
        public TipoEmpresaUserControl()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            InsertTipoEmpresaView alta = new InsertTipoEmpresaView();
            try
            {
                CatalogTipoEmpresaViewModel tipoEmpresaViewModel = (this.DataContext as ObjectDataProvider).Data as CatalogTipoEmpresaViewModel;
                alta.DataContext = tipoEmpresaViewModel.CreateInsertTipoEmpresaViewModel();
                alta.ShowDialog();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void dtGridItems_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGrid dg = sender as DataGrid;
                if (dg != null && dg.SelectedItems != null && dg.SelectedItems.Count == 1)
                {
                    ModifyTipoEmpresaView dlgModify = new ModifyTipoEmpresaView();
                    try
                    {
                        CatalogTipoEmpresaViewModel tipoEmpresaViewModel = (this.DataContext as ObjectDataProvider).Data as CatalogTipoEmpresaViewModel;
                        dlgModify.DataContext = tipoEmpresaViewModel.CreateModifyTipoEmpresaViewModel();
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
