﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using InventoryApp.Model;
using InventoryApp.ViewModel.GridMovimientos;
using InventoryApp.DAL;
using InventoryApp.DAL.POCOS;
using System.Windows.Input;
using Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Collections.ObjectModel;

namespace InventoryApp.ViewModel.Salidas
{
    public class SalidaBajaViewModel : IPageViewModel, INotifyPropertyChanged
    {

        #region campos
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
        private CatalogTecnicoModel _catalogTecnicoModel;
        private MovimientoGridSalidaBajaViewModel _movimientoSalida;
        private RelayCommand _addItemCommand;
        private RelayCommand _deleteItemCommand;
        private RelayCommand _imprimirCommand;
        public USUARIO ActualUser;
        #endregion

        #region propiedades
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

        public MovimientoDetalleModel MovimientoDetalleModel
        {
            get
            {
                return _movimientoDetalleModel;

            }
            set
            {
                _movimientoDetalleModel = value;
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
        #endregion

        #region constructores

        public SalidaBajaViewModel(MovimientoGridSalidaBajaViewModel salida)
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
                IDataMapper dataMapper8 = new TecnicoDataMapper();
                IDataMapper datamapper11 = new EmpresaDataMapper();

                this._catalogEmpresaModel = new CatalogEmpresaModel(datamapper11);

                this._catalogSolicitanteModel = new CatalogSolicitanteModel(dataMapper);
                this._movimientoModel = new MovimientoSalidasModel(new MovimientoDataMapper(), salida.ActualUser);
                TIPO_MOVIMIENTO mov = new TIPO_MOVIMIENTO();
                mov.UNID_TIPO_MOVIMIENTO = 18;
                this._movimientoModel.TipoMovimiento = mov;
                this._movimientoSalida = salida;
                this._movimientoModel.PropertyChanged += OnPropertyChanged2;
                this._movimientoDetalleModel = new MovimientoDetalleModel(new MovimientoDetalleDataMapper());
                this._itemModel = new CatalogItemModel(new ItemDataMapper());
                this._catalogAlmacenDestinoModel = new CatalogAlmacenModel(dataMapper2);
                this._catalogAlmacenProcedenciaModel = new CatalogAlmacenModel(dataMapper2);
                this._catalogProveedorDestinoModel = new CatalogProveedorModel(dataMapper3);
                this._catalogClienteDestinoModel = new CatalogClienteModel(dataMapper4);
                this._catalogServicioModel = new CatalogServicioModel(dataMapper5);
                this._catalogTipoPedimentoModel = new CatalogTipoPedimentoModel(dataMapper6);
                this._catalogTransporteModel = new CatalogTransporteModel(dataMapper7);
                this._catalogClienteModel = new CatalogClienteModel(dataMapper4);
                this._catalogTecnicoModel = new CatalogTecnicoModel(dataMapper8);

                //Asignaciones especiales para los combos 
                this._movimientoModel.Empresa = _catalogEmpresaModel.Empresa[0];
                this._movimientoModel.Solicitante = _catalogSolicitanteModel.Solicitante[0];
                this._movimientoModel.AlmacenProcedencia = _catalogAlmacenProcedenciaModel.Almacen[0];
                this._movimientoModel.Tecnico = _movimientoModel.Tecnicos[0];
                this._movimientoModel.AlmacenDestino = _catalogAlmacenDestinoModel.Almacen[0];
                this._movimientoModel.ClienteDestino = _catalogClienteDestinoModel.Cliente[0];
                //this._movimientoModel.ProveedorDestino = _catalogProveedorDestinoModel.Proveedor[0];
                this._IsEnabled = true;
                this.ActualUser = salida.ActualUser;                
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
        #endregion

        #region metodos
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
            this._catalogTecnicoModel.loadItems();
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

            if (this.ItemModel.ItemModel.Count() != 0 && !String.IsNullOrEmpty(this.MovimientoDetalleModel.Observaciones)  && seleccion == 1)
                _canInsertArticulo = true;

            if (this._itemModel.ItemModel.Count > 0)
                this.IsEnabled = false;
            else
                this.IsEnabled = true;

            return _canInsertArticulo;
        }

        public void AttempArticulo()
        {
            this._movimientoModel.saveArticuloBaja();
            this._movimientoSalida.updateItems();

            foreach (ItemModel item in this._itemModel.ItemModel)
            {
                this._movimientoDetalleModel = new MovimientoDetalleModel(new MovimientoDetalleDataMapper(), this._movimientoModel.UnidMovimiento, item.UnidItem, item.CantidadMovimiento, item.UNID_ITEM_STATUS,this.MovimientoDetalleModel.Observaciones);
                this._movimientoDetalleModel.saveArticuloBaja();
                this._ultimoMovimientoModel = new UltimoMovimientoModel(new UltimoMovimientoDataMapper(), item.UnidItem, this._movimientoModel.UnidAlmacenDestino, this._movimientoModel.UnidClienteDestino, this._movimientoModel.UnidProveedorDestino, this._movimientoDetalleModel.UnidMovimientoDetalle, item.CantidadMovimiento, item.UNID_ITEM_STATUS,"Salida baja");
                this._ultimoMovimientoModel.updateArticuloBaja(this.MovimientoModel.AlmacenProcedencia,"salida baja");
                this._ultimoMovimientoModel.saveArticuloBaja();
            }
            this._movimientoSalida.updateItems();
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

            if (this.ItemModel.ItemModel.Count() != 0 && !String.IsNullOrEmpty(this.MovimientoDetalleModel.Observaciones) && seleccion == 1)
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

                Workbook excelPrint = excel.Workbooks.Open(@"C:\Programs\ElaraInventario\Resources\SalidaBaja.xlsx", Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                Worksheet excelSheetPrint = (Worksheet)excelPrint.Worksheets[1];

                //Folio
                excel.Cells[8, 6] = _movimientoModel.UnidMovimiento.ToString();
                //Fecha
                excel.Cells[8, 23] = _movimientoModel.FechaMovimiento;

                //Solicitante y su área
                excel.Cells[13, 12] = _movimientoModel.Solicitante.SOLICITANTE_NAME;
                excel.Cells[15, 12] = _movimientoModel.Solicitante.Departamento.DEPARTAMENTO_NAME;
                try
                {
                    //Recibe
                    excel.Cells[19, 12] = _movimientoModel.Tecnico.TECNICO_NAME;
                    //Procedencia                
                    excel.Cells[17, 12] = "Almacén: " + _movimientoModel.AlmacenProcedencia.ALMACEN_NAME;
                }
                catch (Exception Ex)
                {

                }
                //Empresa
                excel.Cells[11, 12] = _movimientoModel.Empresa.EMPRESA_NAME;
                int X = 26;
                Microsoft.Office.Interop.Excel.Borders borders;

                for (int i = 0; i < ItemModel.ItemModel.Count; i++)
                {
                    
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
                try
                {
                    excel.Cells[X, 3] = "OBSERVACIONES:";
                    excel.Range[excel.Cells[X, 9], excel.Cells[X + 2, 33]].Merge();
                    excel.Cells[X, 9] = _movimientoDetalleModel.Observaciones;
                    borders = excel.Range[excel.Cells[X, 9], excel.Cells[X + 2, 33]].Borders;
                    borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                }
                catch (Exception ex)
                {
                    ; 
                   
                }
                

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

        private void OnPropertyChanged2(object sender, PropertyChangedEventArgs ev)
        {
            if (ev.PropertyName.Equals("AlmacenProcedencia"))
                this.Tecnicos = this.GetTecnicosByAlmacen(this.MovimientoModel.AlmacenProcedencia);
        }

        public void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
        #endregion

        #region collecion
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
        #endregion

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

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
