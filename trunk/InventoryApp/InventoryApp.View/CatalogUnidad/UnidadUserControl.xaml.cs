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
using InventoryApp.ViewModel.CatalogUnidad;

namespace InventoryApp.View.CatalogUnidad
{
    /// <summary>
    /// Lógica de interacción para UnidadUserControl.xaml
    /// </summary>
    public partial class UnidadUserControl : UserControl
    {
        public UnidadUserControl()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            AltaUnidad alta = new AltaUnidad();
            try
            {
                CatalogUnidadViewModel unidadViewModel = (this.DataContext as ObjectDataProvider).Data as CatalogUnidadViewModel;
                alta.DataContext = unidadViewModel.CreateAddUnidadViewModel();
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

        private void dtGridUnidad_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGrid dg = sender as DataGrid;
                if (dg != null && dg.SelectedItems != null && dg.SelectedItems.Count == 1)
                {
                    ModifyUnidadView dlgModify = new ModifyUnidadView();
                    try
                    {
                        CatalogUnidadViewModel unidadViewModel = (this.DataContext as ObjectDataProvider).Data as CatalogUnidadViewModel;
                        dlgModify.DataContext = unidadViewModel.CreateModifyUnidadViewModel();
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
