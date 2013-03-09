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
using InventoryApp.ViewModel.CatalogArticulo;

namespace InventoryApp.View.CatalogArticulo
{
    /// <summary>
    /// Lógica de interacción para ArticuloUserControl.xaml
    /// </summary>
    public partial class ArticuloUserControl : UserControl
    {
        public ArticuloUserControl()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            AddArticuloView alta = new AddArticuloView();
            try
            {
                CatalogArticuloViewModel articuloViewModel = (this.DataContext as ObjectDataProvider).Data as CatalogArticuloViewModel;
                alta.DataContext = articuloViewModel.CreateAddArticuloViewModel();
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
                    ModifyArticuloView dlgModify = new ModifyArticuloView();
                    try
                    {
                        CatalogArticuloViewModel articuloViewModel = (this.DataContext as ObjectDataProvider).Data as CatalogArticuloViewModel;
                        dlgModify.DataContext = articuloViewModel.CreateModifyArticuloViewModel();
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

        private void RowCheckBox_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
