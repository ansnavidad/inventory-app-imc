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
using InventoryApp.ViewModel.Recibo;
using InventoryApp.View.Historial;

namespace InventoryApp.View.Recibo
{
    /// <summary>
    /// Lógica de interacción para AddFacturaView.xaml
    /// </summary>
    public partial class AddFacturaView : UserControl
    {
        public AddFacturaView()
        {
            InitializeComponent();
            FocusManager.SetFocusedElement(this, this.txtNumFactura);
        }

        private void btnFacturaAdd_Click(object sender, RoutedEventArgs e)
        {
            IFacturaViewModel facturaViewModel = this.DataContext as IFacturaViewModel;
            if (facturaViewModel != null)
            {
                DlgAddFacturaArticuloView addFacturaArticuloView = new DlgAddFacturaArticuloView();
                addFacturaArticuloView.DataContext = facturaViewModel.CreateFacturaArticuloViewModel();
                addFacturaArticuloView.ShowDialog();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAgregar_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("test");
        }

        private void Historal_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                HistorialView addFactura = new HistorialView();
                ModifyFacturaViewModel viewModel = this.ConvertDataContext(this.DataContext);
                addFactura.DataContext = viewModel.CreateHistorialViewModel();
                addFactura.ShowDialog();
            }
            catch (Exception Ex)
            {
                MessageBox.Show("Este es un registro nuevo que no cuenta con Historial.");
            }
        }

        private ModifyFacturaViewModel ConvertDataContext(object dataSource)
        {
            ModifyFacturaViewModel viewModel = dataSource as ModifyFacturaViewModel;
            return viewModel;
        }
    }
}
