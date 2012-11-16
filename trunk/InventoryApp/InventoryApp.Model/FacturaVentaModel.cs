using System;
using System.Collections.Generic;
using System.Linq;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;
using System.ComponentModel;
using System.Text;

namespace InventoryApp.Model
{
    public class FacturaVentaModel : INotifyPropertyChanged
    {
         #region Fields
        private long _unidFacturaVenta;
        private string _folio;
        private float _iva;
        private float _porIva;
        private float _impFactura;
        private float _tipoCambio;
        public float _total;
        private MONEDA _moneda;
        private FacturaVentaDataMapper _dataMapper;
        // aqui me quede
        #endregion

        #region Props
        public long UnidFacturaVenta
        {
            get
            {
                return _unidFacturaVenta;
            }
            set
            {
                if (_unidFacturaVenta != value)
                {
                    _unidFacturaVenta = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("UnidFacturaVenta"));
                    }
                }
            }
        }

        public float Iva
        {
            get
            {
                return _iva;
            }
            set
            {
                if (_iva != value)
                {
                    _iva = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Iva"));
                    }
                }
            }
        }

        public float Total
        {
            get
            {
                return _total;
            }
            set
            {
                if (_total != value)
                {
                    _total = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Total"));
                    }
                }
            }
        }

        public float PorIva
        {
            get
            {
                return _porIva;
            }
            set
            {
                if (_porIva != value)
                {
                    _porIva = value;
                    calculaTotal();
                    calculaIVA();
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("PorIva"));
                    }
                }
            }
        }
        public string Folio
        {
            get
            {
                return _folio;
            }
            set
            {
                if (_folio != value)
                {
                    _folio = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Folio"));
                    }
                }
            }
        }

        public float ImpFactura
        {
            get
            {
                return _impFactura;
            }
            set
            {
                if (_impFactura != value)
                {
                    _impFactura = value;
                    this.calculaTotal();
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("ImpFactura"));
                    }
                }
            }
        }

        public float TipoCambio
        {
            get
            {
                return _tipoCambio;
            }
            set
            {
                if (_tipoCambio != value)
                {
                    _tipoCambio = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("TipoCambio"));
                    }
                }
            }
        }

        public MONEDA Moneda
        {
            get
            {
                return _moneda;
            }
            set
            {
                if (_moneda != value)
                {
                    _moneda = value;
                    if (PropertyChanged != null)
                    {
                        this.PropertyChanged(this, new PropertyChangedEventArgs("Moneda"));
                    }
                }
            }
        }
        #endregion

        public void saveFactura()
        {
            if (_dataMapper != null)
            {
                _dataMapper.insertElement(new FACTURA_VENTA() {UNID_FACTURA_VENTA=this._unidFacturaVenta, FOLIO = this._folio, IVA_PESOS = this._iva, POR_IVA = this._porIva, IMPORTE_FACTURA = this._impFactura, UNID_MONEDA = this._moneda.UNID_MONEDA, TIPO_CAMBIO = this._tipoCambio, TOTAL_DESC_FACTURA= "", TOTAL_FACTURA = 1.0});
            }
        }

       
        #region Constructors
        public FacturaVentaModel(IDataMapper dataMapper)
        {
            if ((dataMapper as FacturaVentaDataMapper) != null)
            {
                this._dataMapper = new FacturaVentaDataMapper();
            }
            this._unidFacturaVenta= UNID.getNewUNID();
            this._iva = 0;
            this._porIva = 0;
            this._impFactura = 0;
            this._total = 0;

        }
        #endregion

        public void calculaTotal()
        {
            this.Total = this._impFactura * (1 + (this._porIva/100));
        }

        public void calculaIVA()
        {
            this.Iva = this._impFactura * (this._porIva / 100);
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
