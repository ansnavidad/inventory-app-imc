using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using InventoryApp.Model.Historial;
using System.Collections.ObjectModel;
using InventoryApp.DAL.Historial;
using InventoryApp.DAL.POCOS;
using InventoryApp.Model.Seguridad;
using InventoryApp.Model.CatalogInventario;
using System.Windows.Input;
using InventoryApp.DAL;
using InventoryApp.Model.CatalogInventario;
using Microsoft.Office.Interop.Excel;
using System.Reflection;
using InventoryApp.DAL.CatalogInventario;

namespace InventoryApp.ViewModel.CatalogInventario
{
    public class ModifyInventarioViewModel: ViewModelBase
    {
        #region Relay Command

        public ICommand GuardarFinalizarCommand
        {
            get
            {
                if (_GuardarFinalizarCommand == null)
                {
                    _GuardarFinalizarCommand = new RelayCommand(p => this.AttempGuardarFinalizar(), p => this.CanAttempGuardarFinalizar());
                }
                return _GuardarFinalizarCommand;
            }
        }
        private RelayCommand _GuardarFinalizarCommand;
        public bool CanAttempGuardarFinalizar()
        {
            return BotonFinalizar;
        }
        public void AttempGuardarFinalizar()
        {
            this.IsEnabled = false;            
            this.BotonImprimir = true;

            long UnidSegmento;
            UnidSegmento = UNID.getNewUNID();
            DateTime d = DateTime.Now;

            InventarioDataMapper iDM_ = new InventarioDataMapper();
            iDM_.delete(catalogInvViewModel.SelectedInventario.UnidSegmento, this.ActualUser);

            foreach (Descriptor ii in DescriptorCollection)
            {
                if (!String.IsNullOrEmpty(ii.DescriptorName))
                {
                    InventarioDataMapper iDM = new InventarioDataMapper();
                    for (int i = 0; i < ii.Cantidad; i++)
                        iDM.insert(new INVENTARIO() { DESCRIPTOR = ii.DescriptorName, UNID_SEGMENTO = UnidSegmento, UNID_ALMACEN = this.SelectedAlmacen.UNID_ALMACEN, FECHA = d, FINISHED = true }, this.ActualUser);
                }
            }
            this.catalogInvViewModel.InventarioCollection = this.catalogInvViewModel.GetInventarios();
        }

        public ICommand GuardarCommand
        {
            get
            {
                if (_GuardarCommand == null)
                {
                    _GuardarCommand = new RelayCommand(p => this.AttempGuardar(), p => this.CanAttempGuardar());
                }
                return _GuardarCommand;
            }
        }
        private RelayCommand _GuardarCommand;
        public bool CanAttempGuardar()
        {
            if (this.DescriptorCollection != null && this.DescriptorCollection.Count > 0)
            {
                if (!String.IsNullOrEmpty(this.DescriptorCollection[this.DescriptorCollection.Count - 1].DescriptorName))
                {
                    this.DescriptorCollection.Add(new Model.CatalogInventario.Descriptor() { DescriptorName = "", IsChecked = false, Cantidad = 1 });
                }
            }
            else if (this.DescriptorCollection.Count == 0)
                this.DescriptorCollection.Add(new Model.CatalogInventario.Descriptor() { DescriptorName = "", IsChecked = false, Cantidad = 1 });  

            return true;
        }
        public void AttempGuardar()
        {
            long UnidSegmento;
            UnidSegmento = UNID.getNewUNID();
            DateTime d = DateTime.Now;

            InventarioDataMapper iDM_ = new InventarioDataMapper();            
            iDM_.delete(catalogInvViewModel.SelectedInventario.UnidSegmento, this.ActualUser);
            
            foreach (Descriptor ii in DescriptorCollection)
            {
                if (!String.IsNullOrEmpty(ii.DescriptorName))
                {
                    InventarioDataMapper iDM = new InventarioDataMapper();
                    for (int i = 0; i < ii.Cantidad; i++ )
                        iDM.insert(new INVENTARIO() { DESCRIPTOR = ii.DescriptorName, UNID_SEGMENTO = UnidSegmento, UNID_ALMACEN = this.SelectedAlmacen.UNID_ALMACEN, FECHA = d, FINISHED = false }, this.ActualUser);
                }
            }
            this.catalogInvViewModel.InventarioCollection = this.catalogInvViewModel.GetInventarios();
        }

        public ICommand EliminarCommand
        {
            get
            {
                if (_EliminarCommand == null)
                {
                    _EliminarCommand = new RelayCommand(p => this.AttempEliminarCommand(), p => this.CanAttempEliminarCommand());
                }
                return _EliminarCommand;
            }
        }
        private RelayCommand _EliminarCommand;
        public bool CanAttempEliminarCommand()
        {
            return true;
        }
        public void AttempEliminarCommand()
        {
            for (int i = 0; i < DescriptorCollection.Count; )
            {

                if (DescriptorCollection[i].IsChecked)
                    this.DescriptorCollection.RemoveAt(i);
                else
                    i++;
            }
        }

        public ICommand ImprimirCommand
        {
            get
            {
                if (_ImprimirCommand == null)
                {
                    _ImprimirCommand = new RelayCommand(p => this.AttempImprimir(), p => this.CanAttempImprimir());
                }
                return _ImprimirCommand;
            }
        }
        private RelayCommand _ImprimirCommand;
        public bool CanAttempImprimir()
        {
            return BotonImprimir;
        }
        public void AttempImprimir()
        {
            List<ARTICULO> ArticulosFaltantes = new List<ARTICULO>();
            List<string> AuxArticulosFaltantes = new List<string>();
            //
            ItemDataMapper iDT = new ItemDataMapper();

            List<ITEM> Existentes = iDT.getElementsByAlmacen(this.SelectedAlmacen);
            List<ITEM> Capturados = new List<ITEM>();
            
            foreach (Descriptor ii in DescriptorCollection) {

                if (!String.IsNullOrEmpty(ii.DescriptorName))
                {
                    if (IsSKU)
                    {
                        Capturados.Add(new ITEM() { SKU = ii.DescriptorName, CANTIDAD = ii.Cantidad });
                    }
                    else
                    {
                        Capturados.Add(new ITEM() { NUMERO_SERIE = ii.DescriptorName, CANTIDAD = ii.Cantidad });
                    }
                }
            }


            if(IsSKU){
                
                for (int i = 0; i < Existentes.Count;i++) {

                    for (int j = 0; j < Capturados.Count;j++) {

                        if (Existentes[i].SKU.Equals(Capturados[j].SKU))
                        {
                            if (Existentes[i].CANTIDAD > 0 && Capturados[j].CANTIDAD > 0)
                            {
                                Existentes[i].CANTIDAD--;
                                Capturados[j].CANTIDAD--;

                                if (Existentes[i].CANTIDAD <= 0)
                                {
                                    Existentes.RemoveAt(i);                                    
                                }
                                if (Capturados[j].CANTIDAD <= 0)
                                {
                                    Capturados.RemoveAt(j);
                                    j--;
                                }
                            }
                            else {

                                AuxArticulosFaltantes.Add(Existentes[i].ARTICULO.ARTICULO1);
                                ArticulosFaltantes.Add(new ARTICULO() { ARTICULO1 = Existentes[i].ARTICULO.ARTICULO1, EQUIPO = new EQUIPO() { EQUIPO_NAME = "1" }, CATEGORIA = new CATEGORIA() { CATEGORIA_NAME = Existentes[i].SKU }, MODELO = new MODELO() { MODELO_NAME = Existentes[i].NUMERO_SERIE } });                   
                            }
                        }
                    }
                }
            } else {

                for (int i = 0; i < Existentes.Count; i++)
                {

                    for (int j = 0; j < Capturados.Count; j++)
                    {

                        if (Existentes[i].NUMERO_SERIE.Equals(Capturados[j].NUMERO_SERIE))
                        {
                            if (Existentes[i].CANTIDAD > 0 && Capturados[j].CANTIDAD > 0)
                            {
                                Existentes[i].CANTIDAD--;
                                Capturados[j].CANTIDAD--;

                                if (Existentes[i].CANTIDAD <= 0)
                                {
                                    Existentes.RemoveAt(i);                                    
                                }
                                if (Capturados[j].CANTIDAD <= 0)
                                {
                                    Capturados.RemoveAt(j);
                                    j--;
                                }
                            }
                            else
                            {
                                AuxArticulosFaltantes.Add(Existentes[i].ARTICULO.ARTICULO1);
                                ArticulosFaltantes.Add(new ARTICULO() { ARTICULO1 = Existentes[i].ARTICULO.ARTICULO1, EQUIPO = new EQUIPO() { EQUIPO_NAME = "1" } , CATEGORIA = new CATEGORIA() { CATEGORIA_NAME = Existentes[i].SKU }, MODELO = new MODELO() { MODELO_NAME = Existentes[i].NUMERO_SERIE } });                   
                            }
                        }
                    }
                }
            }            
            
            foreach (ITEM ii in Existentes) {

                while (ii.CANTIDAD > 0) {

                    AuxArticulosFaltantes.Add(ii.ARTICULO.ARTICULO1);
                    ArticulosFaltantes.Add(new ARTICULO() { ARTICULO1 = ii.ARTICULO.ARTICULO1, EQUIPO = new EQUIPO() { EQUIPO_NAME = "-1" }, CATEGORIA = new CATEGORIA() { CATEGORIA_NAME = ii.SKU }, MODELO = new MODELO() { MODELO_NAME = ii.NUMERO_SERIE } });                   

                    ii.CANTIDAD--;
                }    
            }

            if (IsSKU)
            {
                foreach (ITEM ii in Capturados)
                {
                    while (ii.CANTIDAD > 0)
                    {
                        if (ExisteRegistro(ii.SKU))
                        {
                            string aux = GetNameArticulo(ii.SKU);
                            AuxArticulosFaltantes.Add(aux);
                            ArticulosFaltantes.Add(new ARTICULO() { ARTICULO1 = aux, EQUIPO = new EQUIPO() { EQUIPO_NAME = "1" }, CATEGORIA = new CATEGORIA() { CATEGORIA_NAME = ii.SKU }, MODELO = new MODELO() { MODELO_NAME = ii.NUMERO_SERIE } });                   

                            ii.CANTIDAD--;
                        }
                        else {

                            AuxArticulosFaltantes.Add("El SKU '" + ii.SKU + "' no existe en el sistema.");
                            ArticulosFaltantes.Add(new ARTICULO() { ARTICULO1 = "El SKU '" + ii.SKU + "' no existe en el sistema.", EQUIPO = new EQUIPO() { EQUIPO_NAME = "1" } });

                            ii.CANTIDAD--;
                        } 
                    } 
                }
            }
            else {

                foreach (ITEM ii in Capturados)
                {
                    while (ii.CANTIDAD > 0)
                    {
                        if (ExisteRegistro(ii.NUMERO_SERIE))
                        {
                            string aux = GetNameArticulo(ii.NUMERO_SERIE);
                            AuxArticulosFaltantes.Add(aux);
                            ArticulosFaltantes.Add(new ARTICULO() { ARTICULO1 = aux, EQUIPO = new EQUIPO() { EQUIPO_NAME = "1" }, CATEGORIA = new CATEGORIA() { CATEGORIA_NAME = ii.SKU }, MODELO = new MODELO() { MODELO_NAME = ii.NUMERO_SERIE},  });                   

                            ii.CANTIDAD--;
                        }
                        else
                        {
                            AuxArticulosFaltantes.Add("El número dde serie '" + ii.NUMERO_SERIE + "' no existe en el sistema.");
                            ArticulosFaltantes.Add(new ARTICULO() { ARTICULO1 = "El número dde serie '" + ii.NUMERO_SERIE + "' no existe en el sistema.", EQUIPO = new EQUIPO() { EQUIPO_NAME = "1" }, CATEGORIA = new CATEGORIA() { CATEGORIA_NAME = ii.SKU }, MODELO = new MODELO() { MODELO_NAME = ii.NUMERO_SERIE } });                   

                            ii.CANTIDAD--;
                        }
                    }
                }
            }

            ImprimirFinalmente(ArticulosFaltantes);
        }

        private void ImprimirFinalmente(List<ARTICULO> ArtExcel) {

            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.DefaultExt = ".xlsx";
            dlg.Filter = "Documentos Excel (.xlsx)|*.xlsx";
            if (dlg.ShowDialog() == true)
            {
                string filename = dlg.FileName;
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
                excel.Visible = false;

                Workbook excelPrint = excel.Workbooks.Open(@"C:\Programs\ElaraInventario\Resources\ExcelInventarioFisico.xlsx", Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                Worksheet excelSheetPrint = (Worksheet)excelPrint.Worksheets[1];

                //Folio
                excel.Cells[8, 6] = SelectedAlmacen.ALMACEN_NAME;
                //Fecha
                excel.Cells[8, 23] =  DateTime.Now.ToShortDateString() + " - " + DateTime.Now.ToShortTimeString();
                
                int X = 11;
                Microsoft.Office.Interop.Excel.Borders borders;

                for (int i = 0; i < ArtExcel.Count; i++)
                {                    
                    //No.
                    excel.Range[excel.Cells[X, 2], excel.Cells[X, 3]].Merge();
                    excel.Range[excel.Cells[X, 2], excel.Cells[X, 3]].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    excel.Cells[X, 2] = (i + 1).ToString() + ".-";
                    borders = excel.Range[excel.Cells[X, 2], excel.Cells[X, 3]].Borders;
                    borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    //ARTÍCULO																															
                    excel.Range[excel.Cells[X, 4], excel.Cells[X, 22]].Merge();
                    excel.Range[excel.Cells[X, 4], excel.Cells[X, 22]].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    excel.Cells[X, 4] = ArtExcel[i].ARTICULO1;
                    borders = excel.Range[excel.Cells[X, 4], excel.Cells[X, 22]].Borders;
                    borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    //SKU					
                    excel.Range[excel.Cells[X, 23], excel.Cells[X, 28]].Merge();
                    excel.Range[excel.Cells[X, 23], excel.Cells[X, 28]].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    if (ArtExcel[i].CATEGORIA != null && ArtExcel[i].CATEGORIA.CATEGORIA_NAME != null)
                        excel.Cells[X, 23] = ArtExcel[i].CATEGORIA.CATEGORIA_NAME;
                    borders = excel.Range[excel.Cells[X, 23], excel.Cells[X, 28]].Borders;
                    borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    //No. de Serie					
                    excel.Range[excel.Cells[X, 29], excel.Cells[X, 35]].Merge();
                    excel.Range[excel.Cells[X, 29], excel.Cells[X, 35]].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    if (ArtExcel[i].MODELO != null && ArtExcel[i].MODELO.MODELO_NAME != null)
                        excel.Cells[X, 29] = ArtExcel[i].MODELO.MODELO_NAME;
                    borders = excel.Range[excel.Cells[X, 29], excel.Cells[X, 35]].Borders;
                    borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    //cantidad				
                    excel.Range[excel.Cells[X, 36], excel.Cells[X, 41]].Merge();
                    excel.Range[excel.Cells[X, 36], excel.Cells[X, 41]].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    excel.Cells[X, 36] = ArtExcel[i].EQUIPO.EQUIPO_NAME;
                    borders = excel.Range[excel.Cells[X, 36], excel.Cells[X, 41]].Borders;
                    borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    
                    ItemDataMapper iaux = new ItemDataMapper();

                    if (IsSKU)
                    {
                        //Moneda				
                        excel.Range[excel.Cells[X, 42], excel.Cells[X, 47]].Merge();
                        excel.Range[excel.Cells[X, 42], excel.Cells[X, 47]].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        if (ArtExcel[i].CATEGORIA != null && ArtExcel[i].CATEGORIA.CATEGORIA_NAME != null)
                            excel.Cells[X, 42] = iaux.ExcelGetMonedaSKU(ArtExcel[i].CATEGORIA.CATEGORIA_NAME);
                        borders = excel.Range[excel.Cells[X, 42], excel.Cells[X, 47]].Borders;
                        borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;

                        //Tipo de Cambio				
                        excel.Range[excel.Cells[X, 48], excel.Cells[X, 53]].Merge();
                        excel.Range[excel.Cells[X, 48], excel.Cells[X, 53]].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        if (ArtExcel[i].CATEGORIA != null && ArtExcel[i].CATEGORIA.CATEGORIA_NAME != null)    
                            excel.Cells[X, 48] = iaux.ExcelGetTcSKU(ArtExcel[i].CATEGORIA.CATEGORIA_NAME);
                        borders = excel.Range[excel.Cells[X, 48], excel.Cells[X, 53]].Borders;
                        borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;

                        //Costo Unitario				
                        excel.Range[excel.Cells[X, 54], excel.Cells[X, 59]].Merge();
                        excel.Range[excel.Cells[X, 54], excel.Cells[X, 59]].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        if (ArtExcel[i].CATEGORIA != null && ArtExcel[i].CATEGORIA.CATEGORIA_NAME != null)
                            excel.Cells[X, 54] = iaux.ExcelGetCostoUnitarioSKU(ArtExcel[i].CATEGORIA.CATEGORIA_NAME).ToString();
                        borders = excel.Range[excel.Cells[X, 54], excel.Cells[X, 59]].Borders;
                        borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    }
                    else
                    {

                        //Moneda				
                        excel.Range[excel.Cells[X, 42], excel.Cells[X, 47]].Merge();
                        excel.Range[excel.Cells[X, 42], excel.Cells[X, 47]].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        if (ArtExcel[i].MODELO != null && ArtExcel[i].MODELO.MODELO_NAME != null)
                            excel.Cells[X, 42] = iaux.ExcelGetMonedaNUMEROSERIE(ArtExcel[i].MODELO.MODELO_NAME);
                        borders = excel.Range[excel.Cells[X, 42], excel.Cells[X, 47]].Borders;
                        borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;

                        //Tipo de Cambio				
                        excel.Range[excel.Cells[X, 48], excel.Cells[X, 53]].Merge();
                        excel.Range[excel.Cells[X, 48], excel.Cells[X, 53]].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        if (ArtExcel[i].MODELO != null && ArtExcel[i].MODELO.MODELO_NAME != null)
                            excel.Cells[X, 48] = iaux.ExcelGetTcNUMEROSERIE(ArtExcel[i].MODELO.MODELO_NAME);
                        borders = excel.Range[excel.Cells[X, 48], excel.Cells[X, 53]].Borders;
                        borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;

                        //Costo Unitario				
                        excel.Range[excel.Cells[X, 54], excel.Cells[X, 59]].Merge();
                        excel.Range[excel.Cells[X, 54], excel.Cells[X, 59]].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        if (ArtExcel[i].MODELO != null && ArtExcel[i].MODELO.MODELO_NAME != null)
                            excel.Cells[X, 54] = iaux.ExcelGetCostoUnitarioNUMEROSERIE(ArtExcel[i].MODELO.MODELO_NAME).ToString();
                        borders = excel.Range[excel.Cells[X, 54], excel.Cells[X, 59]].Borders;
                        borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    }
                    
                    //ESTATUS					
                    excel.Range[excel.Cells[X, 60], excel.Cells[X, 66]].Merge();
                    excel.Range[excel.Cells[X, 60], excel.Cells[X, 66]].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                    if (Int32.Parse(ArtExcel[i].EQUIPO.EQUIPO_NAME) > 0)
                        excel.Cells[X, 60] = "Artículo en existencia";
                    else
                        excel.Cells[X, 60] = "Artículo extraviado";

                    borders = excel.Range[excel.Cells[X, 60], excel.Cells[X, 66]].Borders;
                    borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    X++;
                }

                excelSheetPrint.SaveAs(filename, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                excel.Visible = true;
            }
        }
        private bool ExisteRegistro(string s) {

            ItemDataMapper iDM = new ItemDataMapper();
            if (IsSKU)
                return iDM.ExistSKU(s);
            return iDM.ExistNumSerie(s);
        }
        private string GetNameArticulo(string s)
        {
            ItemDataMapper iDM = new ItemDataMapper();
            if (IsSKU)
                return iDM.GetSKU(s);
            return iDM.GetNumSerie(s);
        }

        #endregion

        #region Properties

        bool BotonFinalizar;
        bool BotonImprimir;

        public ObservableCollection<Descriptor> DescriptorCollection
        {
            get { return _DescriptorCollection; }
            set
            {
                if (_DescriptorCollection != value)
                {
                    _DescriptorCollection = value;
                    OnPropertyChanged(DescriptorPropertyName);
                }
            }
        }
        protected ObservableCollection<Descriptor> _DescriptorCollection;
        public const string DescriptorPropertyName = "DescriptorCollection";

        public ObservableCollection<ALMACEN> AlmacenCollection
        {
            get { return _AlmacenCollection; }
            set
            {
                if (_AlmacenCollection != value)
                {
                    _AlmacenCollection = value;
                    OnPropertyChanged(AlmacenCollectionInventarioPropertyName);
                }
            }
        }
        protected ObservableCollection<ALMACEN> _AlmacenCollection;
        public const string AlmacenCollectionInventarioPropertyName = "AlmacenCollection";

        public ALMACEN SelectedAlmacen
        {
            get { return _SelectedAlmacen; }
            set
            {
                if (_SelectedAlmacen != value)
                {
                    _SelectedAlmacen = value;
                    OnPropertyChanged(SelectedAlmacenPropertyName);
                }
            }
        }
        protected ALMACEN _SelectedAlmacen;
        public const string SelectedAlmacenPropertyName = "SelectedAlmacen";

        public bool IsSKU
        {
            get { return _IsSKU; }
            set
            {
                if (_IsSKU != value)
                {
                    _IsSKU = value;
                    OnPropertyChanged(IsSKUPropertyName);
                }
            }
        }
        protected bool _IsSKU;
        public const string IsSKUPropertyName = "IsSKU";

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
        protected bool _IsEnabled;
        public const string IsEnabledPropertyName = "IsEnabled";

        CatalogInvViewModel catalogInvViewModel;
        public USUARIO ActualUser;

        #endregion

        #region Constructors

        public ModifyInventarioViewModel(CatalogInvViewModel c) {

            this.catalogInvViewModel = c;
            this.ActualUser = c.ActualUser;
            this.DescriptorCollection = new ObservableCollection<Descriptor>();      
            this.AlmacenCollection = GetAlmacenes();
            if (this.AlmacenCollection.Count > 0)
            {
                this.SelectedAlmacen = this.AlmacenCollection[0];
            }
            IsEnabled = true;

            BotonFinalizar = true;
            BotonImprimir = false;
            IsSKU = true;

            for (int i = 0; i < c.SelectedInventario.Descriptores.Count; i++) {

                this.DescriptorCollection.Add(new Model.CatalogInventario.Descriptor() { Cantidad = c.SelectedInventario.Descriptores[i].Cantidad, DescriptorName = c.SelectedInventario.Descriptores[i].DescriptorName, IsChecked = c.SelectedInventario.Descriptores[i].IsChecked });
            }

            this.SelectedAlmacen = c.SelectedInventario.SelectedAlmacen;
            IsEnabled = !c.SelectedInventario.Finished;
        }

        #endregion

        #region Methods

        public ObservableCollection<ALMACEN> GetAlmacenes() {

            ObservableCollection<ALMACEN> res = new ObservableCollection<ALMACEN>();
            List<ALMACEN> aux = new List<ALMACEN>();

            AlmacenDataMapper aDM = new AlmacenDataMapper();
            aux = (List<ALMACEN>)aDM.getElements();

            foreach (ALMACEN aa in aux) {

                res.Add(aa);
            }

            return res;
        }

        #endregion
    }
}
