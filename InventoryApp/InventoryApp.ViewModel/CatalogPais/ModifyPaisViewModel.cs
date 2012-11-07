using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;

namespace InventoryApp.ViewModel.CatalogPais
{
    public class ModifyPaisViewModel
    {
        #region Fields
        private PaisModel _paisModel;
        private RelayCommand _modifyPaisCommand;
        private CatalogPaisViewModel _catalogPaisViewModel;
        #endregion

        //Exponer la propiedad item status
        #region Props
        public PaisModel PaisModel 
        {
            get
            {
                return _paisModel;
            }
            set
            {
                _paisModel = value;
            }
        }

        public ICommand ModifyPaisCommand
        {
            get 
            {
                if (_modifyPaisCommand == null)
                {
                    _modifyPaisCommand = new RelayCommand(p => this.AttempModifyPais(), p => this.CanAttempModifyPais());
                }
                return _modifyPaisCommand; 
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Ejecuta la acción del command
        /// </summary>
        /// <param name="catalogItemStatusViewModel"></param>
        public ModifyPaisViewModel(CatalogPaisViewModel catalogPaisViewModel, PaisModel selectedPaisModel)
        {
            this._paisModel = new PaisModel(new PaisDataMapper());
            this._catalogPaisViewModel = catalogPaisViewModel;
            this._paisModel.UnidPais = selectedPaisModel.UnidPais;
            this._paisModel.Pais = selectedPaisModel.Pais;
            this._paisModel.Iso = selectedPaisModel.Iso;            
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempModifyPais()
        {
            bool _canAddPais = true;
            if (String.IsNullOrEmpty(this._paisModel.Pais))
                _canAddPais = false;

            return _canAddPais;
        }

        public void AttempModifyPais()
        {
            this._paisModel.updatePais();

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
