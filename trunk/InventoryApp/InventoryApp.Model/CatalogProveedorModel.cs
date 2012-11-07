using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL.POCOS;
using InventoryApp.DAL;
using System.ComponentModel;

namespace InventoryApp.Model
{
    public class CatalogProveedorModel : INotifyPropertyChanged
    {
        private FixupCollection<PROVEEDOR> _proveedor;
        private PROVEEDOR _selectedProveedor;
        private IDataMapper _dataMapper;

        public FixupCollection<PROVEEDOR> Proveedor
        {
            get
            {
                return _proveedor;
            }
            set
            {
                if (_proveedor != value)
                {
                    _proveedor = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Proveedor"));
                    }
                }
            }
        }

        public PROVEEDOR SelectedProveedor
        {
            get
            {
                return _selectedProveedor;
            }
            set
            {
                if (_selectedProveedor != value)
                {
                    _selectedProveedor = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("SelectedProveedor"));
                    }
                }
            }
        }

        public void loadItems()
        {
            object element = this._dataMapper.getElements();

            FixupCollection<PROVEEDOR> ic = new FixupCollection<PROVEEDOR>();

            foreach (PROVEEDOR elemento in (List<PROVEEDOR>)element)
            {
                ic.Add((PROVEEDOR)elemento);
            }
            if (ic != null)
            {
                this.Proveedor = ic;
            }



            //FixupCollection<PROVEEDOR> ic = element as FixupCollection<PROVEEDOR>;
            //if (ic != null)
            //{
            //    this.Proveedor = ic;
            //}
        }

        public CatalogProveedorModel(IDataMapper dataMapper)
        {
            this._dataMapper = new ProveedorDataMapper();
            this._proveedor = new FixupCollection<PROVEEDOR>();
            this._selectedProveedor = new PROVEEDOR();
            this.loadItems();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
