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
    /// Lógica de interacción para ModifyFacturaView.xaml
    /// </summary>
    public partial class ModifyFacturaView : UserControl
    {
        public ModifyFacturaView()
        {
            InitializeComponent();
        }

        private void btnFacturaAdd_Click(object sender, RoutedEventArgs e)
        {
            ModifyFacturaViewModel addFacturaViewModel = this.DataContext as ModifyFacturaViewModel;
            if (addFacturaViewModel != null)
            {
                DlgAddFacturaArticuloView addFacturaArticuloView = new DlgAddFacturaArticuloView();
                addFacturaArticuloView.DataContext = addFacturaViewModel.CreateAddFacturaArticuloViewModel();
                addFacturaArticuloView.ShowDialog();
            }
        }
    }
}
