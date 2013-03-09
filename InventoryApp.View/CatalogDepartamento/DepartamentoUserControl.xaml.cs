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
using InventoryApp.ViewModel.CatalogDepartamento;

namespace InventoryApp.View.CatalogDepartamento
{
    /// <summary>
    /// Lógica de interacción para DepartamentoUserControl.xaml
    /// </summary>
    public partial class DepartamentoUserControl : UserControl
    {
        public DepartamentoUserControl()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            AddDepartamentoView alta = new AddDepartamentoView();
            try
            {
                CatalogDepartamentoViewModel departamentoViewModel = (this.DataContext as ObjectDataProvider).Data as CatalogDepartamentoViewModel;
                alta.DataContext = departamentoViewModel.CreateAddDepartamentoViewModel();
                alta.ShowDialog();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void dtGridDepartamento_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGrid dg = sender as DataGrid;
                if (dg != null && dg.SelectedItems != null && dg.SelectedItems.Count == 1)
                {
                    ModifyDepartamentoView dlgModify = new ModifyDepartamentoView();
                    try
                    {
                        CatalogDepartamentoViewModel departamentoViewModel = (this.DataContext as ObjectDataProvider).Data as CatalogDepartamentoViewModel;
                        dlgModify.DataContext = departamentoViewModel.CreateDepartamentoViewModel();
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
