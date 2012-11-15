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
using InventoryApp.ViewModel.CatalogServicio;

namespace InventoryApp.View.CatalogServicio
{
    /// <summary>
    /// Lógica de interacción para ServicioUserControl.xaml
    /// </summary>
    public partial class ServicioUserControl : UserControl
    {
        public ServicioUserControl()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            AltaServicio alta = new AltaServicio();
            try
            {
                CatalogServicioViewModel proyectoViewModel = (this.DataContext as ObjectDataProvider).Data as CatalogServicioViewModel;
                alta.DataContext = proyectoViewModel.CreateAddServicioViewModel();
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

        private void dtGridServicio_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGrid dg = sender as DataGrid;
                if (dg != null && dg.SelectedItems != null && dg.SelectedItems.Count == 1)
                {
                    ModifyServicioView dlgModify = new ModifyServicioView();
                    try
                    {
                        CatalogServicioViewModel servicioViewModel = (this.DataContext as ObjectDataProvider).Data as CatalogServicioViewModel;
                        dlgModify.DataContext = servicioViewModel.CreateModifyServicioViewModel();
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
