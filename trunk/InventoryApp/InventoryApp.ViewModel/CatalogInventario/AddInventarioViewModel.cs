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

namespace InventoryApp.ViewModel.CatalogInventario
{
    public class AddInventarioViewModel : ViewModelBase
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
            return true;
        }
        public void AttempGuardar()
        {
            
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
            
            foreach (InventarioModel.Descriptor ii in DescriptorCollection) { 
                
                if(IsSKU)
                    Capturados.Add(new ITEM(){ SKU = ii.Desc, CANTIDAD = 1 });
                else
                    Capturados.Add(new ITEM() { NUMERO_SERIE = ii.Desc, CANTIDAD = 1 });
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

                                if (Existentes[i].CANTIDAD == 0)
                                {
                                    Existentes.RemoveAt(i);                                    
                                }
                                if (Capturados[j].CANTIDAD == 0)
                                {
                                    Capturados.RemoveAt(j);
                                    j--;
                                }
                            }
                            else {

                                if (AuxArticulosFaltantes.Contains(Existentes[i].ARTICULO.ARTICULO1))
                                {
                                    for (int k = 0; k < ArticulosFaltantes.Count; k++)
                                    {
                                        if (ArticulosFaltantes[k].ARTICULO1.Equals(Existentes[i].ARTICULO.ARTICULO1))
                                        {
                                            ArticulosFaltantes[k].EQUIPO.EQUIPO_NAME = (Int32.Parse(ArticulosFaltantes[k].EQUIPO.EQUIPO_NAME) + 1).ToString();
                                        }
                                    }
                                }
                                else
                                {
                                    AuxArticulosFaltantes.Add(Existentes[i].ARTICULO.ARTICULO1);
                                    ArticulosFaltantes.Add(new ARTICULO() { ARTICULO1 = Existentes[i].ARTICULO.ARTICULO1, EQUIPO = new EQUIPO() { EQUIPO_NAME = "1" } });
                                }
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

                                if (Existentes[i].CANTIDAD == 0)
                                {
                                    Existentes.RemoveAt(i);                                    
                                }
                                if (Capturados[j].CANTIDAD == 0)
                                {
                                    Capturados.RemoveAt(j);
                                    j--;
                                }
                            }
                            else
                            {
                                if (AuxArticulosFaltantes.Contains(Existentes[i].ARTICULO.ARTICULO1))
                                {
                                    for (int k = 0; k < ArticulosFaltantes.Count; k++)
                                    {
                                        if (ArticulosFaltantes[k].ARTICULO1.Equals(Existentes[i].ARTICULO.ARTICULO1))
                                        {
                                            ArticulosFaltantes[k].EQUIPO.EQUIPO_NAME = (Int32.Parse(ArticulosFaltantes[k].EQUIPO.EQUIPO_NAME) + 1).ToString();
                                        }
                                    }
                                }
                                else
                                {
                                    AuxArticulosFaltantes.Add(Existentes[i].ARTICULO.ARTICULO1);
                                    ArticulosFaltantes.Add(new ARTICULO() { ARTICULO1 = Existentes[i].ARTICULO.ARTICULO1, EQUIPO = new EQUIPO() { EQUIPO_NAME = "1" } });
                                }
                            }
                        }
                    }
                }
            }            
            
            foreach (ITEM ii in Existentes) {

                while (ii.CANTIDAD > 0) {

                    if (AuxArticulosFaltantes.Contains(ii.ARTICULO.ARTICULO1))
                    {
                        for (int i = 0; i < ArticulosFaltantes.Count; i++)
                        {
                            if (ArticulosFaltantes[i].ARTICULO1.Equals(ii.ARTICULO.ARTICULO1))
                            {
                                ArticulosFaltantes[i].EQUIPO.EQUIPO_NAME = (Int32.Parse(ArticulosFaltantes[i].EQUIPO.EQUIPO_NAME) - 1).ToString();                                    
                            }
                        }
                    }
                    else {

                        AuxArticulosFaltantes.Add(ii.ARTICULO.ARTICULO1);
                        ArticulosFaltantes.Add(new ARTICULO() { ARTICULO1 = ii.ARTICULO.ARTICULO1, EQUIPO = new EQUIPO() { EQUIPO_NAME = "-1" } });
                    }

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
                            if (AuxArticulosFaltantes.Contains(aux))
                            {
                                for (int i = 0; i < ArticulosFaltantes.Count; i++)
                                {
                                    if (ArticulosFaltantes[i].ARTICULO1.Equals(aux))
                                    {
                                        ArticulosFaltantes[i].EQUIPO.EQUIPO_NAME = (Int32.Parse(ArticulosFaltantes[i].EQUIPO.EQUIPO_NAME) + 1).ToString();
                                    }
                                }
                            }
                            else
                            {

                                AuxArticulosFaltantes.Add(aux);
                                ArticulosFaltantes.Add(new ARTICULO() { ARTICULO1 = aux, EQUIPO = new EQUIPO() { EQUIPO_NAME = "1" } });
                            }

                            ii.CANTIDAD--;
                        }
                        else {

                            if (AuxArticulosFaltantes.Contains("El SKU '" + ii.SKU + "' no existe en el sistema."))
                            {
                                for (int i = 0; i < ArticulosFaltantes.Count; i++)
                                {
                                    if (ArticulosFaltantes[i].ARTICULO1.Equals("El SKU '" + ii.SKU + "' no existe en el sistema."))
                                    {
                                        ArticulosFaltantes[i].EQUIPO.EQUIPO_NAME = (Int32.Parse(ArticulosFaltantes[i].EQUIPO.EQUIPO_NAME) + 1).ToString();
                                    }
                                }
                            }
                            else
                            {
                                AuxArticulosFaltantes.Add("El SKU '" + ii.SKU + "' no existe en el sistema.");
                                ArticulosFaltantes.Add(new ARTICULO() { ARTICULO1 = "El SKU '" + ii.SKU + "' no existe en el sistema.", EQUIPO = new EQUIPO() { EQUIPO_NAME = "1" } });
                            }

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
                            if (AuxArticulosFaltantes.Contains(aux))
                            {
                                for (int i = 0; i < ArticulosFaltantes.Count; i++)
                                {
                                    if (ArticulosFaltantes[i].ARTICULO1.Equals(aux))
                                    {
                                        ArticulosFaltantes[i].EQUIPO.EQUIPO_NAME = (Int32.Parse(ArticulosFaltantes[i].EQUIPO.EQUIPO_NAME) + 1).ToString();
                                    }
                                }
                            }
                            else
                            {

                                AuxArticulosFaltantes.Add(aux);
                                ArticulosFaltantes.Add(new ARTICULO() { ARTICULO1 = aux, EQUIPO = new EQUIPO() { EQUIPO_NAME = "1" } });
                            }

                            ii.CANTIDAD--;
                        }
                        else
                        {

                            if (AuxArticulosFaltantes.Contains("El número dde serie '" + ii.NUMERO_SERIE + "' no existe en el sistema."))
                            {
                                for (int i = 0; i < ArticulosFaltantes.Count; i++)
                                {
                                    if (ArticulosFaltantes[i].ARTICULO1.Equals("El número dde serie '" + ii.NUMERO_SERIE + "' no existe en el sistema."))
                                    {
                                        ArticulosFaltantes[i].EQUIPO.EQUIPO_NAME = (Int32.Parse(ArticulosFaltantes[i].EQUIPO.EQUIPO_NAME) + 1).ToString();
                                    }
                                }
                            }
                            else
                            {
                                AuxArticulosFaltantes.Add("El número dde serie '" + ii.NUMERO_SERIE + "' no existe en el sistema.");
                                ArticulosFaltantes.Add(new ARTICULO() { ARTICULO1 = "El número dde serie '" + ii.NUMERO_SERIE + "' no existe en el sistema.", EQUIPO = new EQUIPO() { EQUIPO_NAME = "1" } });
                            }

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
                    excel.Range[excel.Cells[X, 4], excel.Cells[X, 35]].Merge();
                    excel.Range[excel.Cells[X, 4], excel.Cells[X, 35]].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    excel.Cells[X, 4] = ArtExcel[i].ARTICULO1;
                    borders = excel.Range[excel.Cells[X, 4], excel.Cells[X, 35]].Borders;
                    borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    //CANTIDAD					
                    excel.Range[excel.Cells[X, 36], excel.Cells[X, 41]].Merge();
                    excel.Range[excel.Cells[X, 36], excel.Cells[X, 41]].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    excel.Cells[X, 36] = ArtExcel[i].EQUIPO.EQUIPO_NAME;
                    borders = excel.Range[excel.Cells[X, 36], excel.Cells[X, 41]].Borders;
                    borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                    //ESTATUS					
                    excel.Range[excel.Cells[X, 42], excel.Cells[X, 48]].Merge();
                    excel.Range[excel.Cells[X, 42], excel.Cells[X, 48]].HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                    if (Int32.Parse(ArtExcel[i].EQUIPO.EQUIPO_NAME) > 0)
                        excel.Cells[X, 42] = "Artículo en existencia";
                    else
                        excel.Cells[X, 42] = "Artículo extraviado";

                    borders = excel.Range[excel.Cells[X, 42], excel.Cells[X, 48]].Borders;
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

        public ObservableCollection<InventarioModel.Descriptor> DescriptorCollection
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
        protected ObservableCollection<InventarioModel.Descriptor> _DescriptorCollection;
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

        public AddInventarioViewModel(CatalogInvViewModel c) {

            this.catalogInvViewModel = c;
            this.ActualUser = c.ActualUser;
            this.DescriptorCollection = new ObservableCollection<InventarioModel.Descriptor>();
            this.DescriptorCollection.Add(new InventarioModel.Descriptor() { Desc = "A", IsChecked = false });
            this.DescriptorCollection.Add(new InventarioModel.Descriptor() { Desc = "B", IsChecked = false });
            this.DescriptorCollection.Add(new InventarioModel.Descriptor() { Desc = "B", IsChecked = false });
            this.DescriptorCollection.Add(new InventarioModel.Descriptor() { Desc = "E", IsChecked = false });            
            this.AlmacenCollection = GetAlmacenes();
            if (this.AlmacenCollection.Count > 0)
                this.SelectedAlmacen = this.AlmacenCollection[0];
            IsEnabled = true;

            BotonFinalizar = true;
            BotonImprimir = false;
            IsSKU = true;
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
