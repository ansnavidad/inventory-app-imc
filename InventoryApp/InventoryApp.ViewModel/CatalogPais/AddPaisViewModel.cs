using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;

namespace InventoryApp.ViewModel.CatalogPais
{
    public class AddPaisViewModel
    {
        #region Fields
        private PaisModel _pais;
        private RelayCommand _addPaisCommand;
        private CatalogPaisViewModel _catalogPaisViewModel;
        #endregion

        //Exponer la propiedad item status
        #region Props
        public PaisModel Pais 
        {
            get
            {
                return _pais;
            }
            set
            {
                _pais = value;
            }
        }

        public ICommand AddPaisCommand
        {
            get 
            {
                if (_addPaisCommand == null)
                {
                    _addPaisCommand = new RelayCommand(p => this.AttempAddPais(), p => this.CanAttempAddPais());
                }
                return _addPaisCommand; 
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Ejecuta la acción del command
        /// </summary>
        /// <param name="catalogItemStatusViewModel"></param>
        public AddPaisViewModel(CatalogPaisViewModel catalogPaisViewModel)
        {
            this._pais = new PaisModel(new PaisDataMapper(), catalogPaisViewModel.ActualUser);
            this._catalogPaisViewModel = catalogPaisViewModel;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempAddPais()
        {
            bool _canAddPais = true;
            if (String.IsNullOrEmpty(this._pais.Iso))
                _canAddPais = false;

            return _canAddPais;
        }

        public void AttempAddPais()
        {
            this._pais.savePais();

            //Puede ser que para pruebas unitarias catalogItemStatusViewModel sea nulo ya que
            //es una dependencia inyectada
            if (this._catalogPaisViewModel != null)
            {
                this._catalogPaisViewModel.loadItems();
            }
        }
        #endregion
    }
}
