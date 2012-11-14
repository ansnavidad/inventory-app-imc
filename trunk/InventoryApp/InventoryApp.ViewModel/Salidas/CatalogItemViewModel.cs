using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using InventoryApp.DAL;
using System.Windows.Input;

namespace InventoryApp.ViewModel.Salidas
{
    public class CatalogItemViewModel
    {
        private CatalogItemModel _catalogItemModel;
        private RelayCommand _addItemCommand;
        private RelayCommand _addItemsCommand;
        private SalidaRentaViewModel _salidaRentaViewModel;
        private SalidaRevisionViewModel _salidaRevisionViewModel;
	    private SalidaDemoViewModel _salidaDemoViewModel;

        public CatalogItemViewModel(SalidaRentaViewModel _salidaRentaViewModel)
        {
            IDataMapper dataMapper = new ItemDataMapper();
            this._catalogItemModel = new CatalogItemModel(dataMapper);

            this._salidaRentaViewModel = _salidaRentaViewModel;

        }

        public CatalogItemViewModel(SalidaRevisionViewModel _salidaRevisionViewModel)
        {
            IDataMapper dataMapper = new ItemDataMapper();
            this._catalogItemModel = new CatalogItemModel(dataMapper);

            this._salidaRevisionViewModel = _salidaRevisionViewModel;

        }

		public CatalogItemViewModel(SalidaDemoViewModel _salidaDemoViewModel)
        {
            IDataMapper dataMapper = new ItemDataMapper();
            this._catalogItemModel = new CatalogItemModel(dataMapper);

            this._salidaDemoViewModel = _salidaDemoViewModel;

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

        public SalidaRentaViewModel SalidaRentaViewModel
        {
            get
            {
                return _salidaRentaViewModel;
            }
            set
            {
                _salidaRentaViewModel = value;
            }
        }

        public SalidaRevisionViewModel SalidaRevisionViewModel
        {
            get
            {
                return _salidaRevisionViewModel;
            }
            set
            {
                _salidaRevisionViewModel = value;
            }
        }
		
		public SalidaDemoViewModel SalidaDemoViewModel
        {
            get
            {
                return _salidaDemoViewModel;
            }
            set
            {
                _salidaDemoViewModel = value;
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
            if (!String.IsNullOrEmpty(this._catalogItemModel.Serie))
                _canInsertArticulo = true;

            return _canInsertArticulo;
        }

        public void AttempArticulo()
        {
            if (_salidaRentaViewModel != null)
            {
                this.CatalogItemModel.loadItems(_salidaRentaViewModel.MovimientoModel.AlmacenProcedencia);
            }
            else if(_salidaRevisionViewModel != null)
            {
                this.CatalogItemModel.loadItems(_salidaRevisionViewModel.MovimientoModel.AlmacenProcedencia);
            } 
			else if (_salidaDemoViewModel != null)
            {
                this.CatalogItemModel.loadItems(_salidaDemoViewModel.MovimientoModel.AlmacenProcedencia);
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
            if (_salidaRentaViewModel != null)
            {
                foreach (ItemModel item in this._catalogItemModel.ItemModel)
                {
                    if (item.IsChecked)
                    {
                        bool aux = true;

                        for (int i = 0; i < this._salidaRentaViewModel.ItemModel.ItemModel.Count; i++)
                        {
                            if (this._salidaRentaViewModel.ItemModel.ItemModel[i].UnidItem == item.UnidItem)
                                aux = false;
                        }

                        if (aux)
                        {
                            item.IsChecked = false;
                            this._salidaRentaViewModel.ItemModel.ItemModel.Add(item);
                        }
                    }
                }
                this.SalidaRentaViewModel.MovimientoModel.CantidadItems = this.SalidaRentaViewModel.ItemModel.ItemModel.Count();
            }
            else if (_salidaRevisionViewModel != null) 
            {
                foreach (ItemModel item in this._catalogItemModel.ItemModel)
                {
                    if (item.IsChecked)
                    {
                        bool aux = true;

                        for (int i = 0; i < this._salidaRevisionViewModel.ItemModel.ItemModel.Count; i++)
                        {
                            if (this._salidaRevisionViewModel.ItemModel.ItemModel[i].UnidItem == item.UnidItem)
                                aux = false;
                        }

                        if (aux)
                        {
                            item.IsChecked = false;
                            this._salidaRevisionViewModel.ItemModel.ItemModel.Add(item);
                        }
                    }
                }
                this.SalidaRevisionViewModel.MovimientoModel.CantidadItems = this.SalidaRevisionViewModel.ItemModel.ItemModel.Count();
            } 
            else if(_salidaDemoViewModel != null)
            {
			    foreach (ItemModel item in this._catalogItemModel.ItemModel)
                {
                    if (item.IsChecked)
                    {
                        bool aux = true;

                        for (int i = 0; i < this._salidaRentaViewModel.ItemModel.ItemModel.Count; i++)
                        {

                            if (this._salidaRentaViewModel.ItemModel.ItemModel[i].UnidItem == item.UnidItem)
                                aux = false;
                        }

                        if (aux)
                        {
                            item.IsChecked = false;
                            this._salidaRentaViewModel.ItemModel.ItemModel.Add(item);



                        }
                    }

                }
            this.SalidaRentaViewModel.MovimientoModel.CantidadItems = this.SalidaRentaViewModel.ItemModel.ItemModel.Count();
			}
        }
        #endregion
    }
}
