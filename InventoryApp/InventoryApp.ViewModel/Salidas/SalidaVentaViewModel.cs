using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using InventoryApp.DAL;
using InventoryApp.DAL.POCOS;
using System.Windows.Input;
using System.Collections.ObjectModel;
using InventoryApp.ViewModel;
using System.Windows.Input;
using System.ComponentModel;
using InventoryApp.ViewModel.GridMovimientos;
using Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace InventoryApp.ViewModel.Salidas
{
    public class SalidaVentaViewModel : IPageViewModel, INotifyPropertyChanged
    {
        private CatalogEmpresaModel _catalogEmpresaModel;
        private MovimientoSalidasModel _movimientoModel;
        private MovimientoDetalleModel _movimientoDetalleModel;
        private UltimoMovimientoModel _ultimoMovimientoModel;
        private CatalogSolicitanteModel _catalogSolicitanteModel;
        private CatalogAlmacenModel _catalogAlmacenDestinoModel;
        private CatalogAlmacenModel _catalogAlmacenProcedenciaModel;
        private CatalogProveedorModel _catalogProveedorDestinoModel;
        private CatalogClienteModel _catalogClienteModel;
        private CatalogClienteModel _catalogClienteDestinoModel;
        private CatalogTransporteModel _catalogTransporteModel;
        private CatalogItemModel _itemModel;
        private CatalogServicioModel _catalogServicioModel;
        private CatalogTipoPedimentoModel _catalogTipoPedimentoModel;
        private CatalogMonedaModel _catalogMonedaModel;
        private FacturaVentaModel _facturaVentaModel;
        private CatalogTecnicoModel _catalogTecnicoModel;
        private MovimientoGridSalidaVentaViewModel _movimientoSalida;
        private RelayCommand _addItemCommand;
        private RelayCommand _deleteItemCommand;
        private RelayCommand _imprimirCommand;

        public SalidaVentaViewModel()
        {            
            try
            {
                IDataMapper dataMapper = new SolicitanteDataMapper();
                IDataMapper dataMapper2 = new AlmacenDataMapper();
                IDataMapper dataMapper3 = new ProveedorDataMapper();
                IDataMapper dataMapper4 = new ClienteDataMapper();
                IDataMapper dataMapper5 = new ServicioDataMapper();
                IDataMapper dataMapper6 = new TipoPedimentoDataMapper();
                IDataMapper dataMapper7 = new TransporteDataMapper();
                IDataMapper dataMapper8 = new MonedaDataMapper();
                IDataMapper dataMapper9 = new FacturaVentaDataMapper();
                IDataMapper dataMapper10 = new TecnicoDataMapper();

                this._catalogSolicitanteModel = new CatalogSolicitanteModel(dataMapper);
                this._movimientoModel = new MovimientoSalidasModel(new MovimientoDataMapper());
                TIPO_MOVIMIENTO mov = new TIPO_MOVIMIENTO();
                mov.UNID_TIPO_MOVIMIENTO = 8;
                this._movimientoModel.TipoMovimiento = mov;
                this._movimientoModel.PropertyChanged += OnPropertyChanged2;
                this._itemModel = new CatalogItemModel(new ItemDataMapper());
                this._catalogAlmacenDestinoModel = new CatalogAlmacenModel(dataMapper2);
                this._catalogAlmacenProcedenciaModel = new CatalogAlmacenModel(dataMapper2);
                this._catalogProveedorDestinoModel = new CatalogProveedorModel(dataMapper3);
                this._catalogClienteDestinoModel = new CatalogClienteModel(dataMapper4);
                this._catalogServicioModel = new CatalogServicioModel(dataMapper5);
                this._catalogTipoPedimentoModel = new CatalogTipoPedimentoModel(dataMapper6);
                this._catalogTransporteModel = new CatalogTransporteModel(dataMapper7);
                this._catalogClienteModel = new CatalogClienteModel(dataMapper4);
                this._catalogMonedaModel = new CatalogMonedaModel(dataMapper8);
                this._facturaVentaModel = new FacturaVentaModel(dataMapper9);
                this._catalogTecnicoModel = new CatalogTecnicoModel(dataMapper10);

                //Asignaciones especiales para los combos 
                this._movimientoModel.Empresa = _catalogEmpresaModel.Empresa[0];
                this._movimientoModel.Solicitante = _catalogSolicitanteModel.Solicitante[0];
                this._movimientoModel.Servicio = _catalogServicioModel.Servicio[0];
                this._movimientoModel.Cliente = _catalogClienteModel.Cliente[0];
                this._movimientoModel.AlmacenProcedencia = _catalogAlmacenProcedenciaModel.Almacen[0];
                this._movimientoModel.Tecnico = _movimientoModel.Tecnicos[0];
                this._movimientoModel.AlmacenDestino = _catalogAlmacenDestinoModel.Almacen[0];
                this._movimientoModel.ClienteDestino = _catalogClienteDestinoModel.Cliente[0];
                this._movimientoModel.ProveedorDestino = _catalogProveedorDestinoModel.Proveedor[0];
                this._movimientoModel.Transporte = _catalogTransporteModel.Transporte[0];
                this._facturaVentaModel.Moneda = _catalogMonedaModel.Moneda[0];
            }
            catch (ArgumentException a)
            {
                ;
            }
            catch(Exception ex)
            {
                throw ex;
            }   
            
        }

        public SalidaVentaViewModel(MovimientoGridSalidaVentaViewModel salida)
        {
            try
            {
                IDataMapper dataMapper = new SolicitanteDataMapper();
                IDataMapper dataMapper2 = new AlmacenDataMapper();
                IDataMapper dataMapper3 = new ProveedorDataMapper();
                IDataMapper dataMapper4 = new ClienteDataMapper();
                IDataMapper dataMapper5 = new ServicioDataMapper();
                IDataMapper dataMapper6 = new TipoPedimentoDataMapper();
                IDataMapper dataMapper7 = new TransporteDataMapper();
                IDataMapper dataMapper8 = new MonedaDataMapper();
                IDataMapper dataMapper9 = new FacturaVentaDataMapper();
                IDataMapper dataMapper10 = new TecnicoDataMapper();
                IDataMapper datamapper11 = new EmpresaDataMapper();

                this._catalogEmpresaModel = new CatalogEmpresaModel(datamapper11);

                this._catalogSolicitanteModel = new CatalogSolicitanteModel(dataMapper);
                this._movimientoModel = new MovimientoSalidasModel(new MovimientoDataMapper());
                TIPO_MOVIMIENTO mov = new TIPO_MOVIMIENTO();
                mov.UNID_TIPO_MOVIMIENTO = 8;
                this._movimientoModel.TipoMovimiento = mov;
                this._movimientoSalida = salida;
                this._movimientoModel.PropertyChanged += OnPropertyChanged2;
                this._itemModel = new CatalogItemModel(new ItemDataMapper());
                this._catalogAlmacenDestinoModel = new CatalogAlmacenModel(dataMapper2);
                this._catalogAlmacenProcedenciaModel = new CatalogAlmacenModel(dataMapper2);
                this._catalogProveedorDestinoModel = new CatalogProveedorModel(dataMapper3);
                this._catalogClienteDestinoModel = new CatalogClienteModel(dataMapper4);
                this._catalogServicioModel = new CatalogServicioModel(dataMapper5);
                this._catalogTipoPedimentoModel = new CatalogTipoPedimentoModel(dataMapper6);
                this._catalogTransporteModel = new CatalogTransporteModel(dataMapper7);
                this._catalogClienteModel = new CatalogClienteModel(dataMapper4);
                this._catalogMonedaModel = new CatalogMonedaModel(dataMapper8);
                this._facturaVentaModel = new FacturaVentaModel(dataMapper9);
                this._catalogTecnicoModel = new CatalogTecnicoModel(dataMapper10);

                //Asignaciones especiales para los combos 
                this._movimientoModel.Empresa = _catalogEmpresaModel.Empresa[0];
                this._movimientoModel.Solicitante = _catalogSolicitanteModel.Solicitante[0];
                this._movimientoModel.Servicio = _catalogServicioModel.Servicio[0];
                this._movimientoModel.Cliente = _catalogClienteModel.Cliente[0];
                this._movimientoModel.AlmacenProcedencia = _catalogAlmacenProcedenciaModel.Almacen[0];
                this._movimientoModel.Tecnico = _movimientoModel.Tecnicos[0];
                this._movimientoModel.AlmacenDestino = _catalogAlmacenDestinoModel.Almacen[0];
                this._movimientoModel.ClienteDestino = _catalogClienteDestinoModel.Cliente[0];
                this._movimientoModel.ProveedorDestino = _catalogProveedorDestinoModel.Proveedor[0];
                this._movimientoModel.Transporte = _catalogTransporteModel.Transporte[0];
                this._facturaVentaModel.Moneda = _catalogMonedaModel.Moneda[0];
            }
            catch (ArgumentException a)
            {
                ;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public CatalogEmpresaModel CatalogEmpresaModel
        {
            get
            {
                return _catalogEmpresaModel;
            }
            set
            {
                _catalogEmpresaModel = value;
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
        public ICommand AddItemCommand
        {
            get
            {
                if (_addItemCommand == null)
                {
                    _addItemCommand = new RelayCommand(p => this.AttempArticulo(), p => this.CanAttempArticulo());
                }
                return _addItemCommand;
            }
        }
        public ICommand DeleteItemCommand
        {
            get
            {
                if (_deleteItemCommand == null)
                {
                    _deleteItemCommand = new RelayCommand(p => this.AttempDeleteArticulo(), p => this.CanAttempDeleteArticulo());
                }
                return _deleteItemCommand;
            }
        }
        public CatalogTecnicoModel CatalogTecnicoModel
        {
            get
            {
                return _catalogTecnicoModel;

            }
            set
            {
                if (_catalogTecnicoModel != value)
                {
                    _catalogTecnicoModel = value;
                }
            }
        }
        public CatalogProveedorModel CatalogProveedorDestinoModel
        {
            get
            {
                return _catalogProveedorDestinoModel;

            }
            set
            {
                _catalogProveedorDestinoModel = value;
            }
        }
        public CatalogAlmacenModel CatalogAlmacenDestinoModel
        {
            get
            {
                return _catalogAlmacenDestinoModel;

            }
            set
            {
                _catalogAlmacenDestinoModel = value;
            }
        }

        public FacturaVentaModel FacturaVentaModel
        {
            get
            {
                return _facturaVentaModel;

            }
            set
            {
                _facturaVentaModel = value;
            }
        }

        public CatalogMonedaModel CatalogMonedaModel
        {
            get
            {
                return _catalogMonedaModel;

            }
            set
            {
                _catalogMonedaModel = value;
            }
        }
        public CatalogServicioModel CatalogServicioModel
        {
            get
            {
                return _catalogServicioModel;

            }
            set
            {
                _catalogServicioModel = value;
            }
        }
        public CatalogTipoPedimentoModel CatalogTipoPedimentoModel
        {
            get
            {
                return _catalogTipoPedimentoModel;

            }
            set
            {
                _catalogTipoPedimentoModel = value;
            }
        }
        public CatalogTransporteModel CatalogTransporteModel
        {
            get
            {
                return _catalogTransporteModel;

            }
            set
            {
                _catalogTransporteModel = value;
            }
        }
        public CatalogClienteModel CatalogClienteDestinoModel
        {
            get
            {
                return _catalogClienteDestinoModel;

            }
            set
            {
                _catalogClienteDestinoModel = value;
            }
        }
        public CatalogClienteModel CatalogClienteModel
        {
            get
            {
                return _catalogClienteModel;

            }
            set
            {
                _catalogClienteModel = value;
            }
        }
        public CatalogAlmacenModel CatalogAlmacenProcedenciaModel
        {
            get
            {
                return _catalogAlmacenProcedenciaModel;

            }
            set
            {
                _catalogAlmacenProcedenciaModel = value;
            }
        }
        public MovimientoSalidasModel MovimientoModel
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
        public CatalogSolicitanteModel CatalogSolicitanteModel
        {
            get
            {
                return _catalogSolicitanteModel;
            }
            set
            {
                _catalogSolicitanteModel = value;
            }
        }
        
        public void loadItems()
        {
            this._catalogSolicitanteModel.loadSolicitante();
            this._catalogAlmacenDestinoModel.loadItems();
            this._catalogAlmacenProcedenciaModel.loadItems();
            this._catalogProveedorDestinoModel.loadItems();
            this._catalogClienteDestinoModel.loadCliente();
            this._catalogServicioModel.loadItems();
            this._catalogTipoPedimentoModel.loadItems();
            this._catalogTransporteModel.loadItems();
            this._catalogClienteModel.loadCliente();
        }

        public CatalogItemViewModel CreateCatalogItemViewModel()
        {
            return new CatalogItemViewModel(this); 
        }

        public bool CanAttempArticulo()
        {
            bool _canInsertArticulo = false;

            int seleccion = 0;
            if (this.MovimientoModel.AlmacenDestino != null)
                seleccion++;
            if (this.MovimientoModel.ClienteDestino != null)
                seleccion++;
            if (this.MovimientoModel.ProveedorDestino != null)
                seleccion++;

            if (this.ItemModel.ItemModel.Count() != 0 && !String.IsNullOrEmpty(this.MovimientoModel.NombreSitio) && !String.IsNullOrEmpty(this.MovimientoModel.PedimentoExpo) && !String.IsNullOrEmpty(this.MovimientoModel.DireccionEnvio) && !String.IsNullOrEmpty(this.MovimientoModel.Contacto) && !String.IsNullOrEmpty(this.MovimientoModel.Tt) && !String.IsNullOrEmpty(this.MovimientoModel.Guia) && seleccion == 1)
                _canInsertArticulo = true;
            
            return _canInsertArticulo;
        }

        public void AttempArticulo()
        {
            this._facturaVentaModel.saveFactura();
            this._movimientoModel.UnidFacturaVenta = this.FacturaVentaModel.UnidFacturaVenta;
            this._movimientoModel.saveArticulo();

            foreach (ItemModel item in this._itemModel.ItemModel)
            {
                
                this._movimientoDetalleModel = new MovimientoDetalleModel(new MovimientoDetalleDataMapper(), this._movimientoModel.UnidMovimiento, item.UnidItem);
                this._movimientoDetalleModel.saveArticulo();
                this._ultimoMovimientoModel = new UltimoMovimientoModel(new UltimoMovimientoDataMapper(), item.UnidItem, this._movimientoModel.UnidAlmacenDestino, this._movimientoModel.UnidClienteDestino, this._movimientoModel.UnidProveedorDestino, this._movimientoDetalleModel.UnidMovimientoDetalle);
                this._ultimoMovimientoModel.saveArticulo();
            }            
        }

        public bool CanAttempImprimir()
        {
            bool _canImprimir = false;

            int seleccion = 0;
            if (this.MovimientoModel.AlmacenDestino != null)
                seleccion++;
            if (this.MovimientoModel.ClienteDestino != null)
                seleccion++;
            if (this.MovimientoModel.ProveedorDestino != null)
                seleccion++;

            if (this.ItemModel.ItemModel.Count() != 0 && !String.IsNullOrEmpty(this.MovimientoModel.NombreSitio) && !String.IsNullOrEmpty(this.MovimientoModel.PedimentoExpo) && !String.IsNullOrEmpty(this.MovimientoModel.DireccionEnvio) && !String.IsNullOrEmpty(this.MovimientoModel.Contacto) && !String.IsNullOrEmpty(this.MovimientoModel.Tt) && !String.IsNullOrEmpty(this.MovimientoModel.Guia) && seleccion == 1)
                _canImprimir = true;

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

                Workbook excelPrint = excel.Workbooks.Open(@"C:\temp\elarainventarios\SalidaVenta.xlsx", Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                Worksheet excelSheetPrint = (Worksheet)excelPrint.Worksheets[1];

                //Folio
                excel.Cells[8, 6] = _movimientoModel.UnidMovimiento.ToString();
                //Fecha
                excel.Cells[8, 23] = _movimientoModel.FechaMovimiento;

                //Solicitante y su área
                excel.Cells[11, 12] = _movimientoModel.Solicitante.SOLICITANTE_NAME;
                excel.Cells[13, 12] = _movimientoModel.Solicitante.Departamento.DEPARTAMENTO_NAME;
                //Recibe
                excel.Cells[15, 12] = _movimientoModel.Tecnico.TECNICO_NAME;
                //Procedencia                
                excel.Cells[17, 12] = _movimientoModel.AlmacenProcedencia.ALMACEN_NAME;
                //Destino
                string p = "";

                if (_movimientoModel.ProveedorDestino != null)
                    p = _movimientoModel.ProveedorDestino.PROVEEDOR_NAME;
                else if (_movimientoModel.AlmacenDestino != null)
                    p = _movimientoModel.AlmacenDestino.ALMACEN_NAME;
                else
                    p = _movimientoModel.ClienteDestino.CLIENTE1;
                excel.Cells[19, 12] = p;
                //TT
                excel.Cells[21, 12] = _movimientoModel.Tt;
                //Empresa
                excel.Cells[23, 12] = _movimientoModel.Empresa.EMPRESA_NAME;
                //Transporte
                excel.Cells[25, 12] = _movimientoModel.Transporte.TRANSPORTE_NAME;
                //Contacto
                excel.Cells[27, 12] = _movimientoModel.Contacto;
                //Guia
                excel.Cells[29, 12] = _movimientoModel.Guia;
                //Nombre de Sitio
                excel.Cells[31, 12] = _movimientoModel.NombreSitio;
                //Dirección
                excel.Cells[33, 12] = _movimientoModel.DireccionEnvio;
                //Servicio
                excel.Cells[35, 12] = _movimientoModel.Servicio.SERVICIO_NAME;
                //Cliente
                excel.Cells[37, 12] = _movimientoModel.Cliente.CLIENTE1;
                //Pedimento Expo
                excel.Cells[39, 12] = _movimientoModel.PedimentoExpo;
                //No. de Factura
                excel.Cells[41, 12] = _facturaVentaModel.Folio;
                //Importe
                excel.Cells[43, 12] = enletras(FacturaVentaModel.Total.ToString(), FacturaVentaModel.Moneda.MONEDA_NAME);

                int X = 51;
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
                    excel.Range[excel.Cells[X, 4], excel.Cells[X, 26]].Merge();
                    excel.Range[excel.Cells[X, 4], excel.Cells[X, 26]].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    excel.Cells[X, 4] = ItemModel.ItemModel[i].Articulo.ARTICULO1;
                    borders = excel.Range[excel.Cells[X, 4], excel.Cells[X, 26]].Borders;
                    borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    //N° DE SERIE
                    excel.Range[excel.Cells[X, 27], excel.Cells[X, 30]].Merge();
                    excel.Range[excel.Cells[X, 27], excel.Cells[X, 30]].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    excel.Cells[X, 27] = ItemModel.ItemModel[i].NUMERO_SERIE;
                    borders = excel.Range[excel.Cells[X, 27], excel.Cells[X, 30]].Borders;
                    borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    //MODELO
                    excel.Range[excel.Cells[X, 31], excel.Cells[X, 34]].Merge();
                    excel.Range[excel.Cells[X, 31], excel.Cells[X, 34]].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    excel.Cells[X, 31] = ItemModel.ItemModel[i].Modelo.MODELO_NAME;
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

        public bool CanAttempDeleteArticulo()
        {
            bool _canDeleteArticulo = true;
            return _canDeleteArticulo;
        }

        public void AttempDeleteArticulo()
        {
            for (int i = 0; i < this._itemModel.ItemModel.Count; )
            {

                if (this._itemModel.ItemModel[i].IsChecked)
                    this._itemModel.ItemModel.RemoveAt(i);
                else
                    i++;
            }

            this.MovimientoModel.CantidadItems = this.ItemModel.ItemModel.Count();            
        }

        public string PageName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }


        private ObservableCollection<TECNICO> _tecnicos;
        public ObservableCollection<TECNICO> Tecnicos
        {
            get { return _tecnicos; }
            set
            {
                if (_tecnicos != value)
                {
                    _tecnicos = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Tecnicos"));
                    }
                }
            }
        }

        private ObservableCollection<TECNICO> GetTecnicosByAlmacen(ALMACEN alm)
        {
            ObservableCollection<TECNICO> almCollec = new ObservableCollection<TECNICO>();
            try
            {
                TecnicoDataMapper artDataMapper = new TecnicoDataMapper();
                List<TECNICO> tecnicoss = (List<TECNICO>)artDataMapper.getTecnicosByAlmancen(alm);

                foreach (TECNICO t in tecnicoss)
                {

                    almCollec.Add(t);
                }
            }
            catch (Exception)
            {
                ;
            }

            return almCollec;
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
                else if(divisa.Equals("PESOS MEXICANOS"))
                    divisaFin = " PESO MEXICANO ";
                else if(divisa.Equals("PESOS COLOMBIANOS"))
                    divisaFin = " PESO COLOMBIANO ";
                else if(divisa.Equals("EUROS"))
                    divisaFin = " EURO ";
            }
            else {

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
            else {

                res = toText(Convert.ToDouble(entero)) + " " + divisaFin;
            }
            
            return res;
        }

        private string toText(double value){

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

   

        private void OnPropertyChanged2(object sender, PropertyChangedEventArgs ev)
        {
            if (ev.PropertyName.Equals("AlmacenProcedencia"))
                this.Tecnicos = this.GetTecnicosByAlmacen(this.MovimientoModel.AlmacenProcedencia);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
