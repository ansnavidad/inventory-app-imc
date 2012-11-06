using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;

namespace InventoryApp.ViewModel.CatalogMarca
{
    public class ModifyMarcaViewModel
    {
        #region Fields
        private MarcaModel _marca;
        private RelayCommand _modifyItemCommand;
        private CatalogMarcaViewModel _catalogMarcaViewModel;
        #endregion

        //Exponer la propiedad item status
        #region Props
        public MarcaModel Marca 
        {
            get
            {
                return _marca;
            }
            set
            {
                _marca = value;
            }
        }

        public ICommand ModifyItemCommand
        {
            get 
            {
                if (_modifyItemCommand == null)
                {
                    _modifyItemCommand = new RelayCommand(p => this.AttempModifyMarca(), p => this.CanAttempModifyMarca());
                }
                return _modifyItemCommand; 
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Ejecuta la acción del command
        /// </summary>
        /// <param name="catalogItemStatusViewModel"></param>
        public ModifyMarcaViewModel(CatalogMarcaViewModel catalogMarcaViewModel, MarcaModel selectedMarcaModel)
        {
            this._marca = new MarcaModel(new MarcaDataMapper());
            this._catalogMarcaViewModel = catalogMarcaViewModel;
            this._marca.UnidMarca = selectedMarcaModel.UnidMarca;
            this._marca.MarcaName = selectedMarcaModel.MarcaName;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempModifyMarca()
        {
            bool _canAddMarca = true;
            if (String.IsNullOrEmpty(this._marca.MarcaName))
                _canAddMarca = false;

            return _canAddMarca;
        }

        public void AttempModifyMarca()
        {
            this._marca.updateMarca();

            //Puede ser que para pruebas unitarias catalogItemStatusViewModel sea nulo ya que
            //es una dependencia inyectada
            if (this._catalogMarcaViewModel != null)
            {
                this._catalogMarcaViewModel.loadItems();
            }
        }
        #endregion
    }
}
