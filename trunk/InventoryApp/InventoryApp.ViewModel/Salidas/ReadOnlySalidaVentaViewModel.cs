using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using InventoryApp.DAL;
using System.Windows.Input;
using InventoryApp.DAL.POCOS;
using Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace InventoryApp.ViewModel.Salidas
{
    public class ReadOnlySalidaVentaViewModel
    {
        #region Fields
        object almacenDestino;
        object almacenProcedencia;
        object transporte;
        object infraestructura;
        object clienteProcedencia;
        object clienteDestino;
        object proveedorDestino;
        object proveedorProcedencia;
        object tecnicoProcedencia;
        object tecnicoDestino;
        object solicitante;
        object facturaVenta;
        private MovimientoModel _movimientoModel;
        private CatalogItemModel _itemModel;
        private RelayCommand _imprimirCommand;
        private IDataMapper _dataMapperTransporte;
        private IDataMapper _dataMapperInfraestructura;
        private IDataMapper _dataMapperAlmacenDestino;
        private IDataMapper _dataMapperAlmacenProcedencia;
        private IDataMapper _dataMapperClienteProcedencia;
        private IDataMapper _dataMapperClienteDestino;
        private IDataMapper _dataMapperProveedorDestino;
        private IDataMapper _dataMapperProveedorProcedencia;
        private IDataMapper _dataMapperTecnico;
        private IDataMapper _dataMapperTecnicoDestino;
        private IDataMapper _dataMapperSolicitante;
        private IDataMapper _dataMapperFacturaVenta;
        #endregion

        #region Props
        public MovimientoModel MovimientoModel
        {
            get
            {
                return _movimientoModel;

            }
            set
            {
                _movimientoModel = value;
            }
        }
        public CatalogItemModel ItemModel
        {
            get
            {
                return _itemModel;
            }
            set
            {
                _itemModel = value;
            }
        }
        public ICommand ImprimirCommand
        {
            get
            {
                if (_imprimirCommand == null)
                {
                    _imprimirCommand = new RelayCommand(p => this.AttempImprimir(), p => this.CanAttempImprimir());
                }
                return _imprimirCommand;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Ejecuta la acción del command
        /// </summary>
        /// <param name="catalogItemStatusViewModel"></param>
        public ReadOnlySalidaVentaViewModel(MovimientoModel selectedMovimientoModel)
        {
            this._movimientoModel = new MovimientoModel(new MovimientoDataMapper(), 1);
            this._itemModel = new CatalogItemModel(new ItemDataMapper());
            DeleteMovimiento movLectura = new DeleteMovimiento();

            //consulta por unidmovimiento
            movLectura.GetMovimientos(selectedMovimientoModel.UnidMovimiento);

            this._dataMapperTransporte = new TransporteDataMapper();
            this._dataMapperInfraestructura = new InfraestructuraDataMapper();
            this._dataMapperAlmacenDestino = new AlmacenDataMapper();
            this._dataMapperAlmacenProcedencia = new AlmacenDataMapper();
            this._dataMapperClienteDestino = new ClienteDataMapper();
            this._dataMapperClienteProcedencia = new ClienteDataMapper();
            this._dataMapperProveedorDestino = new ProveedorDataMapper();
            this._dataMapperProveedorProcedencia = new ProveedorDataMapper();
            this._dataMapperTecnico = new TecnicoDataMapper();
            this._dataMapperTecnicoDestino = new TecnicoDataMapper();
            this._dataMapperSolicitante = new SolicitanteDataMapper();
            this._dataMapperFacturaVenta = new FacturaVentaDataMapper();

            //consulta 
            if (movLectura.AlmacenDestino != null && movLectura.AlmacenDestino.UNID_ALMACEN != 0)
                almacenDestino = this._dataMapperAlmacenDestino.getElement(movLectura.AlmacenDestino);

            if (movLectura.AlmacenProcedencia != null && movLectura.AlmacenProcedencia.UNID_ALMACEN != 0)
                almacenProcedencia = this._dataMapperAlmacenProcedencia.getElement(movLectura.AlmacenProcedencia);

            if (movLectura.ClienteProcedencia != null && movLectura.ClienteProcedencia.UNID_CLIENTE != 0)
                clienteProcedencia = this._dataMapperClienteProcedencia.getElement(movLectura.ClienteProcedencia);

            if (movLectura.ClienteDestino != null && movLectura.ClienteDestino.UNID_CLIENTE != 0)
                clienteDestino = this._dataMapperClienteDestino.getElement(movLectura.ClienteDestino);

            if (movLectura.UnidInfraestructura != null && movLectura.UnidInfraestructura.UNID_INFRAESTRUCTURA != 0)
                infraestructura = this._dataMapperInfraestructura.getElement(movLectura.UnidInfraestructura);

            if (movLectura.ProveedorProcedenia != null && movLectura.ProveedorProcedenia.UNID_PROVEEDOR != 0)
                proveedorProcedencia = this._dataMapperProveedorProcedencia.getElement(movLectura.ProveedorProcedenia);

            if (movLectura.ProveedorDestino != null && movLectura.ProveedorDestino.UNID_PROVEEDOR != 0)
                proveedorDestino = this._dataMapperProveedorDestino.getElement(movLectura.ProveedorDestino);

            if (movLectura.Transporte != null && movLectura.Transporte.UNID_TRANSPORTE != 0)
                transporte = this._dataMapperTransporte.getElement(movLectura.Transporte);

            if (movLectura.UnidTecnico != null && movLectura.UnidTecnico.UNID_TECNICO != 0)
                tecnicoProcedencia = this._dataMapperTecnico.getElement(movLectura.UnidTecnico);

            if (movLectura.UnidTecnicoTrans != null && movLectura.UnidTecnicoTrans.UNID_TECNICO != 0)
                tecnicoDestino = this._dataMapperTecnicoDestino.getElement(movLectura.UnidTecnicoTrans);

            if (movLectura.UnidSolicitante != null && movLectura.UnidSolicitante.UNID_SOLICITANTE != 0)
                solicitante = this._dataMapperSolicitante.getElement(movLectura.UnidSolicitante);

            if (movLectura.UnidFacturaVenta != null && movLectura.UnidFacturaVenta.UNID_FACTURA_VENTA != 0)
                facturaVenta = this._dataMapperFacturaVenta.getElement(movLectura.UnidFacturaVenta);

            //asignacion a propiedades de solo lectura

            this._movimientoModel.UnidMovimiento = selectedMovimientoModel.UnidMovimiento;


            if (almacenDestino != null)
                this._movimientoModel.AlmacenDestino = almacenDestino as ALMACEN;

            if (almacenProcedencia != null)
                this._movimientoModel.AlmacenProcedenciaLectura = almacenProcedencia as ALMACEN;


            if (clienteProcedencia != null)
                this._movimientoModel.ClienteProcedenciaLectura = clienteProcedencia as CLIENTE;

            if (clienteDestino != null)
                this._movimientoModel.ClienteDestinoLectura = clienteDestino as CLIENTE;

            if (infraestructura != null)
                this._movimientoModel.Infraestructura = infraestructura as INFRAESTRUCTURA;

            if (proveedorProcedencia != null)
                this._movimientoModel.ProveedorProcedencia = proveedorProcedencia as PROVEEDOR;

            if (proveedorDestino != null)
                this._movimientoModel.ProveedorDestinoLectura = proveedorDestino as PROVEEDOR;

            if (tecnicoProcedencia != null)
                this._movimientoModel.Tecnico = tecnicoProcedencia as TECNICO;

            if (tecnicoDestino != null)
                this._movimientoModel.TecnicoTrnas = tecnicoDestino as TECNICO;

            if (facturaVenta != null)
            {
                this._movimientoModel.FacturaVentaLectura = facturaVenta as FACTURA_VENTA;
                this._movimientoModel.calculaTotal();
                
            }
            if (solicitante != null)
            {
                this._movimientoModel.SolicitanteLectura = solicitante as SOLICITANTE;
                this._movimientoModel.EmpresaLectura = ((SOLICITANTE)solicitante).EMPRESA;
                this._movimientoModel.DepartamentoLectura = ((SOLICITANTE)solicitante).DEPARTAMENTO;
            }

            if (transporte != null)
                this._movimientoModel.Transporte = transporte as TRANSPORTE;

            this._movimientoModel.Tt = movLectura.Tt;
            this._movimientoModel.SitioEnlace = movLectura.SitioEnlace;
            this._movimientoModel.NombreSitio = movLectura.NombreSitio;
            this._movimientoModel.Guia = movLectura.Guia;
            this._movimientoModel.Contacto = movLectura.Contacto;
            this._movimientoModel.FechaMovimiento = movLectura.TimeFecha;
            //carga el grid
            if (movLectura.ArticulosLectura != null)
            {
                this._itemModel.ItemModel = movLectura.ArticulosLectura;
                this._movimientoModel.CantidadItems = movLectura.ArticulosLectura.Count;  
            }
                
            

        }
        public ReadOnlySalidaVentaViewModel() { }
        #endregion

        #region Metodos
        public bool CanAttempImprimir()
        {
            bool _canImprimir = true;

            //if (this.ItemModel.ItemModel.Count() != 0 && !String.IsNullOrEmpty(this.MovimientoModel.NombreSitio) && !String.IsNullOrEmpty(this.MovimientoModel.Contacto) && !String.IsNullOrEmpty(this.MovimientoModel.Tt))
            //    _canImprimir = true;

            return _canImprimir;
        }

        public void AttempImprimir()
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.DefaultExt = ".xlsx";
            dlg.Filter = "Documentos Excel (.xlsx)|*.xlsx";

                if (dlg.ShowDialog() == true)
                {
                    string filename = dlg.FileName;
                    Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                    excel.Visible = true;

                    Workbook excelPrint = excel.Workbooks.Open(@"C:\Programs\ElaraInventario\Resources\SalidaVenta.xlsx", Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                    Worksheet excelSheetPrint = (Worksheet)excelPrint.Worksheets[1];

                    //Folio
                    excel.Cells[8, 6] = _movimientoModel.UnidMovimiento.ToString();
                    //Fecha
                    excel.Cells[8, 23] = _movimientoModel.FechaMovimiento;

                    //Solicitante y su área
                    excel.Cells[13, 12] = _movimientoModel.SolicitanteLectura.SOLICITANTE_NAME;
                    excel.Cells[15, 12] = _movimientoModel.DepartamentoLectura.DEPARTAMENTO_NAME;
                    try
                    {
                        //Recibe
                        excel.Cells[21, 12] = _movimientoModel.Tecnico.TECNICO_NAME;
                        //Procedencia                
                        excel.Cells[17, 12] = "Almacén: " + _movimientoModel.AlmacenProcedenciaLectura.ALMACEN_NAME;
                        //Destino
                        string p = "";

                        if (_movimientoModel.ProveedorDestinoLectura != null)
                            p = "Proveedor : " + _movimientoModel.ProveedorDestinoLectura.PROVEEDOR_NAME;
                        else
                            p = "Cliente: " + _movimientoModel.ClienteDestinoLectura.CLIENTE1;
                        excel.Cells[19, 12] = p;
                    //TT
                    excel.Cells[31, 12] = _movimientoModel.Tt;
                    //Empresa
                    excel.Cells[11, 12] = _movimientoModel.EmpresaLectura.EMPRESA_NAME;
                    //Transporte
                    excel.Cells[25, 12] = _movimientoModel.Transporte.TRANSPORTE_NAME;
                    //Contacto
                    excel.Cells[29, 12] = _movimientoModel.Contacto;
                    //Guia
                    excel.Cells[27, 12] = _movimientoModel.Guia;
                    //Nombre de Sitio
                    excel.Cells[23, 12] = _movimientoModel.NombreSitio;
                    //No. de Factura
                    excel.Cells[33, 12] = _movimientoModel.FacturaVentaLectura.FOLIO;
                    //Importe
                    excel.Cells[35, 12] = enletras(_movimientoModel.TotalLectura.ToString(), _movimientoModel.FacturaVentaLectura.MONEDA.MONEDA_NAME);
                    }
                    catch (Exception Ex)
                    {

                    }
                    int X = 43;
                    Microsoft.Office.Interop.Excel.Borders borders;

                    for (int i = 0; i < ItemModel.ItemModel.Count; i++)
                    {
                        excel.Range[excel.Cells[X, 2], excel.Cells[X, 3]].Merge();
                        excel.Range[excel.Cells[X, 2], excel.Cells[X, 3]].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        excel.Cells[X, 2] = (i + 1).ToString() + ".-";
                        borders = excel.Range[excel.Cells[X, 2], excel.Cells[X, 3]].Borders;
                        borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                        //DESCRIPCIÓN
                        excel.Range[excel.Cells[X, 4], excel.Cells[X, 22]].Merge();
                        excel.Range[excel.Cells[X, 4], excel.Cells[X, 22]].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        excel.Cells[X, 4] = ItemModel.ItemModel[i].Articulo.ARTICULO1;
                        borders = excel.Range[excel.Cells[X, 4], excel.Cells[X, 22]].Borders;
                        borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                        //N° DE SERIE
                        excel.Range[excel.Cells[X, 23], excel.Cells[X, 26]].Merge();
                        excel.Range[excel.Cells[X, 23], excel.Cells[X, 26]].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        excel.Cells[X, 23] = ItemModel.ItemModel[i].NUMERO_SERIE;
                        borders = excel.Range[excel.Cells[X, 23], excel.Cells[X, 26]].Borders;
                        borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                        //SKU
                        excel.Range[excel.Cells[X, 27], excel.Cells[X, 30]].Merge();
                        excel.Range[excel.Cells[X, 27], excel.Cells[X, 30]].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        excel.Cells[X, 27] = ItemModel.ItemModel[i].SKU;
                        borders = excel.Range[excel.Cells[X, 27], excel.Cells[X, 30]].Borders;
                        borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                        //CANTIDAD
                        excel.Range[excel.Cells[X, 31], excel.Cells[X, 34]].Merge();
                        excel.Range[excel.Cells[X, 31], excel.Cells[X, 34]].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        excel.Cells[X, 31] = ItemModel.ItemModel[i].CantidadMovimiento;
                        borders = excel.Range[excel.Cells[X, 31], excel.Cells[X, 34]].Borders;
                        borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                        X++;
                    }

                    X += 2;
                    excel.Cells[X, 3] = "OBSERVACIONES:";
                    excel.Range[excel.Cells[X, 9], excel.Cells[X + 2, 33]].Merge();
                    borders = excel.Range[excel.Cells[X, 9], excel.Cells[X + 2, 33]].Borders;
                    borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;

                    X += 4;
                    excel.Range[excel.Cells[X, 2], excel.Cells[X, 17]].Merge();
                    excel.Range[excel.Cells[X, 2], excel.Cells[X, 17]].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    excel.Cells[X, 2] = "ENTREGADO POR:";
                    excel.Cells[X, 2].Font.Bold = true;
                    excel.Range[excel.Cells[X, 18], excel.Cells[X, 34]].Merge();
                    excel.Range[excel.Cells[X, 18], excel.Cells[X, 34]].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    excel.Cells[X, 18] = "RECIBIDO POR:";
                    excel.Cells[X, 18].Font.Bold = true;
                    X += 1;
                    excel.Range[excel.Cells[X, 2], excel.Cells[X + 2, 17]].Merge();
                    borders = excel.Range[excel.Cells[X, 2], excel.Cells[X + 2, 17]].Borders;
                    borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    excel.Range[excel.Cells[X, 18], excel.Cells[X + 2, 34]].Merge();
                    borders = excel.Range[excel.Cells[X, 18], excel.Cells[X + 2, 34]].Borders;
                    borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;



                    excelSheetPrint.SaveAs(filename, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                }
            
            
        }

        public string enletras(string num, string divisa)
        {
            string res, dec = "";
            Int64 entero;
            int decimales;
            double nro;
            string divisaFin = "";

            try
            {
                nro = Convert.ToDouble(num);
            }
            catch
            {
                return "";
            }



            entero = Convert.ToInt64(Math.Truncate(nro));

            if (entero == 1)
            {
                if (divisa.Equals("DÓLAR AMÉRICANO"))
                    divisaFin = " DÓLAR ";
                else if (divisa.Equals("PESOS MEXICANOS"))
                    divisaFin = " PESO MEXICANO ";
                else if (divisa.Equals("PESOS COLOMBIANOS"))
                    divisaFin = " PESO COLOMBIANO ";
                else if (divisa.Equals("EUROS"))
                    divisaFin = " EURO ";
            }
            else
            {

                if (divisa.Equals("DÓLAR AMÉRICANO"))
                    divisaFin = " DÓLARES ";
                else if (divisa.Equals("PESOS MEXICANOS"))
                    divisaFin = " PESOS MEXICANOS ";
                else if (divisa.Equals("PESOS COLOMBIANOS"))
                    divisaFin = " PESOS COLOMBIANOS ";
                else if (divisa.Equals("EUROS"))
                    divisaFin = " EUROS ";
            }

            decimales = Convert.ToInt32(Math.Round((nro - entero) * 100, 2));
            if (decimales > 0)
            {
                res = toText(Convert.ToDouble(entero)) + " " + divisaFin + "CON " + toText(Convert.ToDouble(decimales)) + " CENTAVOS ";
            }
            else
            {

                res = toText(Convert.ToDouble(entero)) + " " + divisaFin;
            }

            return res;
        }

        private string toText(double value)
        {

            string Num2Text = "";
            value = Math.Truncate(value);
            if (value == 0) Num2Text = "CERO";
            else if (value == 1) Num2Text = "UNO";
            else if (value == 2) Num2Text = "DOS";
            else if (value == 3) Num2Text = "TRES";
            else if (value == 4) Num2Text = "CUATRO";
            else if (value == 5) Num2Text = "CINCO";
            else if (value == 6) Num2Text = "SEIS";
            else if (value == 7) Num2Text = "SIETE";
            else if (value == 8) Num2Text = "OCHO";
            else if (value == 9) Num2Text = "NUEVE";
            else if (value == 10) Num2Text = "DIEZ";
            else if (value == 11) Num2Text = "ONCE";
            else if (value == 12) Num2Text = "DOCE";
            else if (value == 13) Num2Text = "TRECE";
            else if (value == 14) Num2Text = "CATORCE";
            else if (value == 15) Num2Text = "QUINCE";
            else if (value < 20) Num2Text = "DIECI" + toText(value - 10);
            else if (value == 20) Num2Text = "VEINTE";
            else if (value < 30) Num2Text = "VEINTI" + toText(value - 20);
            else if (value == 30) Num2Text = "TREINTA";
            else if (value == 40) Num2Text = "CUARENTA";
            else if (value == 50) Num2Text = "CINCUENTA";
            else if (value == 60) Num2Text = "SESENTA";
            else if (value == 70) Num2Text = "SETENTA";
            else if (value == 80) Num2Text = "OCHENTA";
            else if (value == 90) Num2Text = "NOVENTA";
            else if (value < 100) Num2Text = toText(Math.Truncate(value / 10) * 10) + " Y " + toText(value % 10);
            else if (value == 100) Num2Text = "CIEN";
            else if (value < 200) Num2Text = "CIENTO " + toText(value - 100);
            else if ((value == 200) || (value == 300) || (value == 400) || (value == 600) || (value == 800)) Num2Text = toText(Math.Truncate(value / 100)) + "CIENTOS";
            else if (value == 500) Num2Text = "QUINIENTOS";
            else if (value == 700) Num2Text = "SETECIENTOS";
            else if (value == 900) Num2Text = "NOVECIENTOS";
            else if (value < 1000) Num2Text = toText(Math.Truncate(value / 100) * 100) + " " + toText(value % 100);
            else if (value == 1000) Num2Text = "MIL";
            else if (value < 2000) Num2Text = "MIL " + toText(value % 1000);
            else if (value < 1000000)
            {
                Num2Text = toText(Math.Truncate(value / 1000)) + " MIL";
                if ((value % 1000) > 0) Num2Text = Num2Text + " " + toText(value % 1000);
            }

            else if (value == 1000000) Num2Text = "UN MILLON";
            else if (value < 2000000) Num2Text = "UN MILLON " + toText(value % 1000000);
            else if (value < 1000000000000)
            {
                Num2Text = toText(Math.Truncate(value / 1000000)) + " MILLONES ";
                if ((value - Math.Truncate(value / 1000000) * 1000000) > 0) Num2Text = Num2Text + " " + toText(value - Math.Truncate(value / 1000000) * 1000000);
            }

            else if (value == 1000000000000) Num2Text = "UN BILLON";
            else if (value < 2000000000000) Num2Text = "UN BILLON " + toText(value - Math.Truncate(value / 1000000000000) * 1000000000000);

            else
            {
                Num2Text = toText(Math.Truncate(value / 1000000000000)) + " BILLONES";
                if ((value - Math.Truncate(value / 1000000000000) * 1000000000000) > 0) Num2Text = Num2Text + " " + toText(value - Math.Truncate(value / 1000000000000) * 1000000000000);
            }
            return Num2Text;
        }

        #endregion
    }
}
