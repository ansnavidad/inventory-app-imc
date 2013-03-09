using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;
using InventoryApp.ViewModel.Historial;

namespace InventoryApp.ViewModel.CatalogTipoPedimento
{
    public class ModifyTipoPedimentoViewModel
    {
        #region Fields
        private TipoPedimentoModel _tipoPedimento;
        private RelayCommand _modifyTipoPedimentoCommand;
        private CatalogTipoPedimentoViewModel _catalogTipoPedimentoViewModel;
        #endregion

        //Exponer la propiedad item status
        #region Props
        public TipoPedimentoModel TipoPedimento 
        {
            get
            {
                return _tipoPedimento;
            }
            set
            {
                _tipoPedimento = value;
            }
        }

        public ICommand ModifyTipoPedimentoCommand
        {
            get 
            {
                if (_modifyTipoPedimentoCommand == null)
                {
                    _modifyTipoPedimentoCommand = new RelayCommand(p => this.AttempModifyTipoPedimento(), p => this.CanAttempModifyTipoPedimento());
                }
                return _modifyTipoPedimentoCommand; 
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Ejecuta la acción del command
        /// </summary>
        /// <param name="catalogItemStatusViewModel"></param>
        public ModifyTipoPedimentoViewModel(CatalogTipoPedimentoViewModel catalogTipoPedimentoViewModel, TipoPedimentoModel selectedTipoPedimento)
        {
            this._tipoPedimento = new TipoPedimentoModel(new TipoPedimentoDataMapper(), catalogTipoPedimentoViewModel.ActualUser);
            this._catalogTipoPedimentoViewModel = catalogTipoPedimentoViewModel;
            this._tipoPedimento.UnidTipoPedimento = selectedTipoPedimento.UnidTipoPedimento;
            this._tipoPedimento.TipoPedimentoName = selectedTipoPedimento.TipoPedimentoName;
            this._tipoPedimento.Regimen = selectedTipoPedimento.Regimen;
            this._tipoPedimento.Nota = selectedTipoPedimento.Nota;
            this._tipoPedimento.Clave = selectedTipoPedimento.Clave;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempModifyTipoPedimento()
        {
            bool _canAddTipoPedimento = true;
            if (String.IsNullOrEmpty(this._tipoPedimento.TipoPedimentoName) || String.IsNullOrEmpty(this._tipoPedimento.Clave) || String.IsNullOrEmpty(this._tipoPedimento.Nota) || String.IsNullOrEmpty(this._tipoPedimento.Regimen))
                _canAddTipoPedimento = false;

            return _canAddTipoPedimento;
        }

        public void AttempModifyTipoPedimento()
        {
            this._tipoPedimento.updateTipoPedimento();

            //Puede ser que para pruebas unitarias catalogItemStatusViewModel sea nulo ya que
            //es una dependencia inyectada
            if (this._catalogTipoPedimentoViewModel != null)
            {
                this._catalogTipoPedimentoViewModel.loadItems();
            }
        }

        public HistorialViewModel CreateHistorialViewModel()
        {
            HistorialViewModel historialViewModel = new HistorialViewModel(this.TipoPedimento);
            return historialViewModel;
        }
        #endregion
    }
}
