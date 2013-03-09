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
using InventoryApp.ViewModel.CatalogItemStatus;

namespace InventoryApp.View.CatalogItemStatus
{
    /// <summary>
    /// Lógica de interacción para ItemStatusUserControl.xaml
    /// </summary>
    public partial class ItemStatusUserControl : UserControl
    {
        public ItemStatusUserControl()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            AltaItemStatus alta = new AltaItemStatus();
            try
            {
                CatalogItemStatusViewModel itemStatusViewModel = (this.DataContext as ObjectDataProvider).Data as CatalogItemStatusViewModel;
                alta.DataContext = itemStatusViewModel.CreateAddItemStatusViewModel();
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
                    ModifyItemStatusView dlgModify = new ModifyItemStatusView();
                    try
                    {
                        CatalogItemStatusViewModel equipoViewModel = (this.DataContext as ObjectDataProvider).Data as CatalogItemStatusViewModel;
                        dlgModify.DataContext = equipoViewModel.CreateModifyItemStatusViewModel();
                        dlgModify.ShowDialog();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
