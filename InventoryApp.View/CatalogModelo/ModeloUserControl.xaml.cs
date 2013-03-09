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
using InventoryApp.ViewModel.CatalogModelo;

namespace InventoryApp.View.CatalogModelo
{
    /// <summary>
    /// Lógica de interacción para ModeloUserControl.xaml
    /// </summary>
    public partial class ModeloUserControl : UserControl
    {
        public ModeloUserControl()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            AddModeloView alta = new AddModeloView();
            try
            {
                CatalogModeloViewModel modeloViewModel = (this.DataContext as ObjectDataProvider).Data as CatalogModeloViewModel;
                alta.DataContext = modeloViewModel.CreateAddModeloViewModel();
                alta.ShowDialog();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void dtGridModelo_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGrid dg = sender as DataGrid;
                if (dg != null && dg.SelectedItems != null && dg.SelectedItems.Count == 1)
                {
                    ModifyModeloView dlgModify = new ModifyModeloView();
                    try
                    {
                        CatalogModeloViewModel modeloViewModel = (this.DataContext as ObjectDataProvider).Data as CatalogModeloViewModel;
                        dlgModify.DataContext = modeloViewModel.CreateModifyModeloViewModel();
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
