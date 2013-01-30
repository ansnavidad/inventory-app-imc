using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using InventoryApp.DAL;
using InventoryApp.DAL.POCOS;
using System.Windows.Input;
using Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.ComponentModel;

namespace InventoryApp.ViewModel.Entradas
{
    public class EntradaPrestamoViewModel : IPageViewModel, INotifyPropertyChanged
    {
        private MovimientoModel _movimientoModel;
        private MovimientoDetalleModel _movimientoDetalleModel;
        private UltimoMovimientoModel _ultimoMovimientoModel;
        private CatalogSolicitanteModel _catalogSolicitanteModel;
        private CatalogAlmacenModel _catalogAlmacenModel;
        private CatalogAlmacenModel _catalogAlmacenProcedenciaModel;
        private CatalogProveedorModel _catalogProveedorProcedenciaModel;
        private CatalogClienteModel _catalogClienteProcedenciaModel;
        private CatalogItemModel _itemModel;
        private CatalogTransporteModel _catalogTransporteModel;
        private RelayCommand _addItemCommand;
        private RelayCommand _deleteItemCommand;
        private RelayCommand _imprimirCommand;
        private InventoryApp.ViewModel.GridMovimientos.MovimientoGridEntradasPrestamoViewModel _movimientoEntradas;

        private CatalogEmpresaModel _catalogEmpresaModel;

        public EntradaPrestamoViewModel()
        {            
            try
            {
                IDataMapper dataMapper = new SolicitanteDataMapper();
                IDataMapper dataMapper2 = new AlmacenDataMapper();
                IDataMapper dataMapper3 = new ProveedorDataMapper();
                IDataMapper dataMapper4 = new ClienteDataMapper();
                IDataMapper dataMapper5 = new TransporteDataMapper();


                this._catalogSolicitanteModel = new CatalogSolicitanteModel(dataMapper);
                this._movimientoModel = new MovimientoModel(new MovimientoDataMapper());
                TIPO_MOVIMIENTO mov = new TIPO_MOVIMIENTO();
                mov.UNID_TIPO_MOVIMIENTO = 2;
                this._movimientoModel.TipoMovimiento = mov;
                this._itemModel = new CatalogItemModel(new ItemDataMapper());
                this._catalogAlmacenModel = new CatalogAlmacenModel(dataMapper2);
                this._catalogAlmacenProcedenciaModel = new CatalogAlmacenModel(dataMapper2);
                this._catalogProveedorProcedenciaModel = new CatalogProveedorModel(dataMapper3);
                this._catalogClienteProcedenciaModel = new CatalogClienteModel(dataMapper4);
                this._catalogTransporteModel = new CatalogTransporteModel(dataMapper5);

                //Asignaciones especiales para los combos 
                this._movimientoModel.Transporte = _catalogTransporteModel.Transporte[0];                
                this._movimientoModel.AlmacenProcedencia = _catalogAlmacenProcedenciaModel.Almacen[0];
                this._movimientoModel.ClienteProcedencia = _catalogClienteProcedenciaModel.Cliente[0];
                this._movimientoModel.ProveedorProcedencia = _catalogProveedorProcedenciaModel.Proveedor[0];
                this._movimientoModel.AlmacenDestino = _catalogAlmacenModel.Almacen[0];
                this._movimientoModel.Tecnico = _movimientoModel.Tecnicos[0];
                this._movimientoModel.Empresa = _catalogEmpresaModel.Empresa[0];
                this._movimientoModel.Solicitante = _catalogSolicitanteModel.Solicitante[0];
                this.IsEnabledCheck1 = true;
                this.IsEnabledCheck2 = true;
                this.IsEnabledCombo1 = true;
                this.IsEnabledCombo2 = false;
                this.IsCheck1 = true;
                this.IsCheck2 = false;
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

        public EntradaPrestamoViewModel(InventoryApp.ViewModel.GridMovimientos.MovimientoGridEntradasPrestamoViewModel entradas)
        {
            try
            {
                IDataMapper dataMapper = new SolicitanteDataMapper();
                IDataMapper dataMapper2 = new AlmacenDataMapper();
                IDataMapper dataMapper3 = new ProveedorDataMapper();
                IDataMapper dataMapper4 = new ClienteDataMapper();
                IDataMapper dataMapper5 = new TransporteDataMapper();
                IDataMapper datamapper5 = new EmpresaDataMapper();

                this._catalogEmpresaModel = new CatalogEmpresaModel(datamapper5);
                this._movimientoEntradas = entradas;
                this._catalogSolicitanteModel = new CatalogSolicitanteModel(dataMapper);
                this._movimientoModel = new MovimientoModel(new MovimientoDataMapper());
                TIPO_MOVIMIENTO mov = new TIPO_MOVIMIENTO();
                mov.UNID_TIPO_MOVIMIENTO = 2;
                this._movimientoModel.TipoMovimiento = mov;
                this._itemModel = new CatalogItemModel(new ItemDataMapper());
                this._catalogAlmacenModel = new CatalogAlmacenModel(dataMapper2);
                this._catalogAlmacenProcedenciaModel = new CatalogAlmacenModel(dataMapper2);
                this._catalogProveedorProcedenciaModel = new CatalogProveedorModel(dataMapper3);
                this._catalogClienteProcedenciaModel = new CatalogClienteModel(dataMapper4);
                this._catalogTransporteModel = new CatalogTransporteModel(dataMapper5);

                //Asignaciones especiales para los combos 
                this._movimientoModel.Transporte = _catalogTransporteModel.Transporte[0];                
                this._movimientoModel.AlmacenProcedencia = _catalogAlmacenProcedenciaModel.Almacen[0];
                this._movimientoModel.ClienteProcedencia = _catalogClienteProcedenciaModel.Cliente[0];
                this._movimientoModel.ProveedorProcedencia = _catalogProveedorProcedenciaModel.Proveedor[0];
                this._movimientoModel.AlmacenDestino = _catalogAlmacenModel.Almacen[0];
                this._movimientoModel.Tecnico = _movimientoModel.Tecnicos[0];
                this._movimientoModel.Empresa = _catalogEmpresaModel.Empresa[0];
                this._movimientoModel.Solicitante = _catalogSolicitanteModel.Solicitante[0];
                this.IsEnabledCheck1 = true;
                this.IsEnabledCheck2 = true;
                this.IsEnabledCombo1 = true;
                this.IsEnabledCombo2 = false;
                this.IsCheck1 = true;
                this.IsCheck2 = false;
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

        public bool IsEnabledCheck1
        {
            get { return _IsEnabledCheck1; }
            set
            {
                if (_IsEnabledCheck1 != value)
                {
                    _IsEnabledCheck1 = value;
                    OnPropertyChanged(IsEnabledCheck1PropertyName);
                }
            }
        }
        private bool _IsEnabledCheck1;
        public const string IsEnabledCheck1PropertyName = "IsEnabledCheck1";

        public bool IsEnabledCheck2
        {
            get { return _IsEnabledCheck2; }
            set
            {
                if (_IsEnabledCheck2 != value)
                {
                    _IsEnabledCheck2 = value;
                    OnPropertyChanged(IsEnabledCheck2PropertyName);
                }
            }
        }
        private bool _IsEnabledCheck2;
        public const string IsEnabledCheck2PropertyName = "IsEnabledCheck2";

        public bool IsEnabledCombo1
        {
            get { return _IsEnabledCombo1; }
            set
            {
                if (_IsEnabledCombo1 != value)
                {
                    _IsEnabledCombo1 = value;
                    OnPropertyChanged(IsEnabledCombo1PropertyName);
                }
            }
        }
        private bool _IsEnabledCombo1;
        public const string IsEnabledCombo1PropertyName = "IsEnabledCombo1";

        public bool IsEnabledCombo2
        {
            get { return _IsEnabledCombo2; }
            set
            {
                if (_IsEnabledCombo2 != value)
                {
                    _IsEnabledCombo2 = value;
                    OnPropertyChanged(IsEnabledCombo2PropertyName);
                }
            }
        }
        private bool _IsEnabledCombo2;
        public const string IsEnabledCombo2PropertyName = "IsEnabledCombo2";
        
        public bool IsCheck2
        {
            get { return _IsCheck2; }
            set
            {
                if (_IsCheck2 != value)
                {
                    _IsCheck2 = value;
                    OnPropertyChanged(IsCheck2PropertyName);
                }
            }
        }
        private bool _IsCheck2;
        public const string IsCheck2PropertyName = "IsCheck2";

        public bool IsCheck1
        {
            get { return _IsCheck1; }
            set
            {
                if (_IsCheck1 != value)
                {
                    _IsCheck1 = value;
                    OnPropertyChanged(IsCheck1PropertyName);
                }
            }
        }
        private bool _IsCheck1;
        public const string IsCheck1PropertyName = "IsCheck1";
        
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        public CatalogProveedorModel CatalogProveedorProcedenciaModel
        {
            get
            {
                return _catalogProveedorProcedenciaModel;

            }
            set
            {
                _catalogProveedorProcedenciaModel = value;
            }
        }

        public CatalogAlmacenModel CatalogAlmacenModel
        {
            get
            {
                return _catalogAlmacenModel;

            }
            set
            {
                _catalogAlmacenModel = value;
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

        public CatalogClienteModel CatalogClienteProcedenciaModel
        {
            get
            {
                return _catalogClienteProcedenciaModel;

            }
            set
            {
                _catalogClienteProcedenciaModel = value;
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

        public void loadItems()
        {
            this._catalogSolicitanteModel.loadSolicitante();
            this._catalogAlmacenModel.loadItems();
            this._catalogAlmacenProcedenciaModel.loadItems();
            this._catalogProveedorProcedenciaModel.loadItems();
            this._catalogClienteProcedenciaModel.loadCliente();
        }

        public CatalogItemViewModel CreateCatalogItemViewModel()
        {
            return new CatalogItemViewModel(this); 
        }

        public bool CanAttempArticulo()
        {
            bool _canInsertArticulo = false;

            int seleccion = 0;
            if (this.MovimientoModel.AlmacenProcedencia != null)
                seleccion++;
            if (this.MovimientoModel.ClienteProcedencia != null)
                seleccion++;
            if (this.MovimientoModel.ProveedorProcedencia != null)
                seleccion++;

            if (this.ItemModel.ItemModel.Count() != 0 && !String.IsNullOrEmpty(this.MovimientoModel.Contacto) && !String.IsNullOrEmpty(this.MovimientoModel.Tt) && seleccion == 1)
                _canInsertArticulo = true;

            if (this._itemModel.ItemModel.Count > 0)
            {
                this.IsEnabledCheck1 = false;
                this.IsEnabledCheck2 = false;
                this.IsEnabledCombo1 = false;
                this.IsEnabledCombo2 = false;
            }
            else
            {
                this.IsEnabledCheck1 = true;
                this.IsEnabledCheck2 = true;

                if (this.IsCheck1)
                {
                    this.IsEnabledCombo1 = true;
                    this.IsEnabledCombo2 = false;
                }
                else {

                    this.IsEnabledCombo1 = false;
                    this.IsEnabledCombo2 = true;
                }
            }

            return _canInsertArticulo;
        }

        public void AttempArticulo()
        {
            this._movimientoModel.saveArticulo();
            this._movimientoEntradas.updateItems();

            foreach (ItemModel item in this._itemModel.ItemModel)
            {
                this._movimientoDetalleModel = new MovimientoDetalleModel(new MovimientoDetalleDataMapper(), this._movimientoModel.UnidMovimiento, item.UnidItem, item.CantidadMovimiento, item.UNID_ITEM_STATUS);
                this._movimientoDetalleModel.saveArticulo();
                this._ultimoMovimientoModel = new UltimoMovimientoModel(new UltimoMovimientoDataMapper(), item.UnidItem, this._movimientoModel.UnidAlmacenDestino, null, null, this._movimientoDetalleModel.UnidMovimientoDetalle, item.CantidadMovimiento, item.UNID_ITEM_STATUS);
                this._ultimoMovimientoModel.updateArticulo(this.MovimientoModel.ClienteProcedencia);
                this._ultimoMovimientoModel.updateArticulo(this.MovimientoModel.ProveedorProcedencia);
                this._ultimoMovimientoModel.saveArticulo();
            }

            this._movimientoEntradas.updateItems();
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

        public bool CanAttempImprimir()
        {
            bool _canImprimir = false;

            int seleccion = 0;
            if (this.MovimientoModel.AlmacenProcedencia != null)
                seleccion++;
            if (this.MovimientoModel.ClienteProcedencia != null)
                seleccion++;
            if (this.MovimientoModel.ProveedorProcedencia != null)
                seleccion++;

            if (this.ItemModel.ItemModel.Count() != 0 && !String.IsNullOrEmpty(this.MovimientoModel.Contacto) && !String.IsNullOrEmpty(this.MovimientoModel.Tt) && seleccion == 1)
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

                Workbook excelPrint = excel.Workbooks.Open(@"C:\Programs\ElaraInventario\Resources\EntradaPrestamo.xlsx", Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                Worksheet excelSheetPrint = (Worksheet)excelPrint.Worksheets[1];

                //Folio
                excel.Cells[8, 6] = _movimientoModel.UnidMovimiento.ToString();
                //Fecha
                excel.Cells[8, 23] = _movimientoModel.FechaMovimiento;

                //Empresa
                excel.Cells[11, 12] = _movimientoModel.Empresa.EMPRESA_NAME;
                //Solicitante y su área
                excel.Cells[13, 12] = _movimientoModel.Solicitante.SOLICITANTE_NAME;
                excel.Cells[15, 12] = _movimientoModel.Solicitante.Departamento.DEPARTAMENTO_NAME;
                try
                {
                    //Procedencia
                    string p = "";

                    if (_movimientoModel.ProveedorProcedencia != null)
                        p = "Proveedor : " + _movimientoModel.ProveedorProcedencia.PROVEEDOR_NAME;
                    else if (_movimientoModel.AlmacenProcedencia != null)
                        p = "Almacén: " + _movimientoModel.AlmacenProcedencia.ALMACEN_NAME;
                    else
                        p = "Cliente: " + _movimientoModel.ClienteProcedencia.CLIENTE1;

                    excel.Cells[17, 12] = p.ToString();

                    //Destino
                    excel.Cells[19, 12] = "Almacén: " + _movimientoModel.AlmacenDestino.ALMACEN_NAME;
                    //Recibe
                    excel.Cells[21, 12] = _movimientoModel.Tecnico.TECNICO_NAME;
                }
                catch (Exception Ex)
                {

                }
                //TT
                excel.Cells[23, 12] = _movimientoModel.Tt;
                //Transporte
                excel.Cells[25, 12] = _movimientoModel.Transporte.TRANSPORTE_NAME;
                //Contacto
                excel.Cells[27, 12] = _movimientoModel.Contacto;
                //Guia
                excel.Cells[29, 12] = _movimientoModel.Guia;

                int X = 35;
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
            }
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
    }
}
