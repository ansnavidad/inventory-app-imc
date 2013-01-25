using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;
using System.ComponentModel;

namespace InventoryApp.Model
{
    public class CatalogMovimientoModel : INotifyPropertyChanged
    {
        private FixupCollection<CatalogMovimiento> _catalogMovimiento;
        private MOVIMENTO _selectedMovimiento;
        private MovimientoDataMapper _dataMapper;

        public FixupCollection<CatalogMovimiento> CatalogMovimiento
        {
            get
            {
                return _catalogMovimiento;
            }
            set
            {
                if (_catalogMovimiento != value)
                {
                    _catalogMovimiento = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("CatalogMovimiento"));
                    }
                }
            }
        }

        public MOVIMENTO SelectedMovimiento
        {
            get
            {
                return _selectedMovimiento;
            }
            set
            {
                if (_selectedMovimiento != value)
                {
                    _selectedMovimiento = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("SelectedMovimiento"));
                    }
                }
            }
        }

        public void loadItems()
        {
            object element = this._dataMapper.getElements();

            FixupCollection<CatalogMovimiento> ic = new FixupCollection<CatalogMovimiento>();

            if (element != null)
            {
                if (((List<MOVIMENTO>)element).Count > 0)
                {
                    foreach (MOVIMENTO item in (List<MOVIMENTO>)element)
                    {
                        CatalogMovimiento aux = new CatalogMovimiento(item);
                        ic.Add(aux);
                    }
                }
            }
            this.CatalogMovimiento = ic;
        }
        
        #region GetElements para los Grids de Movimientos (Salidas/Traspaso/Entradas)

        //Ya no se usa
        //public void loadItemsEntrada()
        //{
        //    object element = this._dataMapper.getEntradasElements();

        //    FixupCollection<CatalogMovimiento> ic = new FixupCollection<CatalogMovimiento>();

        //    if (element != null)
        //    {
        //        if (((List<MOVIMENTO>)element).Count > 0)
        //        {
        //            foreach (MOVIMENTO item in (List<MOVIMENTO>)element)
        //            {
        //                CatalogMovimiento aux = new CatalogMovimiento(item);
        //                ic.Add(aux);
        //            }
        //        }
        //    }
        //    this.CatalogMovimiento = ic;
        //}

        public void loadItemsSalida()
        {
            object element = this._dataMapper.getSalidasElements();

            FixupCollection<CatalogMovimiento> ic = new FixupCollection<CatalogMovimiento>();

            if (element != null)
            {
                if (((List<MOVIMENTO>)element).Count > 0)
                {
                    foreach (MOVIMENTO item in (List<MOVIMENTO>)element)
                    {
                        CatalogMovimiento aux = new CatalogMovimiento(item);
                        ic.Add(aux);
                    }
                }
            }
            this.CatalogMovimiento = ic;
        }

        public void loadItemsEntradaDesinstalacion()
        {
            object element = this._dataMapper.getEntradaDesinstalacionElements();

            FixupCollection<CatalogMovimiento> ic = new FixupCollection<CatalogMovimiento>();

            if (element != null)
            {
                if (((List<MOVIMENTO>)element).Count > 0)
                {
                    foreach (MOVIMENTO item in (List<MOVIMENTO>)element)
                    {
                        CatalogMovimiento aux = new CatalogMovimiento(item);
                        ic.Add(aux);
                    }
                }
            }
            this.CatalogMovimiento = ic;
        }

        public void loadItemsEntradaDevolucion()
        {
            object element = this._dataMapper.getEntradaDevolucionElements();

            FixupCollection<CatalogMovimiento> ic = new FixupCollection<CatalogMovimiento>();

            if (element != null)
            {
                if (((List<MOVIMENTO>)element).Count > 0)
                {
                    foreach (MOVIMENTO item in (List<MOVIMENTO>)element)
                    {
                        CatalogMovimiento aux = new CatalogMovimiento(item);
                        ic.Add(aux);
                    }
                }
            }
            this.CatalogMovimiento = ic;
        }

        public void loadItemsEntradaPrestamo()
        {
            object element = this._dataMapper.getEntradaPrestamoElements();

            FixupCollection<CatalogMovimiento> ic = new FixupCollection<CatalogMovimiento>();

            if (element != null)
            {
                if (((List<MOVIMENTO>)element).Count > 0)
                {
                    foreach (MOVIMENTO item in (List<MOVIMENTO>)element)
                    {
                        CatalogMovimiento aux = new CatalogMovimiento(item);
                        ic.Add(aux);
                    }
                }
            }
            this.CatalogMovimiento = ic;
        }

        public void loadItemsEntradaValidacion()
        {
            object element = this._dataMapper.getEntradaValidacionElements();

            FixupCollection<CatalogMovimiento> ic = new FixupCollection<CatalogMovimiento>();

            if (element != null)
            {
                if (((List<MOVIMENTO>)element).Count > 0)
                {
                    foreach (MOVIMENTO item in (List<MOVIMENTO>)element)
                    {
                        CatalogMovimiento aux = new CatalogMovimiento(item);
                        ic.Add(aux);
                    }
                }
            }
            this.CatalogMovimiento = ic;
        }

        public void loadItemsSalidaConfiguracion()
        {
            object element = this._dataMapper.getSalidaConfiguracionElements();

            FixupCollection<CatalogMovimiento> ic = new FixupCollection<CatalogMovimiento>();

            if (element != null)
            {
                if (((List<MOVIMENTO>)element).Count > 0)
                {
                    foreach (MOVIMENTO item in (List<MOVIMENTO>)element)
                    {
                        CatalogMovimiento aux = new CatalogMovimiento(item);
                        ic.Add(aux);
                    }
                }
            }
            this.CatalogMovimiento = ic;
        }

        public void loadItemsSalidaCorrectivo()
        {
            object element = this._dataMapper.getSalidaCorrectivoElements();

            FixupCollection<CatalogMovimiento> ic = new FixupCollection<CatalogMovimiento>();

            if (element != null)
            {
                if (((List<MOVIMENTO>)element).Count > 0)
                {
                    foreach (MOVIMENTO item in (List<MOVIMENTO>)element)
                    {
                        CatalogMovimiento aux = new CatalogMovimiento(item);
                        ic.Add(aux);
                    }
                }
            }
            this.CatalogMovimiento = ic;
        }

        public void loadItemsSalidaDemo()
        {
            object element = this._dataMapper.getSalidaDemoElements();

            FixupCollection<CatalogMovimiento> ic = new FixupCollection<CatalogMovimiento>();

            if (element != null)
            {
                if (((List<MOVIMENTO>)element).Count > 0)
                {
                    foreach (MOVIMENTO item in (List<MOVIMENTO>)element)
                    {
                        CatalogMovimiento aux = new CatalogMovimiento(item);
                        ic.Add(aux);
                    }
                }
            }
            this.CatalogMovimiento = ic;
        }

        public void loadItemsSalidaObsequio()
        {
            object element = this._dataMapper.getSalidaObsequioElements();

            FixupCollection<CatalogMovimiento> ic = new FixupCollection<CatalogMovimiento>();

            if (element != null)
            {
                if (((List<MOVIMENTO>)element).Count > 0)
                {
                    foreach (MOVIMENTO item in (List<MOVIMENTO>)element)
                    {
                        CatalogMovimiento aux = new CatalogMovimiento(item);
                        ic.Add(aux);
                    }
                }
            }
            this.CatalogMovimiento = ic;
        }

        public void loadItemsSalidaOffice()
        {
            object element = this._dataMapper.getSalidaOfficeElements();

            FixupCollection<CatalogMovimiento> ic = new FixupCollection<CatalogMovimiento>();

            if (element != null)
            {
                if (((List<MOVIMENTO>)element).Count > 0)
                {
                    foreach (MOVIMENTO item in (List<MOVIMENTO>)element)
                    {
                        CatalogMovimiento aux = new CatalogMovimiento(item);
                        ic.Add(aux);
                    }
                }
            }
            this.CatalogMovimiento = ic;
        }

        public void loadItemsSalidaPrestamo()
        {
            object element = this._dataMapper.getSalidaPrestamoElements();

            FixupCollection<CatalogMovimiento> ic = new FixupCollection<CatalogMovimiento>();

            if (element != null)
            {
                if (((List<MOVIMENTO>)element).Count > 0)
                {
                    foreach (MOVIMENTO item in (List<MOVIMENTO>)element)
                    {
                        CatalogMovimiento aux = new CatalogMovimiento(item);
                        ic.Add(aux);
                    }
                }
            }
            this.CatalogMovimiento = ic;
        }

        public void loadItemsSalidaPruebas()
        {
            object element = this._dataMapper.getSalidaPruebasElements();

            FixupCollection<CatalogMovimiento> ic = new FixupCollection<CatalogMovimiento>();

            if (element != null)
            {
                if (((List<MOVIMENTO>)element).Count > 0)
                {
                    foreach (MOVIMENTO item in (List<MOVIMENTO>)element)
                    {
                        CatalogMovimiento aux = new CatalogMovimiento(item);
                        ic.Add(aux);
                    }
                }
            }
            this.CatalogMovimiento = ic;
        }

        public void loadItemsSalidaRenta()
        {
            object element = this._dataMapper.getSalidaRentaElements();

            FixupCollection<CatalogMovimiento> ic = new FixupCollection<CatalogMovimiento>();

            if (element != null)
            {
                if (((List<MOVIMENTO>)element).Count > 0)
                {
                    foreach (MOVIMENTO item in (List<MOVIMENTO>)element)
                    {
                        CatalogMovimiento aux = new CatalogMovimiento(item);
                        ic.Add(aux);
                    }
                }
            }
            this.CatalogMovimiento = ic;
        }

        public void loadItemsSalidaRevision()
        {
            object element = this._dataMapper.getSalidaRevisionElements();

            FixupCollection<CatalogMovimiento> ic = new FixupCollection<CatalogMovimiento>();

            if (element != null)
            {
                if (((List<MOVIMENTO>)element).Count > 0)
                {
                    foreach (MOVIMENTO item in (List<MOVIMENTO>)element)
                    {
                        CatalogMovimiento aux = new CatalogMovimiento(item);
                        ic.Add(aux);
                    }
                }
            }
            this.CatalogMovimiento = ic;
        }

        public void loadItemsSalidaRMA()
        {
            object element = this._dataMapper.getSalidaRMAElements();

            FixupCollection<CatalogMovimiento> ic = new FixupCollection<CatalogMovimiento>();

            if (element != null)
            {
                if (((List<MOVIMENTO>)element).Count > 0)
                {
                    foreach (MOVIMENTO item in (List<MOVIMENTO>)element)
                    {
                        CatalogMovimiento aux = new CatalogMovimiento(item);
                        ic.Add(aux);
                    }
                }
            }
            this.CatalogMovimiento = ic;
        }

        public void loadItemsSalidaVenta()
        {
            object element = this._dataMapper.getSalidaVentaElements();

            FixupCollection<CatalogMovimiento> ic = new FixupCollection<CatalogMovimiento>();

            if (element != null)
            {
                if (((List<MOVIMENTO>)element).Count > 0)
                {
                    foreach (MOVIMENTO item in (List<MOVIMENTO>)element)
                    {
                        CatalogMovimiento aux = new CatalogMovimiento(item);
                        ic.Add(aux);
                    }
                }
            }
            this.CatalogMovimiento = ic;
        }
        
        public void loadItemsTraspaso()
        {
            object element = this._dataMapper.getTraspasos();

            FixupCollection<CatalogMovimiento> ic = new FixupCollection<CatalogMovimiento>();

            if (element != null)
            {
                if (((List<MOVIMENTO>)element).Count > 0)
                {
                    foreach (MOVIMENTO item in (List<MOVIMENTO>)element)
                    {
                        CatalogMovimiento aux = new CatalogMovimiento(item);
                        ic.Add(aux);
                    }
                }
            }
            this.CatalogMovimiento = ic;
        }

        #endregion

        public CatalogMovimientoModel(IDataMapper dataMapper)
        {
            this._dataMapper = new MovimientoDataMapper();
            this._catalogMovimiento = new FixupCollection<CatalogMovimiento>();
            this._selectedMovimiento = new MOVIMENTO();
            this.loadItems();
        }

        //Constructor Entradas/Salidas
        public CatalogMovimientoModel(IDataMapper dataMapper, string name)
        {
            this._dataMapper = new MovimientoDataMapper();
            this._catalogMovimiento = new FixupCollection<CatalogMovimiento>();
            this._selectedMovimiento = new MOVIMENTO();

            if (name.Equals("Entrada Validación/Entrada"))
                this.loadItemsEntradaValidacion();
            else if (name.Equals("Entrada Prestamo"))
                this.loadItemsEntradaPrestamo();
            else if (name.Equals("Entrada Devolucion"))
                this.loadItemsEntradaDevolucion();
            else if (name.Equals("Entrada Desinstalacion"))
                this.loadItemsEntradaDesinstalacion();
            else if (name.Equals("Salida Renta"))
                this.loadItemsSalidaRenta();
            else if (name.Equals("Salida Demo"))
                this.loadItemsSalidaDemo();
            else if (name.Equals("Salida Prestamo"))
                this.loadItemsSalidaPrestamo();
            else if (name.Equals("Salida Venta"))
                this.loadItemsSalidaVenta();
            else if (name.Equals("Salida RMA"))
                this.loadItemsSalidaRMA();
            else if (name.Equals("Salida Revision"))
                this.loadItemsSalidaRevision();
            else if (name.Equals("Salida Pruebas"))
                this.loadItemsSalidaPruebas();
            else if (name.Equals("Salida Configuración"))
                this.loadItemsSalidaConfiguracion();
            else if (name.Equals("Salida Obsequio"))
                this.loadItemsSalidaObsequio();
            else if (name.Equals("Salida Correctivo"))
                this.loadItemsSalidaCorrectivo();
            else if (name.Equals("Entregado"))
                this.loadItemsSalidaOffice();
            else if (name.Equals("SALIDAS"))
                this.loadItemsSalida();
        }
        //Constructor Traspasos
        public CatalogMovimientoModel(IDataMapper dataMapper, int i)
        {
            this._dataMapper = new MovimientoDataMapper();
            this._catalogMovimiento = new FixupCollection<CatalogMovimiento>();
            this._selectedMovimiento = new MOVIMENTO();
            this.loadItemsTraspaso();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
