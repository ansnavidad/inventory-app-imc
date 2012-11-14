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
using InventoryApp.ViewModel.CatalogMoneda;

namespace InventoryApp.View.CatalogMoneda
{
    /// <summary>
    /// Lógica de interacción para MonedaUserControl.xaml
    /// </summary>
    public partial class MonedaUserControl : UserControl
    {
        public MonedaUserControl()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            AddMonedaView alta = new AddMonedaView();
            try
            {
                CatalogMonedaViewModel monedaViewModel = (this.DataContext as ObjectDataProvider).Data as CatalogMonedaViewModel;
                alta.DataContext = monedaViewModel.CreateAddMonedaViewModel();
                alta.ShowDialog();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void dtGridMoneda_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGrid dg = sender as DataGrid;
                if (dg != null && dg.SelectedItems != null && dg.SelectedItems.Count == 1)
                {
                    ModifyMonedaView dlgModify = new ModifyMonedaView();
                    try
                    {
                        CatalogMonedaViewModel monedaViewModel = (this.DataContext as ObjectDataProvider).Data as CatalogMonedaViewModel;
                        dlgModify.DataContext = monedaViewModel.CreateModifyMonedaViewModel();
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
