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
using InventoryApp.ViewModel.CatalogEmpresa;

namespace InventoryApp.View.CatalogEmpresa
{
    /// <summary>
    /// Lógica de interacción para EmpresaUserControl.xaml
    /// </summary>
    public partial class EmpresaUserControl : UserControl
    {
        public EmpresaUserControl()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            AltaEmpresa alta = new AltaEmpresa();
            try
            {
                CatalogEmpresaViewModel empresaViewModel = (this.DataContext as ObjectDataProvider).Data as CatalogEmpresaViewModel;
                alta.DataContext = empresaViewModel.CreateAddEmpresaViewModel();
                alta.ShowDialog();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void dtGridEmpresa_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGrid dg = sender as DataGrid;
                if (dg != null && dg.SelectedItems != null && dg.SelectedItems.Count == 1)
                {
                    ModifyEmpresaView dlgModify = new ModifyEmpresaView();
                    try
                    {
                        CatalogEmpresaViewModel empresaViewModel = (this.DataContext as ObjectDataProvider).Data as CatalogEmpresaViewModel;
                        dlgModify.DataContext = empresaViewModel.CreateModifyEmpresaViewModel();
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
