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
using InventoryApp.ViewModel.CatalogCiudad;

namespace InventoryApp.View.CatalogCiudad
{
    /// <summary>
    /// Lógica de interacción para CiudadUserControl.xaml
    /// </summary>
    public partial class CiudadUserControl : UserControl
    {
        public CiudadUserControl()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            AltaModificarCiudad alta = new AltaModificarCiudad();
            try
            {
                CatalogCiudadViewModel ciudadViewModel = (this.DataContext as ObjectDataProvider).Data as CatalogCiudadViewModel;
                alta.DataContext = ciudadViewModel.CreateAddCiudadViewModel();
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
                    ModifyCiudadView dlgModify = new ModifyCiudadView();
                    try
                    {
                        CatalogCiudadViewModel ciudadViewModel = (this.DataContext as ObjectDataProvider).Data as CatalogCiudadViewModel;
                        dlgModify.DataContext = ciudadViewModel.CreateModifyCiudadViewModel();
                        dlgModify.ShowDialog();
                    }
                    catch (Exception ex) {
                        throw ex;
                    }
                }
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dtGridItemStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
