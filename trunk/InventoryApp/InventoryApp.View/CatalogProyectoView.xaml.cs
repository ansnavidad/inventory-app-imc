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
using InventoryApp.ViewModel;

namespace InventoryApp.View
{
    /// <summary>
    /// Lógica de interacción para CatalogProyectoView.xaml
    /// </summary>
    public partial class CatalogProyectoView : Window
    {
        public CatalogProyectoView()
        {
            //CatalogProyectoViewModel cat = new CatalogProyectoViewModel();
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            AltaProyecto alta = new AltaProyecto();
            alta.ShowDialog();
        }
    }
}
