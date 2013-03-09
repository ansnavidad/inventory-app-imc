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
using InventoryApp.ViewModel.CatalogProyecto;

namespace InventoryApp.View.CatalogProyecto
{
    /// <summary>
    /// Lógica de interacción para ProyectoUserControl.xaml
    /// </summary>
    public partial class ProyectoUserControl : UserControl
    {
        public ProyectoUserControl()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            AltaProyecto alta = new AltaProyecto();
            try
            {
                CatalogProyectoViewModel proyectoViewModel = (this.DataContext as ObjectDataProvider).Data as CatalogProyectoViewModel;
                alta.DataContext = proyectoViewModel.CreateAddProyectoViewModel();
                alta.ShowDialog();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void dtGridProyecto_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGrid dg = sender as DataGrid;
                if (dg != null && dg.SelectedItems != null && dg.SelectedItems.Count == 1)
                {
                    ModifyProyectoView dlgModify = new ModifyProyectoView();
                    try
                    {
                        CatalogProyectoViewModel proyectoViewModel = (this.DataContext as ObjectDataProvider).Data as CatalogProyectoViewModel;
                        dlgModify.DataContext = proyectoViewModel.CreateModifyProyectoViewModel();
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
