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
        private FixupCollection<DeleteProveedorCuenta> _proveedorCuenta;
        private DeleteProveedorCuenta _selectedProveedorCuenta;
        private IDataMapper _dataMapper;

        public FixupCollection<DeleteProveedorCuenta> ProveedorCuenta
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

        public DeleteProveedorCuenta SelectedProveedorCuenta
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

            FixupCollection<DeleteProveedorCuenta> ic = new FixupCollection<DeleteProveedorCuenta>();

            if (element != null)
            {
                if (((List<PROVEEDOR_CUENTA>)element).Count > 0)
                {
                    foreach (PROVEEDOR_CUENTA item in (List<PROVEEDOR_CUENTA>)element)
                    {
                        DeleteProveedorCuenta aux = new DeleteProveedorCuenta(item);
                        ic.Add(aux);
                    }
                }
            }
            this.ProveedorCuenta = ic;
        }

        public void deleteProveedorCuenta()
        {
            foreach (DeleteProveedorCuenta item in this._proveedorCuenta)
            {
                if (item.IsChecked)
                {
                    this._dataMapper.deleteElement(item);
                }
            }
        }

        public CatalogProveedorCuentaModel(IDataMapper dataMapper)
        {
            this._dataMapper = new ProveedorCuentaDataMapper();
            this._proveedorCuenta = new FixupCollection<DeleteProveedorCuenta>();
            //this._selectedProveedorCuenta = new PROVEEDOR_CUENTA();
            this.loadItems();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
