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
using InventoryApp.ViewModel.CatalogMarca;

namespace InventoryApp.View.CatalogMarca
{
    /// <summary>
    /// Lógica de interacción para MarcaUserControl.xaml
    /// </summary>
    public partial class MarcaUserControl : UserControl
    {
        public MarcaUserControl()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            AddMarca alta = new AddMarca();
            try
            {
                CatalogMarcaViewModel marcaViewModel = (this.DataContext as ObjectDataProvider).Data as CatalogMarcaViewModel;
                alta.DataContext = marcaViewModel.CreateAddMarcaViewModel();
                alta.ShowDialog();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private void dtGridMarca_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGrid dg = sender as DataGrid;
                if (dg != null && dg.SelectedItems != null && dg.SelectedItems.Count == 1)
                {
                    ModifyMarcaView dlgModify = new ModifyMarcaView();
                    try
                    {
                        CatalogMarcaViewModel marcaViewModel = (this.DataContext as ObjectDataProvider).Data as CatalogMarcaViewModel;
                        dlgModify.DataContext = marcaViewModel.CreateModifyMarcaViewModel();
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
