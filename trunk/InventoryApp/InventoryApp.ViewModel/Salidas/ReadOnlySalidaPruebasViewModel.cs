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
using InventoryApp.ViewModel.Historial;

namespace InventoryApp.ViewModel.Salidas
{
    public class ReadOnlySalidaPruebasViewModel
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
        public USUARIO ActualUser;
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
        public ReadOnlySalidaPruebasViewModel(MovimientoModel selectedMovimientoModel, USUARIO u)
        {
            this._movimientoModel = new MovimientoModel(new MovimientoDataMapper(), 1, u);
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


            this.ActualUser = u;
        }
        public ReadOnlySalidaPruebasViewModel() { }
        #endregion

        #region Metodos
        public HistorialViewModel CreateHistorialViewModel()
        {
            HistorialViewModel historialViewModel = new HistorialViewModel(this.MovimientoModel);
            return historialViewModel;
        }

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

                Workbook excelPrint = excel.Workbooks.Open(@"C:\Programs\ElaraInventario\Resources\SalidaPruebas.xlsx", Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
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
                }
                catch (Exception Ex)
                {

                }
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

                int X = 38;
                Microsoft.Office.Interop.Excel.Borders borders;

                for (int i = 0; i < ItemModel.ItemModel.Count; i++)
                {
                    //for (int i = 0; i < 5; i++)  {

                    //No.
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
                excel.Visible = true;
            }
        }
        #endregion
    }
}
