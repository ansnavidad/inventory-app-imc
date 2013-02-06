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
using InventoryApp.ViewModel.CatalogItem;
using InventoryApp.View.Recibo;

namespace InventoryApp.View.CatalogItem
{
    /// <summary>
    /// Lógica de interacción para AddItemView.xaml
    /// </summary>
    public partial class AddItemView : UserControl
    {
        public AddItemView()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            AddFactura addFactura = new AddFactura();
            AgregarItemViewModel viewModel = this.DataContext as AgregarItemViewModel;
            addFactura.DataContext = viewModel.CreateAgregarFacturaViewModel(); ;
            addFactura.ShowDialog();
        }



        private void button1_Click(object sender, RoutedEventArgs e)
        {
            AddDestinoView addFactura = new AddDestinoView();
            AgregarItemViewModel viewModel = this.DataContext as AgregarItemViewModel;
            addFactura.DataContext = viewModel.CreateAgregarDestinoViewModel(); ;
            addFactura.Show();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {

        }

        private void checkBox1_Checked(object sender, RoutedEventArgs e)
        {
            comboPropiedad.IsEnabled = true;
            comboPropiedad.SelectedIndex = 0;
        }

        private void checkBox1_Unchecked(object sender, RoutedEventArgs e)
        {
            comboPropiedad.IsEnabled = false;
            comboPropiedad.SelectedIndex = -1;
        }

        private void btnAgregarItem_Click(object sender, RoutedEventArgs e)
        {
            DlgAddFacturaArticuloView addFactura = new DlgAddFacturaArticuloView();
            AgregarItemViewModel viewModel = this.ConvertDataContext(this.DataContext);
            addFactura.DataContext = viewModel.CreateAddFacturaArticuloViewModel();
            addFactura.ShowDialog();
        }

        private AgregarItemViewModel ConvertDataContext(object dataSource)
        {
            AgregarItemViewModel viewModel = dataSource as AgregarItemViewModel;
            return viewModel;
        }

    }
}
