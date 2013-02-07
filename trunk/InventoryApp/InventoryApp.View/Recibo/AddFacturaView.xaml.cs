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
    }
}
