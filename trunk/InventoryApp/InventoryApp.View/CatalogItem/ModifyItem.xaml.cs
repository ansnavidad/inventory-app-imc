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
using System.Windows.Shapes;
using InventoryApp.ViewModel.CatalogItem;

namespace InventoryApp.View.CatalogItem
{
    /// <summary>
    /// Lógica de interacción para ModifyItem.xaml
    /// </summary>
    public partial class ModifyItem : UserControl
    {
        public ModifyItem()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            AgregarFactura addFactura = new AgregarFactura();
            ModifyItemViewModel viewModel = this.DataContext as ModifyItemViewModel;
            addFactura.DataContext = viewModel.CreateAgregarFacturaViewModel(); ;
            addFactura.Show();
        }
    }
}
