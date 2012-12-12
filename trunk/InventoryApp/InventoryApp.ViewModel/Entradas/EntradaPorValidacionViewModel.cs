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

namespace InventoryApp.ViewModel.Entradas
{
    public class EntradaPorValidacionViewModel : IPageViewModel
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
        private InventoryApp.ViewModel.GridMovimientos.MovimientoGridEntradasViewModel _movimentoGridEntradas;
        private RelayCommand _addItemCommand;
        private RelayCommand _imprimirCommand;
        private RelayCommand _deleteItemCommand;


        private CatalogEmpresaModel _catalogEmpresaModel;

        public EntradaPorValidacionViewModel()
        {
            try
            {
                IDataMapper dataMapper = new SolicitanteDataMapper();
                IDataMapper dataMapper2 = new AlmacenDataMapper();
                IDataMapper dataMapper3 = new ProveedorDataMapper();
                IDataMapper dataMapper4 = new ClienteDataMapper();
                IDataMapper datamapper5 = new EmpresaDataMapper();

                this._catalogEmpresaModel = new CatalogEmpresaModel(datamapper5);

                this._catalogSolicitanteModel = new CatalogSolicitanteModel(dataMapper);
                this._movimientoModel = new MovimientoModel(new MovimientoDataMapper());
                TIPO_MOVIMIENTO mov = new TIPO_MOVIMIENTO();
                mov.UNID_TIPO_MOVIMIENTO = 1;
                this._movimientoModel.TipoMovimiento = mov;
                this._itemModel = new CatalogItemModel(new ItemDataMapper());
                this._catalogAlmacenModel = new CatalogAlmacenModel(dataMapper2);
                this._catalogAlmacenProcedenciaModel = new CatalogAlmacenModel(dataMapper2);
                this._catalogProveedorProcedenciaModel = new CatalogProveedorModel(dataMapper3);
                this._catalogClienteProcedenciaModel = new CatalogClienteModel(dataMapper4);
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
        public EntradaPorValidacionViewModel( InventoryApp.ViewModel.GridMovimientos.MovimientoGridEntradasViewModel grid)
        {
            try
            {
                IDataMapper dataMapper = new SolicitanteDataMapper();
                IDataMapper dataMapper2 = new AlmacenDataMapper();
                IDataMapper dataMapper3 = new ProveedorDataMapper();
                IDataMapper dataMapper4 = new ClienteDataMapper();
                IDataMapper datamapper5 = new EmpresaDataMapper();

                this._catalogEmpresaModel = new CatalogEmpresaModel(datamapper5);

                this._movimentoGridEntradas = grid;

                this._catalogSolicitanteModel = new CatalogSolicitanteModel(dataMapper);
                this._movimientoModel = new MovimientoModel(new MovimientoDataMapper());
                TIPO_MOVIMIENTO mov = new TIPO_MOVIMIENTO();
                mov.UNID_TIPO_MOVIMIENTO = 1;
                this._movimientoModel.TipoMovimiento = mov;
                this._itemModel = new CatalogItemModel(new ItemDataMapper());
                this._catalogAlmacenModel = new CatalogAlmacenModel(dataMapper2);
                this._catalogAlmacenProcedenciaModel = new CatalogAlmacenModel(dataMapper2);
                this._catalogProveedorProcedenciaModel = new CatalogProveedorModel(dataMapper3);
                this._catalogClienteProcedenciaModel = new CatalogClienteModel(dataMapper4);
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

            if (this.ItemModel.ItemModel.Count() != 0 && !String.IsNullOrEmpty(this.MovimientoModel.Tt) && !String.IsNullOrEmpty(this.MovimientoModel.Recibe) && seleccion == 1)
                _canInsertArticulo = true;

            return _canInsertArticulo;
        }

        public void AttempArticulo()
        {
            this._movimientoModel.saveArticulo();
            _movimentoGridEntradas.updateItems();

            foreach (ItemModel item in this._itemModel.ItemModel)
            {
                this._movimientoDetalleModel = new MovimientoDetalleModel(new MovimientoDetalleDataMapper(), this._movimientoModel.UnidMovimiento, item.UnidItem);
                this._movimientoDetalleModel.saveArticulo();
                this._ultimoMovimientoModel = new UltimoMovimientoModel(new UltimoMovimientoDataMapper(), item.UnidItem, this._movimientoModel.UnidAlmacenDestino, null, null, this._movimientoDetalleModel.UnidMovimientoDetalle);
                this._ultimoMovimientoModel.saveArticulo();
            }            
        }

        public bool CanAttempImprimir()
        {
            //bool _canImprimir = false;

            //int seleccion = 0;
            //if (this.MovimientoModel.AlmacenProcedencia != null)
            //    seleccion++;
            //if (this.MovimientoModel.ClienteProcedencia != null)
            //    seleccion++;
            //if (this.MovimientoModel.ProveedorProcedencia != null)
            //    seleccion++;

            //if (this.ItemModel.ItemModel.Count() != 0 && !String.IsNullOrEmpty(this.MovimientoModel.Tt) && !String.IsNullOrEmpty(this.MovimientoModel.Recibe) && seleccion == 1)
            //    _canImprimir = true;

            //return _canImprimir;

            return true;
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

                Workbook excelPrint = excel.Workbooks.Open(@"C:\temp\elarainventarios\EntradaValidacion.xlsx", Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                Worksheet excelSheetPrint = (Worksheet)excelPrint.Worksheets[1];

                //Folio
                excel.Cells[8, 6] = _movimientoModel.UnidMovimiento.ToString();
                //Fecha
                excel.Cells[8, 23] = _movimientoModel.FechaMovimiento;

                //Solicitante y su área
                excel.Cells[11, 12] = _movimientoModel.Solicitante.SOLICITANTE_NAME;
                excel.Cells[13, 12] = _movimientoModel.Solicitante.Departamento.DEPARTAMENTO_NAME;
                //Recibe
                excel.Cells[15, 12] = _movimientoModel.Recibe.ToString();
                //Procedencia
                string p = "";

                if (_movimientoModel.ProveedorProcedencia != null)
                    p = _movimientoModel.ProveedorProcedencia.PROVEEDOR_NAME;
                else if (_movimientoModel.AlmacenProcedencia != null)
                    p = _movimientoModel.AlmacenProcedencia.ALMACEN_NAME;
                else
                    p = _movimientoModel.ClienteProcedencia.CLIENTE1;

                excel.Cells[17, 12] = p.ToString();

                //Destino
                excel.Cells[19, 12] = _movimientoModel.AlmacenDestino.ALMACEN_NAME;
                //TT
                excel.Cells[21, 12] = _movimientoModel.Tt;
                //Empresa
                excel.Cells[23, 12] = _movimientoModel.Empresa.EMPRESA_NAME;

                int X = 31;
                Microsoft.Office.Interop.Excel.Borders borders;

                //for (int i = 0; i < ItemModel.ItemModel.Count; i++) {
                for (int i = 0; i < 5; i++)
                {
                    //No.
                    excel.Range[excel.Cells[X, 2], excel.Cells[X, 3]].Merge();
                    excel.Range[excel.Cells[X, 2], excel.Cells[X, 3]].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;                    
                    borders = excel.Range[excel.Cells[X, 2], excel.Cells[X, 3]].Borders;
                    borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    //CANT.
                    excel.Range[excel.Cells[X, 4], excel.Cells[X, 6]].Merge();
                    excel.Range[excel.Cells[X, 4], excel.Cells[X, 6]].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    borders = excel.Range[excel.Cells[X, 4], excel.Cells[X, 6]].Borders;
                    borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    //DESCRIPCIÓN
                    excel.Range[excel.Cells[X, 7], excel.Cells[X, 20]].Merge();
                    excel.Range[excel.Cells[X, 7], excel.Cells[X, 20]].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    borders = excel.Range[excel.Cells[X, 7], excel.Cells[X, 20]].Borders;
                    borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    //N° DE SERIE
                    excel.Range[excel.Cells[X, 21], excel.Cells[X, 24]].Merge();
                    excel.Range[excel.Cells[X, 21], excel.Cells[X, 24]].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    borders = excel.Range[excel.Cells[X, 21], excel.Cells[X, 24]].Borders;
                    borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    //MODELO
                    excel.Range[excel.Cells[X, 25], excel.Cells[X, 27]].Merge();
                    excel.Range[excel.Cells[X, 25], excel.Cells[X, 27]].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    borders = excel.Range[excel.Cells[X, 25], excel.Cells[X, 27]].Borders;
                    borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    //OBSERVACIONES
                    excel.Range[excel.Cells[X, 28], excel.Cells[X, 34]].Merge();
                    excel.Range[excel.Cells[X, 28], excel.Cells[X, 34]].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    borders = excel.Range[excel.Cells[X, 28], excel.Cells[X, 34]].Borders;
                    borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    
                    X++;
                }                

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
    }
}
