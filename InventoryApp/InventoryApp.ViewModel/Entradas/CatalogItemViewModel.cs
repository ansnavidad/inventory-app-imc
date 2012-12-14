using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using InventoryApp.DAL;
using System.Windows.Input;

namespace InventoryApp.ViewModel.Entradas
{
    public class CatalogItemViewModel
    {

        private CatalogItemModel _catalogItemModel;
        private RelayCommand _addItemCommand;
        private RelayCommand _addItemsCommand;
        private EntradaPorValidacionViewModel _entradaPorValidacionViewModel;
        private EntradaPrestamoViewModel _entradaPrestamoViewModel;
        private EntradaDevolucionViewModel _entradaDevolucionViewModel;
        private EntradaDesinstalacionViewModel _entradaDesinstalacionViewModel;

        public CatalogItemViewModel(EntradaPorValidacionViewModel _entradaPorValidacionViewModel)
        {
            IDataMapper dataMapper = new ItemDataMapper();
            this._catalogItemModel = new CatalogItemModel(dataMapper);

            this._entradaPorValidacionViewModel = _entradaPorValidacionViewModel;

        }

        public CatalogItemViewModel(EntradaPrestamoViewModel _entradaPrestamoViewModel)
        {
            IDataMapper dataMapper = new ItemDataMapper();
            this._catalogItemModel = new CatalogItemModel(dataMapper);

            this._entradaPrestamoViewModel = _entradaPrestamoViewModel;

        }

        public CatalogItemViewModel(EntradaDevolucionViewModel _entradaDevolucionViewModel)
        {
            IDataMapper dataMapper = new ItemDataMapper();
            this._catalogItemModel = new CatalogItemModel(dataMapper);

            this._entradaDevolucionViewModel = _entradaDevolucionViewModel;

        }

        public CatalogItemViewModel(EntradaDesinstalacionViewModel _entradaDesinstalacionViewModel)
        {
            IDataMapper dataMapper = new ItemDataMapper();
            this._catalogItemModel = new CatalogItemModel(dataMapper);

            this._entradaDesinstalacionViewModel = _entradaDesinstalacionViewModel;

        }

        public CatalogItemModel CatalogItemModel
        {
            get
            {
                return _catalogItemModel;
            }
            set
            {
                _catalogItemModel = value;
            }
        }

        public EntradaPorValidacionViewModel EntradaPorValidacionViewModel
        {
            get
            {
                return _entradaPorValidacionViewModel;
            }
            set
            {
                _entradaPorValidacionViewModel = value;
            }
        }

        public EntradaPrestamoViewModel EntradaPrestamoViewModel
        {
            get
            {
                return _entradaPrestamoViewModel;
            }
            set
            {
                _entradaPrestamoViewModel = value;
            }
        }

        public EntradaDevolucionViewModel EntradaDevolucionViewModel
        {
            get
            {
                return _entradaDevolucionViewModel;
            }
            set
            {
                _entradaDevolucionViewModel = value;
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

        public ICommand AddItemsCommand
        {
            get
            {
                if (_addItemsCommand == null)
                {
                    _addItemsCommand = new RelayCommand(p => this.AttempItems(), p => this.CanAttempItems());
                }
                return _addItemsCommand;
            }
        }


        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempArticulo()
        {
            bool _canInsertArticulo = false;
            if (!String.IsNullOrEmpty(this._catalogItemModel.Serie) || !String.IsNullOrEmpty(this._catalogItemModel.SKU))
                _canInsertArticulo = true;

            return _canInsertArticulo;
        }

        public void AttempArticulo()
        {
            if (_entradaPorValidacionViewModel != null)
            {
                this.CatalogItemModel.loadItems(_entradaPorValidacionViewModel.MovimientoModel.Infraestructura);
            }
            else if (EntradaPrestamoViewModel != null)
            {
                this.CatalogItemModel.loadItems(_entradaPrestamoViewModel.MovimientoModel.ProveedorProcedencia, _entradaPrestamoViewModel.MovimientoModel.ClienteProcedencia); 
            }
            else if (_entradaDevolucionViewModel != null)
            {
                this.CatalogItemModel.loadItems(_entradaDevolucionViewModel.MovimientoModel.ProveedorProcedencia, _entradaDevolucionViewModel.MovimientoModel.ClienteProcedencia); 
            }
            else if (_entradaDesinstalacionViewModel != null)
            {
                this.CatalogItemModel.loadItems(_entradaDesinstalacionViewModel.MovimientoModel.ProveedorProcedencia, _entradaDesinstalacionViewModel.MovimientoModel.ClienteProcedencia); 
            }            
        }

        public bool CanAttempItems()
        {
            bool _canInsertArticulo = false;
            foreach (ItemModel item in this._catalogItemModel.ItemModel)
            {
                if (item.IsChecked)
                    _canInsertArticulo = true;
            }

            return _canInsertArticulo;
        }

        public void AttempItems()
        {
            if (_entradaPorValidacionViewModel != null)
            {
                passItems(_entradaPorValidacionViewModel);
            }
            else if (EntradaPrestamoViewModel != null)
            {
                passItems(_entradaPrestamoViewModel);
            }
            else if (_entradaDevolucionViewModel != null)
            {
                passItems(_entradaDevolucionViewModel);
            }
            else if (_entradaDesinstalacionViewModel != null)
            {
                passItems(_entradaDesinstalacionViewModel);
            }

        }

        public void passItems(EntradaPorValidacionViewModel entrada)
        {
            foreach (ItemModel item in this._catalogItemModel.ItemModel)
            {
                if (item.IsChecked)
                {
                    bool aux = true;

                    for (int i = 0; i < this._entradaPorValidacionViewModel.ItemModel.ItemModel.Count; i++)
                    {

                        if (this._entradaPorValidacionViewModel.ItemModel.ItemModel[i].UnidItem == item.UnidItem)
                            aux = false;
                    }

                    if (aux)
                    {
                        item.IsChecked = false;
                        this._entradaPorValidacionViewModel.ItemModel.ItemModel.Add(item);



                    }
                }

            }
            this.EntradaPorValidacionViewModel.MovimientoModel.CantidadItems = this.EntradaPorValidacionViewModel.ItemModel.ItemModel.Count();

        }

        public void passItems(EntradaPrestamoViewModel entrada)
        {
            foreach (ItemModel item in this._catalogItemModel.ItemModel)
            {
                if (item.IsChecked)
                {
                    bool aux = true;

                    for (int i = 0; i < this._entradaPrestamoViewModel.ItemModel.ItemModel.Count; i++)
                    {

                        if (this._entradaPrestamoViewModel.ItemModel.ItemModel[i].UnidItem == item.UnidItem)
                            aux = false;
                    }

                    if (aux)
                    {
                        item.IsChecked = false;
                        this._entradaPrestamoViewModel.ItemModel.ItemModel.Add(item);



                    }
                }

            }
            this.EntradaPrestamoViewModel.MovimientoModel.CantidadItems = this.EntradaPrestamoViewModel.ItemModel.ItemModel.Count();

        }

        public void passItems(EntradaDevolucionViewModel entrada)
        {
            foreach (ItemModel item in this._catalogItemModel.ItemModel)
            {
                if (item.IsChecked)
                {
                    bool aux = true;

                    for (int i = 0; i < this._entradaDevolucionViewModel.ItemModel.ItemModel.Count; i++)
                    {

                        if (this._entradaDevolucionViewModel.ItemModel.ItemModel[i].UnidItem == item.UnidItem)
                            aux = false;
                    }

                    if (aux)
                    {
                        item.IsChecked = false;
                        this._entradaDevolucionViewModel.ItemModel.ItemModel.Add(item);



                    }
                }

            }
            this.EntradaDevolucionViewModel.MovimientoModel.CantidadItems = this.EntradaDevolucionViewModel.ItemModel.ItemModel.Count();

        }

        public void passItems(EntradaDesinstalacionViewModel entrada)
        {
            foreach (ItemModel item in this._catalogItemModel.ItemModel)
            {
                if (item.IsChecked)
                {
                    bool aux = true;

                    for (int i = 0; i < this._entradaDesinstalacionViewModel.ItemModel.ItemModel.Count; i++)
                    {

                        if (this._entradaDesinstalacionViewModel.ItemModel.ItemModel[i].UnidItem == item.UnidItem)
                            aux = false;
                    }

                    if (aux)
                    {
                        item.IsChecked = false;
                        this._entradaDesinstalacionViewModel.ItemModel.ItemModel.Add(item);



                    }
                }

            }
            this._entradaDesinstalacionViewModel.MovimientoModel.CantidadItems = this._entradaDesinstalacionViewModel.ItemModel.ItemModel.Count();

        }
        #endregion
    }
}
