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
        private FixupCollection<DeleteProveedor> _proveedor;
        private DeleteProveedor _selectedProveedor;
        private IDataMapper _dataMapper;

        public FixupCollection<DeleteProveedor> Proveedor
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

        public DeleteProveedor SelectedProveedor
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

            FixupCollection<DeleteProveedor> ic = new FixupCollection<DeleteProveedor>();

            if (element != null)
            {
                if (((List<PROVEEDOR>)element).Count > 0)
                {
                    foreach (PROVEEDOR item in (List<PROVEEDOR>)element)
                    {
                        DeleteProveedor aux = new DeleteProveedor(item);
                        ic.Add(aux);
                    }
                }
            }
            this.Proveedor = ic;
        }

        public void deleteProveedor()
        {
            foreach (DeleteProveedor item in this._proveedor)
            {
                if (item.IsChecked)
                {
                    this._dataMapper.deleteElement(item);
                }
            }
        }

        public CatalogProveedorModel(IDataMapper dataMapper)
        {
            this._dataMapper = new ProveedorDataMapper();
            this._proveedor = new FixupCollection<DeleteProveedor>();
            //this._selectedProveedor = new PROVEEDOR();
            this.loadItems();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
