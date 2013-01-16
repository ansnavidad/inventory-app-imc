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
using InventoryApp.ViewModel.CatalogProgramado;

namespace InventoryApp.View.CatalogProgramado
{
    /// <summary>
    /// Lógica de interacción para CatalogProgramado.xaml
    /// </summary>
    public partial class CatalogProgramado : UserControl
    {
        public CatalogProgramado()
        {
            InitializeComponent();
        }
        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            AddProgramadoView alta = new AddProgramadoView();
            try
            {
                CatalogProgramadoViewModel catalogprogramadoViewModel = new CatalogProgramadoViewModel();
                catalogprogramadoViewModel = this.DataContext as CatalogProgramadoViewModel;
                alta.DataContext = catalogprogramadoViewModel.CreateAddProgramadoViewModel();
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

        private void dtGridItems_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGrid dg = sender as DataGrid;
                if (dg != null && dg.SelectedItems != null && dg.SelectedItems.Count == 1)
                {
                    ModifyProgramadoView modify = new ModifyProgramadoView();
                    try
                    {
                        CatalogProgramadoViewModel catalogprogramadoViewModel = new CatalogProgramadoViewModel();
                        catalogprogramadoViewModel = this.DataContext as CatalogProgramadoViewModel;
                        modify.DataContext = catalogprogramadoViewModel.CreateModifyProgramadoViewModel();
                        modify.ShowDialog();
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
