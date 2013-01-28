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

namespace InventoryApp.View.Entradas
{
    /// <summary>
    /// Lógica de interacción para EntradaValidacionRevisionSoloLectura.xaml
    /// </summary>
    public partial class EntradaValidacionRevisionSoloLectura : Window
    {
        public EntradaValidacionRevisionSoloLectura()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
