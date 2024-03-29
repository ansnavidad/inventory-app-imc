﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using InventoryApp.DAL;
using InventoryApp.DAL.POCOS;
using System.Windows.Input;
using Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Collections.ObjectModel;

namespace InventoryApp.ViewModel.Entradas
{
    public class EntradaPorValidacionViewModel : ViewModelBase,IPageViewModel
    {
        private MovimientoModel _movimientoModel;
        private MovimientoDetalleModel _movimientoDetalleModel;
        private UltimoMovimientoModel _ultimoMovimientoModel;
        private CatalogSolicitanteModel _catalogSolicitanteModel;
        private CatalogAlmacenModel _catalogAlmacenModel;
        private CatalogItemModel _itemModel;
        private InventoryApp.ViewModel.GridMovimientos.MovimientoGridEntradasViewModel _movimentoGridEntradas;
        private RelayCommand _addItemCommand;
        private RelayCommand _imprimirCommand;
        private RelayCommand _deleteItemCommand;
        private CatalogEmpresaModel _catalogEmpresaModel;
        private CatalogInfraestructuraModel _catalogInfraestructuraModel;

        public EntradaPorValidacionViewModel( InventoryApp.ViewModel.GridMovimientos.MovimientoGridEntradasViewModel grid)
        {
            try
            {
                IDataMapper dataMapper = new SolicitanteDataMapper();
                IDataMapper dataMapper2 = new AlmacenDataMapper();
                IDataMapper dataMapper3 = new ProveedorDataMapper();
                IDataMapper dataMapper4 = new ClienteDataMapper();
                IDataMapper datamapper5 = new EmpresaDataMapper();
                IDataMapper datamapper9 = new InfraestructuraDataMapper();
                IDataMapper dataMapper10 = new ItemStatusDataMapper();

                this._catalogInfraestructuraModel = new CatalogInfraestructuraModel(datamapper9);
                this._catalogEmpresaModel = new CatalogEmpresaModel(datamapper5);

                this._movimentoGridEntradas = grid;

                this._catalogSolicitanteModel = new CatalogSolicitanteModel(dataMapper);
                this._movimientoModel = new MovimientoModel(new MovimientoDataMapper());
                TIPO_MOVIMIENTO mov = new TIPO_MOVIMIENTO();
                mov.UNID_TIPO_MOVIMIENTO = 1;
                this._movimientoModel.TipoMovimiento = mov;
                this._itemModel = new CatalogItemModel(new ItemDataMapper());
                this._catalogAlmacenModel = new CatalogAlmacenModel(dataMapper2);

                //Asignaciones especiales para los combos 
                this._movimientoModel.Empresa = _catalogEmpresaModel.Empresa[0];
                this._movimientoModel.Solicitante = _catalogSolicitanteModel.Solicitante[0];
                this._movimientoModel.AlmacenDestino = _catalogAlmacenModel.Almacen[0];
                this._movimientoModel.Tecnico = _movimientoModel.Tecnicos[0];
                this._movimientoModel.Infraestructura = _catalogInfraestructuraModel.Infraestructuras[0];
                this._movimientoModel.ActualUser = grid.ActualUser;
                this._IsEnabled = true;
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

        public CatalogInfraestructuraModel CatalogInfraestructuraModel
        {
            get
            {
                return _catalogInfraestructuraModel;
            }
            set
            {
                _catalogInfraestructuraModel = value;
            }
        }

        public void loadItems()
        {
            this._catalogSolicitanteModel.loadSolicitante();
            this._catalogAlmacenModel.loadItems();
        }

        public CatalogItemViewModel CreateCatalogItemViewModel()
        {
            return new CatalogItemViewModel(this); 
        }

        public bool CanAttempArticulo()
        {
            bool _canImprimir = false;

            if (this.ItemModel.ItemModel.Count() != 0 && !String.IsNullOrEmpty(this.MovimientoModel.Tt) )
                _canImprimir = true;

            return _canImprimir;
        }

        public void AttempArticulo()
        {
            this._movimientoModel.saveArticulo();
            this._movimentoGridEntradas.updateItems();

            foreach (ItemModel item in this._itemModel.ItemModel)
            {

                this._movimientoDetalleModel = new MovimientoDetalleModel(new MovimientoDetalleDataMapper(), this._movimientoModel.UnidMovimiento, item.UnidItem, item.CantidadMovimiento, item.ItemStatuss.UnidItemStatus);
                this._movimientoDetalleModel.saveArticulo();
                this._ultimoMovimientoModel = new UltimoMovimientoModel(new UltimoMovimientoDataMapper(), item.UnidItem, this._movimientoModel.UnidAlmacenDestino, null, null, this._movimientoDetalleModel.UnidMovimientoDetalle, item.CantidadMovimiento, item.ItemStatuss.UnidItemStatus);
                this._ultimoMovimientoModel.updateArticulo(this.MovimientoModel.Infraestructura);
                this._ultimoMovimientoModel.saveArticulo();
            }

            this._movimentoGridEntradas.updateItems();
        }

        public bool CanAttempImprimir()
        {
            bool _canImprimir = false;
            
            if (this.ItemModel.ItemModel.Count() != 0 && !String.IsNullOrEmpty(this.MovimientoModel.Tt))
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
                excel.Visible = false;

                Workbook excelPrint = excel.Workbooks.Open(@"C:\Programs\ElaraInventario\Resources\EntradaValidacion.xlsx", Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
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
                    excel.Cells[17, 12] = "Infraestructura: " + _movimientoModel.Infraestructura.INFRAESTRUCTURA_NAME;
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
                
                int X = 31;
                Microsoft.Office.Interop.Excel.Borders borders;

                for (int i = 0; i < ItemModel.ItemModel.Count; i++) {
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
                excel.Range[excel.Cells[X, 9], excel.Cells[X+2, 33]].Merge();
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
                excel.Range[excel.Cells[X, 2], excel.Cells[X+2, 17]].Merge();
                borders = excel.Range[excel.Cells[X, 2], excel.Cells[X + 2, 17]].Borders;
                borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                excel.Range[excel.Cells[X, 18], excel.Cells[X+2, 34]].Merge();
                borders = excel.Range[excel.Cells[X, 18], excel.Cells[X + 2, 34]].Borders; 
                borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;

                

                excelSheetPrint.SaveAs(filename, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                excel.Visible = true;
            }
        }

        public bool IsEnabled
        {
            get { return _IsEnabled; }
            set
            {
                if (_IsEnabled != value)
                {
                    _IsEnabled = value;
                    OnPropertyChanged(IsEnabledPropertyName);
                }
            }
        }
        private bool _IsEnabled;
        public const string IsEnabledPropertyName = "IsEnabled";

        public bool CanAttempDeleteArticulo()
        {
            bool _canDeleteArticulo = true;

            if (this._itemModel.ItemModel.Count > 0)
                this.IsEnabled = false;
            else
                this.IsEnabled = true;

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

        public ObservableCollection<ItemStatusModel> ItemStatuss
        {
            get
            {
                if (_ItemStatuss == null)
                {
                    _ItemStatuss = this.GetStatuss();
                }

                return _ItemStatuss;
            }
            set
            {
                if (_ItemStatuss != value)
                {
                    _ItemStatuss = value;
                    OnPropertyChanged(ItemStatusPropertyName);
                }
            }
        }
        private ObservableCollection<ItemStatusModel> _ItemStatuss;
        public const string ItemStatusPropertyName = "ItemStatus";

        private ObservableCollection<ItemStatusModel> GetStatuss()
        {
            ObservableCollection<ItemStatusModel> ItemStatusAux = new ObservableCollection<ItemStatusModel>();

            try
            {
                ItemStatusDataMapper dataMapper = new ItemStatusDataMapper();

                List<ITEM_STATUS> ii = new List<ITEM_STATUS>();
                ii = (List<ITEM_STATUS>)dataMapper.getElements();

                foreach (ITEM_STATUS i in ii)
                {

                    ItemStatusModel ism = new ItemStatusModel(new ItemStatusDataMapper());
                    ism.ItemStatusName = i.ITEM_STATUS_NAME;
                    ism.UnidItemStatus = i.UNID_ITEM_STATUS;
                    ItemStatusAux.Add(ism);
                }
            }
            catch (Exception ex)
            {
            }

            return ItemStatusAux;
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
