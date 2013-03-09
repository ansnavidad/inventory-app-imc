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
using InventoryApp.ViewModel.CatalogCategoria;

namespace InventoryApp.View.CatalogCategoria
{
    /// <summary>
    /// Lógica de interacción para CategoriaUserControl.xaml
    /// </summary>
    public partial class CategoriaUserControl : UserControl
    {
        public CategoriaUserControl()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            AltaCategoria alta = new AltaCategoria();
            try
            {
                CatalogCategoriaViewModel categoriaViewModel = (this.DataContext as ObjectDataProvider).Data as CatalogCategoriaViewModel;
                alta.DataContext = categoriaViewModel.CreateAddCategoriaViewModel();
                alta.ShowDialog();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        private void dtGridCategoria_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGrid dg = sender as DataGrid;
                if (dg != null && dg.SelectedItems != null && dg.SelectedItems.Count == 1)
                {
                    ModifyCategoriaView dlgModify = new ModifyCategoriaView();
                    try
                    {
                        CatalogCategoriaViewModel categoriaViewModel = (this.DataContext as ObjectDataProvider).Data as CatalogCategoriaViewModel;
                        dlgModify.DataContext = categoriaViewModel.CreateModifyCategoriaViewModel();
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
