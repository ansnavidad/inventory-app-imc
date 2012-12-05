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
using Microsoft.Reporting.WinForms;

namespace InventoryApp.View.Reportes
{
    /// <summary>
    /// Lógica de interacción para ReporteInventarioMovimientosView.xaml
    /// </summary>
    public partial class ReporteInventarioMovimientosView : UserControl
    {
        public ReporteInventarioMovimientosView()
        {
            InitializeComponent();

            this._reportViewer.ProcessingMode = ProcessingMode.Local;
            this._reportViewer.LocalReport.ReportPath = @"C:\Users\Administrador\Documents\Visual Studio 2010\Projects\InventoryApp2\InventoryApp\InventoryApp.View\Reportes\Inventario-Movimientos - copia.rdlc";
            this._reportViewer.RefreshReport();
        }
    }
}
