using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;
using System.ComponentModel;

namespace InventoryApp.Model
{
    public class CatalogProveedorCuentaModel : INotifyPropertyChanged
    {
        private FixupCollection<PROVEEDOR_CUENTA> _proveedorCuenta;
        private PROVEEDOR_CUENTA _selectedProveedorCuenta;
        private IDataMapper _dataMapper;

        public FixupCollection<PROVEEDOR_CUENTA> ProveedorCuenta
        {
            get
            {
                return _proveedorCuenta;
            }
            set
            {
                if (_proveedorCuenta != value)
                {
                    _proveedorCuenta = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("ProveedorCuenta"));
                    }
                }
            }
        }

        public PROVEEDOR_CUENTA SelectedProveedorCuenta
        {
            get
            {
                return _selectedProveedorCuenta;
            }
            set
            {
                if (_selectedProveedorCuenta != value)
                {
                    _selectedProveedorCuenta = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("SelectedProveedorCuenta"));
                    }
                }
            }
        }

        public void loadItems()
        {
            object element = this._dataMapper.getElements();

            FixupCollection<PROVEEDOR_CUENTA> ic = new FixupCollection<PROVEEDOR_CUENTA>();

            foreach (PROVEEDOR_CUENTA elemento in (List<PROVEEDOR_CUENTA>)element)
            {
                ic.Add((PROVEEDOR_CUENTA)elemento);
            }
            if (ic != null)
            {
                this.ProveedorCuenta = ic;
            }



            //FixupCollection<PROVEEDOR> ic = element as FixupCollection<PROVEEDOR>;
            //if (ic != null)
            //{
            //    this.Proveedor = ic;
            //}
        }

        public CatalogProveedorCuentaModel(IDataMapper dataMapper)
        {
            this._dataMapper = new ProveedorCuentaDataMapper();
            this._proveedorCuenta = new FixupCollection<PROVEEDOR_CUENTA>();
            this._selectedProveedorCuenta = new PROVEEDOR_CUENTA();
            this.loadItems();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
