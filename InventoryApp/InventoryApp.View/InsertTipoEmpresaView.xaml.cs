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
    /// Lógica de interacción para InsertTipoEmpresaView.xaml
    /// </summary>
    public partial class InsertTipoEmpresaView : Window
    {
        public InsertTipoEmpresaView()
        {
            InitializeComponent();
            InsertTipoEmpresaViewModel prueba = new InsertTipoEmpresaViewModel();
            textBox1.DataContext = this.Resources["insertTipoEmpresaViewModel"];
        }

        private void button1_Click_1(object sender, RoutedEventArgs e)
        {
            InsertTipoEmpresaViewModel p = this.Resources["insertTipoEmpresaViewModel"] as InsertTipoEmpresaViewModel;
            p.InsertItem();
        }
    }
}
