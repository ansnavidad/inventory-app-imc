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
using InventoryApp.ViewModel.CatalogProgramado;

namespace InventoryApp.View.CatalogProgramado
{
    /// <summary>
    /// Lógica de interacción para AddProgramadoView.xaml
    /// </summary>
    public partial class AddProgramadoView : Window
    {
        public AddProgramadoView()
        {
            InitializeComponent();
        }

        private void btnAgregarArticulo_Click(object sender, RoutedEventArgs e)
        {
             AddArticuloView altaView = new AddArticuloView();
            AddProgramadoViewModel addArticuloViewModel = this.DataContext as AddProgramadoViewModel;
            altaView.DataContext = addArticuloViewModel.CreateAddArticuloProgramadoViewModel();
            altaView.ShowDialog();
        }
    }
}
