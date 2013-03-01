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
        private FixupCollection<DeleteCategoria> _categoria;
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
        public FixupCollection<DeleteCategoria> Categoria
        {
            get
            {
                return _categoria;
            }
            set
            {
                if (_categoria != value)
                {
                    _categoria = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Categoria"));
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

        public void loadProveedorCategoria()
        {
            object element = this._dataMapper.getElement(this._selectedProveedor);

            FixupCollection<DeleteCategoria> ic = new FixupCollection<DeleteCategoria>();

            if (element != null)
            {
                if (((List<CATEGORIA>)element).Count > 0)
                {
                    foreach (CATEGORIA item in (List<CATEGORIA>)element)
                    {
                        DeleteCategoria aux = new DeleteCategoria(item);
                        ic.Add(aux);
                    }
                }
            }
            this.Categoria = ic;
        }

        public void deleteProveedor(USUARIO u)
        {
            foreach (DeleteProveedor item in this._proveedor)
            {
                if (item.IsChecked)
                {
                    this._dataMapper.deleteElement(item, u);
                }
            }
        }

        public CatalogProveedorModel(IDataMapper dataMapper)
        {
            this._dataMapper = new ProveedorDataMapper();
            this._proveedor = new FixupCollection<DeleteProveedor>();
            this._categoria = new FixupCollection<DeleteCategoria>();
            //this._selectedProveedor = new PROVEEDOR();
            this.loadItems();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
