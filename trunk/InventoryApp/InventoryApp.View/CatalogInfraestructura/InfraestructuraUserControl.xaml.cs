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
using InventoryApp.ViewModel.CatalogInfraestructura;

namespace InventoryApp.View.CatalogInfraestructura
{
    /// <summary>
    /// Lógica de interacción para InfraestructuraUserControl.xaml
    /// </summary>
    public partial class InfraestructuraUserControl : UserControl
    {
        public InfraestructuraUserControl()
        {
            InitializeComponent();
        }
        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            InsertInfraestructuraView alta = new InsertInfraestructuraView();
            try
            {
                CatalogInfraestructuraViewModel InfraestructuraViewModel = (this.DataContext as ObjectDataProvider).Data as CatalogInfraestructuraViewModel;
                alta.DataContext = InfraestructuraViewModel.CreateInsertInfraestructuraViewModel();
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
                    ModifyInfraestructuraView dlgModify = new ModifyInfraestructuraView();
                    try
                    {
                        CatalogInfraestructuraViewModel infraestructuraViewModel = (this.DataContext as ObjectDataProvider).Data as CatalogInfraestructuraViewModel;
                        dlgModify.DataContext = infraestructuraViewModel.CreateModifyInfraestructuraViewModel();
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
