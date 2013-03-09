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
using InventoryApp.ViewModel.CatalogTecnico;

namespace InventoryApp.View.CatalogTecnico
{
    /// <summary>
    /// Lógica de interacción para TecnicoUserControl.xaml
    /// </summary>
    public partial class TecnicoUserControl : UserControl
    {
        public TecnicoUserControl()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            AltaTecnico alta = new AltaTecnico();
            try
            {
                CatalogTecnicoViewModel tecnicoViewModel = (this.DataContext as ObjectDataProvider).Data as CatalogTecnicoViewModel;
                alta.DataContext = tecnicoViewModel.CreateAddTecnicoViewModel();
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
                    ModifyTecnicoView dlgModify = new ModifyTecnicoView();
                    try
                    {
                        CatalogTecnicoViewModel tecnicoViewModel = (this.DataContext as ObjectDataProvider).Data as CatalogTecnicoViewModel;
                        dlgModify.DataContext = tecnicoViewModel.CreateModifyTecnicoViewModel();
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
