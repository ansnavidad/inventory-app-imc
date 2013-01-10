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
using InventoryApp.ViewModel.MaxMin;

namespace InventoryApp.View.MaxMin
{
    /// <summary>
    /// Lógica de interacción para AddMaxMinView.xaml
    /// </summary>
    public partial class AddMaxMinView : Window
    {
        public AddMaxMinView()
        {
            InitializeComponent();
        }

        private void btnAgregarArticulo_Click(object sender, RoutedEventArgs e)
        {
            AddArticuloView alta = new AddArticuloView();
            try
            {
                AddMaxMinViewModel addMaxMinViewModel = new AddMaxMinViewModel();
                addMaxMinViewModel = this.DataContext as AddMaxMinViewModel;
                alta.DataContext = addMaxMinViewModel.CreateAddArticuloMaxMinViewModel();
                alta.ShowDialog();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
