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
using InventoryApp.ViewModel.CatalogAlmacen;

namespace InventoryApp.View.CatalogAlmacen
{
    /// <summary>
    /// Lógica de interacción para AlmacenUserControl.xaml
    /// </summary>
    public partial class AlmacenUserControl : UserControl
    {
        public AlmacenUserControl()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            AltaAlmacen alta = new AltaAlmacen();
            try
            {
                CatalogAlmacenViewModel almacenViewModel = (this.DataContext as ObjectDataProvider).Data as CatalogAlmacenViewModel;
                alta.DataContext = almacenViewModel.CreateAddAlmacenViewModel();
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

        private void dtGridAlmacen_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGrid dg = sender as DataGrid;
                if (dg != null && dg.SelectedItems != null && dg.SelectedItems.Count == 1)
                {
                    ModifyAlmacenView dlgModify = new ModifyAlmacenView();
                    try
                    {
                        CatalogAlmacenViewModel almacenViewModel = (this.DataContext as ObjectDataProvider).Data as CatalogAlmacenViewModel;
                        dlgModify.DataContext = almacenViewModel.CreateModifyAlmacenViewModel();
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
