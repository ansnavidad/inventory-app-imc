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
using InventoryApp.ViewModel.CatalogTransporte;

namespace InventoryApp.View.CatalogTransporte
{
    /// <summary>
    /// Lógica de interacción para TransporteUserControl.xaml
    /// </summary>
    public partial class TransporteUserControl : UserControl
    {
        public TransporteUserControl()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            InsertTransporteView insert = new InsertTransporteView();
            try
            {
                CatalogTransporteViewModel transporteViewModel = (this.DataContext as ObjectDataProvider).Data as CatalogTransporteViewModel;
                insert.DataContext = transporteViewModel.CreateAddTransporteViewModel();
                insert.ShowDialog();
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
                    ModifyTransporteView dlgModify = new ModifyTransporteView();
                    try
                    {
                        CatalogTransporteViewModel transporteViewModel = (this.DataContext as ObjectDataProvider).Data as CatalogTransporteViewModel;
                        dlgModify.DataContext = transporteViewModel.CreateModifyTransporteViewModel();
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
