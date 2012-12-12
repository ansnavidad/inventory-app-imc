using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;

namespace InventoryApp.ViewModel.CatalogInfraestructura
{
    public class ModifyInfraestructuraViewModel
    {
        #region Fields
        private InfraestructuraModel _infraestructura;
        private RelayCommand _modifyItemCommand;
        private CatalogInfraestructuraViewModel _catalogInfraestructuraViewModel;
        #endregion

        //Exponer la propiedad item status
        #region Props
        public InfraestructuraModel Infraestructura
        {
            get
            {
                return _infraestructura;
            }
            set
            {
                _infraestructura = value;
            }
        }

        public ICommand ModifyItemCommand
        {
            get
            {
                if (_modifyItemCommand == null)
                {
                    _modifyItemCommand = new RelayCommand(p => this.AttempModifyInfraestructura(), p => this.CanAttempModifyInfraestructura());
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
        public ModifyInfraestructuraViewModel(CatalogInfraestructuraViewModel catalogInfraestructuraViewModel, InfraestructuraModel selectedInfraestructuraModel)
        {
            this._infraestructura = new InfraestructuraModel(new InfraestructuraDataMapper());
            this._catalogInfraestructuraViewModel = catalogInfraestructuraViewModel;
            this._infraestructura.UnidInfraestructura = selectedInfraestructuraModel.UnidInfraestructura;
            this._infraestructura.InfraestructuraName = selectedInfraestructuraModel.InfraestructuraName;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempModifyInfraestructura()
        {
            bool _canAddInfraestructura = true;
            if (String.IsNullOrEmpty(this._infraestructura.InfraestructuraName))
                _canAddInfraestructura = false;

            return _canAddInfraestructura;
        }

        public void AttempModifyInfraestructura()
        {
            this._infraestructura.updateInfraestructura();

            //Puede ser que para pruebas unitarias catalogItemStatusViewModel sea nulo ya que
            //es una dependencia inyectada
            if (this._catalogInfraestructuraViewModel != null)
            {
                this._catalogInfraestructuraViewModel.loadItems();
            }
        }
        #endregion

    }
}
