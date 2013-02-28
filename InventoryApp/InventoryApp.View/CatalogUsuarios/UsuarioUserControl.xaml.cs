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
using InventoryApp.ViewModel.CatalogUsuarios;

namespace InventoryApp.View.CatalogUsuarios
{
    /// <summary>
    /// Lógica de interacción para UsuarioUserControl.xaml
    /// </summary>
    public partial class UsuarioUserControl : UserControl
    {
        public UsuarioUserControl()
        {
            InitializeComponent();
        }

        private void dataGridUser_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender != null)
            {
                DataGrid dg = sender as DataGrid;
                if (dg != null && dg.SelectedItems != null && dg.SelectedItems.Count == 1)
                {
                    ModifyUsuarioView modify = new ModifyUsuarioView();
                    try
                    {
                        
                        CatalogUsuarioViewModel catalogUsuarioViewModel = (this.DataContext as ObjectDataProvider).Data as CatalogUsuarioViewModel;
                        modify.DataContext = catalogUsuarioViewModel.CreateModifyUsuarioViewModel();
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
