using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;
using InventoryApp.ViewModel.Historial;


namespace InventoryApp.ViewModel.CatalogModelo
{
    public class ModifyModeloViewModel
    {
        #region Fields
        private ModeloModel _modelo;
        private RelayCommand _modifyItemCommand;
        private CatalogModeloViewModel _catalogModeloViewModel;
        #endregion

        //Exponer la propiedad item status
        #region Props
        public ModeloModel Modelo 
        {
            get
            {
                return _modelo;
            }
            set
            {
                _modelo = value;
            }
        }

        public ICommand ModifyItemCommand
        {
            get 
            {
                if (_modifyItemCommand == null)
                {
                    _modifyItemCommand = new RelayCommand(p => this.AttempModifyModelo(), p => this.CanAttempModifyModelo());
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
        public ModifyModeloViewModel(CatalogModeloViewModel catalogModeloViewModel, ModeloModel selectedModeloModel)
        {
            this._modelo = new ModeloModel(new ModeloDataMapper(), catalogModeloViewModel.ActualUser);
            this._catalogModeloViewModel = catalogModeloViewModel;
            this._modelo.UnidModelo = selectedModeloModel.UnidModelo;
            this._modelo.ModeloName = selectedModeloModel.ModeloName;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempModifyModelo()
        {
            bool _canAddModelo = true;
            if (String.IsNullOrEmpty(this._modelo.ModeloName))
                _canAddModelo = false;

            return _canAddModelo;
        }

        public void AttempModifyModelo()
        {
            this._modelo.updateModelo();

            //Puede ser que para pruebas unitarias catalogItemStatusViewModel sea nulo ya que
            //es una dependencia inyectada
            if (this._catalogModeloViewModel != null)
            {
                this._catalogModeloViewModel.loadItems();
            }
        }

        public HistorialViewModel CreateHistorialViewModel()
        {
            HistorialViewModel historialViewModel = new HistorialViewModel(this.Modelo);
            return historialViewModel;
        }
        #endregion
    }
}
