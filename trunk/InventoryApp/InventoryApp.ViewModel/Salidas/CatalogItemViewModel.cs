using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using InventoryApp.DAL;
using System.Windows.Input;

namespace InventoryApp.ViewModel.Salidas
{
    public class CatalogItemViewModel : IPageViewModel
    {
        private CatalogItemModel _catalogItemModel;
        private RelayCommand _addItemCommand;
        private RelayCommand _addItemsCommand;

        private string _mensaje1;
        private string _mensaje2;

        private SalidaRentaViewModel _salidaRentaViewModel;
        private SalidaRevisionViewModel _salidaRevisionViewModel;
	    private SalidaDemoViewModel _salidaDemoViewModel;
        private SalidaPrestamoViewModel _salidaPrestamoViewModel;
        private SalidaVentaViewModel _salidaVentaViewModel;
        private SalidaRMAViewModel _salidaRMAViewModel;
        private SalidaPruebasViewModel _salidaPruebasViewModel;
        private SalidaConfiguracionViewModel _salidaConfiguracionViewModel;
        private SalidaObsequioViewModel _salidaObsequioViewModel;
        private SalidaCorrectivoViewModel _salidaCorrectivoViewModel;
        private SalidaOfficeViewModel _salidaOfficeViewModel;

        public CatalogItemViewModel(SalidaVentaViewModel _salidaVentaViewModel)
        {
            IDataMapper dataMapper = new ItemDataMapper();
            this._catalogItemModel = new CatalogItemModel(dataMapper);

            this._salidaVentaViewModel = _salidaVentaViewModel;

        }
        public CatalogItemViewModel(SalidaRMAViewModel _salidaRMAViewModel)
        {
            IDataMapper dataMapper = new ItemDataMapper();
            this._catalogItemModel = new CatalogItemModel(dataMapper);

            this._salidaRMAViewModel = _salidaRMAViewModel;

        }
        public CatalogItemViewModel(SalidaPruebasViewModel _salidaPruebasViewModel)
        {
            IDataMapper dataMapper = new ItemDataMapper();
            this._catalogItemModel = new CatalogItemModel(dataMapper);

            this._salidaPruebasViewModel = _salidaPruebasViewModel;

        }
        public CatalogItemViewModel(SalidaConfiguracionViewModel _salidaConfiguracionViewModel)
        {
            IDataMapper dataMapper = new ItemDataMapper();
            this._catalogItemModel = new CatalogItemModel(dataMapper);

            this._salidaConfiguracionViewModel = _salidaConfiguracionViewModel;

        }
        public CatalogItemViewModel(SalidaObsequioViewModel _salidaObsequioViewModel)
        {
            IDataMapper dataMapper = new ItemDataMapper();
            this._catalogItemModel = new CatalogItemModel(dataMapper);

            this._salidaObsequioViewModel = _salidaObsequioViewModel;

        }
        public CatalogItemViewModel(SalidaCorrectivoViewModel _salidaCorrectivoViewModel)
        {
            IDataMapper dataMapper = new ItemDataMapper();
            this._catalogItemModel = new CatalogItemModel(dataMapper);

            this._salidaCorrectivoViewModel = _salidaCorrectivoViewModel;

        }
        public CatalogItemViewModel(SalidaOfficeViewModel _salidaOfficeViewModel)
        {
            IDataMapper dataMapper = new ItemDataMapper();
            this._catalogItemModel = new CatalogItemModel(dataMapper);

            this._salidaOfficeViewModel = _salidaOfficeViewModel;

        }
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
        public CatalogItemViewModel(SalidaPrestamoViewModel _salidaPrestamoViewModel)
        {
            IDataMapper dataMapper = new ItemDataMapper();
            this._catalogItemModel = new CatalogItemModel(dataMapper);

            this._salidaPrestamoViewModel = _salidaPrestamoViewModel;

        }

        #region Props
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

        public string Mensaje1
        {
            get
            {
                return _mensaje1;
            }
            set
            {
                _mensaje1 = value;
            }
        }

        public string Mensaje2
        {
            get
            {
                return _mensaje2;
            }
            set
            {
                _mensaje2 = value;
            }
        } 

        public SalidaVentaViewModel SalidaVentaViewModel
        {
            get
            {
                return _salidaVentaViewModel;
            }
            set
            {
                _salidaVentaViewModel = value;
            }
        }
        public SalidaRMAViewModel SalidaRMAViewModel
        {
            get
            {
                return _salidaRMAViewModel;
            }
            set
            {
                _salidaRMAViewModel = value;
            }
        }
        public SalidaPruebasViewModel SalidaPruebasViewModel
        {
            get
            {
                return _salidaPruebasViewModel;
            }
            set
            {
                _salidaPruebasViewModel = value;
            }
        }
        public SalidaConfiguracionViewModel SalidaConfiguracionViewModel
        {
            get
            {
                return _salidaConfiguracionViewModel;
            }
            set
            {
                _salidaConfiguracionViewModel = value;
            }
        }
        public SalidaObsequioViewModel SalidaObsequioViewModel
        {
            get
            {
                return _salidaObsequioViewModel;
            }
            set
            {
                _salidaObsequioViewModel = value;
            }
        }
        public SalidaCorrectivoViewModel SalidaCorrectivoViewModel
        {
            get
            {
                return _salidaCorrectivoViewModel;
            }
            set
            {
                _salidaCorrectivoViewModel = value;
            }
        }
        public SalidaOfficeViewModel SalidaOfficeViewModel
        {
            get
            {
                return _salidaOfficeViewModel;
            }
            set
            {
                _salidaOfficeViewModel = value;
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
        public SalidaPrestamoViewModel SalidaPrestamoViewModel
        {
            get
            {
                return _salidaPrestamoViewModel;
            }
            set
            {
                _salidaPrestamoViewModel = value;
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

        #endregion
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
            if (_salidaRentaViewModel != null)
            {
                this.CatalogItemModel.loadItems(_salidaRentaViewModel.MovimientoModel.AlmacenProcedencia, "Rafa estuvo aquí");
            }
            else if(_salidaRevisionViewModel != null)
            {
                this.CatalogItemModel.loadItems(_salidaRevisionViewModel.MovimientoModel.AlmacenProcedencia, "Rafa estuvo aquí");
            } 
			else if (_salidaDemoViewModel != null)
            {
                this.CatalogItemModel.loadItems(_salidaDemoViewModel.MovimientoModel.AlmacenProcedencia, "Rafa estuvo aquí");
            }
            else if (_salidaPrestamoViewModel != null)
            {
                this.CatalogItemModel.loadItems(_salidaPrestamoViewModel.MovimientoModel.AlmacenProcedencia, "Rafa estuvo aquí");
            }
            else if (_salidaVentaViewModel != null)
            {
                this.CatalogItemModel.loadItems(_salidaVentaViewModel.MovimientoModel.AlmacenProcedencia, "Rafa estuvo aquí");
            }
            else if (_salidaRMAViewModel != null)
            {
                this.CatalogItemModel.loadItems(_salidaRMAViewModel.MovimientoModel.AlmacenProcedencia, "Rafa estuvo aquí");
            }
            else if (_salidaPruebasViewModel != null)
            {
                this.CatalogItemModel.loadItems(_salidaPruebasViewModel.MovimientoModel.AlmacenProcedencia, "Rafa estuvo aquí");
            }
            else if (_salidaConfiguracionViewModel != null)
            {
                this.CatalogItemModel.loadItems(_salidaConfiguracionViewModel.MovimientoModel.AlmacenProcedencia, "Rafa estuvo aquí");
            }
            else if (_salidaObsequioViewModel != null)
            {
                this.CatalogItemModel.loadItems(_salidaObsequioViewModel.MovimientoModel.AlmacenProcedencia, "Rafa estuvo aquí");
            }
            else if (_salidaCorrectivoViewModel != null)
            {
                this.CatalogItemModel.loadItems(_salidaCorrectivoViewModel.MovimientoModel.AlmacenProcedencia, "Rafa estuvo aquí");
            }
            else if (_salidaOfficeViewModel != null)
            {
                this.CatalogItemModel.loadItems(_salidaOfficeViewModel.MovimientoModel.AlmacenProcedencia, "Rafa estuvo aquí");
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
            else if (_salidaPrestamoViewModel != null)
            {
                foreach (ItemModel item in this._catalogItemModel.ItemModel)
                {
                    if (item.IsChecked)
                    {
                        bool aux = true;

                        for (int i = 0; i < this._salidaPrestamoViewModel.ItemModel.ItemModel.Count; i++)
                        {
                            if (this._salidaPrestamoViewModel.ItemModel.ItemModel[i].UnidItem == item.UnidItem)
                                aux = false;
                        }

                        if (aux)
                        {
                            item.IsChecked = false;
                            this._salidaPrestamoViewModel.ItemModel.ItemModel.Add(item);
                        }
                    }
                }
                this.SalidaPrestamoViewModel.MovimientoModel.CantidadItems = this.SalidaPrestamoViewModel.ItemModel.ItemModel.Count();
            }
            else if (_salidaVentaViewModel != null)
            {
                foreach (ItemModel item in this._catalogItemModel.ItemModel)
                {
                    if (item.IsChecked)
                    {
                        bool aux = true;

                        for (int i = 0; i < this._salidaVentaViewModel.ItemModel.ItemModel.Count; i++)
                        {
                            if (this._salidaVentaViewModel.ItemModel.ItemModel[i].UnidItem == item.UnidItem)
                                aux = false;
                        }

                        if (aux)
                        {
                            item.IsChecked = false;
                            this._salidaVentaViewModel.ItemModel.ItemModel.Add(item);
                        }
                    }
                }
                this.SalidaVentaViewModel.MovimientoModel.CantidadItems = this.SalidaVentaViewModel.ItemModel.ItemModel.Count();
            }
            else if (_salidaRMAViewModel != null)
            {
                foreach (ItemModel item in this._catalogItemModel.ItemModel)
                {
                    if (item.IsChecked)
                    {
                        bool aux = true;

                        for (int i = 0; i < this._salidaRMAViewModel.ItemModel.ItemModel.Count; i++)
                        {
                            if (this._salidaRMAViewModel.ItemModel.ItemModel[i].UnidItem == item.UnidItem)
                                aux = false;
                        }

                        if (aux)
                        {
                            item.IsChecked = false;
                            this._salidaRMAViewModel.ItemModel.ItemModel.Add(item);
                        }
                    }
                }
                this.SalidaRMAViewModel.MovimientoModel.CantidadItems = this.SalidaRMAViewModel.ItemModel.ItemModel.Count();
            }
            else if (_salidaPruebasViewModel != null)
            {
                foreach (ItemModel item in this._catalogItemModel.ItemModel)
                {
                    if (item.IsChecked)
                    {
                        bool aux = true;

                        for (int i = 0; i < this._salidaPruebasViewModel.ItemModel.ItemModel.Count; i++)
                        {
                            if (this._salidaPruebasViewModel.ItemModel.ItemModel[i].UnidItem == item.UnidItem)
                                aux = false;
                        }

                        if (aux)
                        {
                            item.IsChecked = false;
                            this._salidaPruebasViewModel.ItemModel.ItemModel.Add(item);
                        }
                    }
                }
                this._salidaPruebasViewModel.MovimientoModel.CantidadItems = this._salidaPruebasViewModel.ItemModel.ItemModel.Count();
            }
            else if (_salidaConfiguracionViewModel != null)
            {
                foreach (ItemModel item in this._catalogItemModel.ItemModel)
                {
                    if (item.IsChecked)
                    {
                        bool aux = true;

                        for (int i = 0; i < this._salidaConfiguracionViewModel.ItemModel.ItemModel.Count; i++)
                        {
                            if (this._salidaConfiguracionViewModel.ItemModel.ItemModel[i].UnidItem == item.UnidItem)
                                aux = false;
                        }

                        if (aux)
                        {
                            item.IsChecked = false;
                            this._salidaConfiguracionViewModel.ItemModel.ItemModel.Add(item);
                        }
                    }
                }
                this.SalidaConfiguracionViewModel.MovimientoModel.CantidadItems = this.SalidaConfiguracionViewModel.ItemModel.ItemModel.Count();
            }
            else if (_salidaObsequioViewModel != null)
            {
                foreach (ItemModel item in this._catalogItemModel.ItemModel)
                {
                    if (item.IsChecked)
                    {
                        bool aux = true;

                        for (int i = 0; i < this._salidaObsequioViewModel.ItemModel.ItemModel.Count; i++)
                        {
                            if (this._salidaObsequioViewModel.ItemModel.ItemModel[i].UnidItem == item.UnidItem)
                                aux = false;
                        }

                        if (aux)
                        {
                            item.IsChecked = false;
                            this._salidaObsequioViewModel.ItemModel.ItemModel.Add(item);
                        }
                    }
                }
                this.SalidaObsequioViewModel.MovimientoModel.CantidadItems = this.SalidaObsequioViewModel.ItemModel.ItemModel.Count();
            }
            else if (_salidaCorrectivoViewModel != null)
            {
                foreach (ItemModel item in this._catalogItemModel.ItemModel)
                {
                    if (item.IsChecked)
                    {
                        bool aux = true;

                        for (int i = 0; i < this._salidaCorrectivoViewModel.ItemModel.ItemModel.Count; i++)
                        {
                            if (this._salidaCorrectivoViewModel.ItemModel.ItemModel[i].UnidItem == item.UnidItem)
                                aux = false;
                        }

                        if (aux)
                        {
                            item.IsChecked = false;
                            this._salidaCorrectivoViewModel.ItemModel.ItemModel.Add(item);
                        }
                    }
                }
                this.SalidaCorrectivoViewModel.MovimientoModel.CantidadItems = this.SalidaCorrectivoViewModel.ItemModel.ItemModel.Count();
            }
            else if (_salidaOfficeViewModel != null)
            {
                foreach (ItemModel item in this._catalogItemModel.ItemModel)
                {
                    if (item.IsChecked)
                    {
                        bool aux = true;

                        for (int i = 0; i < this._salidaOfficeViewModel.ItemModel.ItemModel.Count; i++)
                        {
                            if (this._salidaOfficeViewModel.ItemModel.ItemModel[i].UnidItem == item.UnidItem)
                                aux = false;
                        }

                        if (aux)
                        {
                            item.IsChecked = false;
                            this._salidaOfficeViewModel.ItemModel.ItemModel.Add(item);
                        }
                    }
                }
                this.SalidaOfficeViewModel.MovimientoModel.CantidadItems = this.SalidaOfficeViewModel.ItemModel.ItemModel.Count();
            }
        }
        #endregion

        public string PageName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
