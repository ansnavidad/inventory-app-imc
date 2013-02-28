using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;
using InventoryApp.ViewModel.CatalogArticulo;

namespace InventoryApp.ViewModel.CatalogEquipo
{
    public class AddEquipoViewModel
    {
        #region Fields
        private EquipoModel _equipo;
        private RelayCommand _addItemCommand;
        private CatalogEquipoViewModel _catalogEquipoViewModel;
        private AddArticuloViewModel _artVM;
        private ModifyArticuloViewModel _artMod;
        #endregion

        //Exponer la propiedad item status
        #region Props
        public EquipoModel Equipo 
        {
            get
            {
                return _equipo;
            }
            set
            {
                _equipo = value;
            }
        }

        public ICommand AddItemCommand
        {
            get 
            {
                if (_addItemCommand == null)
                {
                    _addItemCommand = new RelayCommand(p => this.AttempAddEquipo(), p => this.CanAttempAddEquipo());
                }
                return _addItemCommand; 
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Ejecuta la acción del command
        /// </summary>
        /// <param name="catalogItemStatusViewModel"></param>
        public AddEquipoViewModel(CatalogEquipoViewModel catalogEquipoViewModel)
        {
            this._equipo = new EquipoModel(new EquipoDataMapper(), catalogEquipoViewModel.ActualUser);
            this._catalogEquipoViewModel = catalogEquipoViewModel;
        }

        public AddEquipoViewModel(CatalogEquipoViewModel catalogEquipoViewModel, AddArticuloViewModel artVM)
        {
            _artVM = artVM;
            this._equipo = new EquipoModel(new EquipoDataMapper(), artVM.Articulo.ActualUser);
            this._catalogEquipoViewModel = catalogEquipoViewModel;
        }

        public AddEquipoViewModel(CatalogEquipoViewModel catalogEquipoViewModel, ModifyArticuloViewModel artVM)
        {
            _artMod = artVM;
            this._equipo = new EquipoModel(new EquipoDataMapper(), artVM.Articulo.ActualUser);
            this._catalogEquipoViewModel = catalogEquipoViewModel;
        }

        #endregion

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempAddEquipo()
        {
            bool _canAddEquipo = true;
            if (String.IsNullOrEmpty(this._equipo.EquipoName))
                _canAddEquipo = false;

            return _canAddEquipo;
        }

        public void AttempAddEquipo()
        {
            this._equipo.saveEquipo();

            //Puede ser que para pruebas unitarias catalogItemStatusViewModel sea nulo ya que
            //es una dependencia inyectada
            if (this._catalogEquipoViewModel != null)
            {
                this._catalogEquipoViewModel.loadItems();
            }

            if (_artVM != null)
            {
                _artVM.CatalogEquipoModel.loadItems();
                _artVM.Articulo.Equipo = _artVM.CatalogEquipoModel.Equipos[_artVM.CatalogEquipoModel.Equipos.Count - 1];
                //_artVM.CatalogEquipoModel.SelectedEquipo = _artVM.CatalogEquipoModel.Equipos[0];
            }

            if (_artMod != null)
            {
                _artMod.CatalogEquipoModel.loadItems();
                _artMod.Articulo.Equipo = _artMod.CatalogEquipoModel.Equipos[_artMod.CatalogEquipoModel.Equipos.Count - 1];
                //_artVM.CatalogEquipoModel.SelectedEquipo = _artVM.CatalogEquipoModel.Equipos[0];
            }
        }
        #endregion
    }
}
