using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;

namespace InventoryApp.ViewModel.CatalogTipoPedimento
{
    public class AddTipoPedimentoViewModel
    {
        #region Fields
        private TipoPedimentoModel _tipoPedimento;
        private RelayCommand _addTipoPedimentoCommand;
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

        public ICommand AddTipoPedimentoCommand
        {
            get 
            {
                if (_addTipoPedimentoCommand == null)
                {
                    _addTipoPedimentoCommand = new RelayCommand(p => this.AttempAddTipoPedimento(), p => this.CanAttempAddTipoPedimento());
                }
                return _addTipoPedimentoCommand; 
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Ejecuta la acción del command
        /// </summary>
        /// <param name="catalogItemStatusViewModel"></param>
        public AddTipoPedimentoViewModel(CatalogTipoPedimentoViewModel catalogTipoPedimentoViewModel)
        {
            this._tipoPedimento = new TipoPedimentoModel(new TipoPedimentoDataMapper(), catalogTipoPedimentoViewModel.ActualUser);
            this._catalogTipoPedimentoViewModel = catalogTipoPedimentoViewModel;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempAddTipoPedimento()
        {
            bool _canAddTipoPedimento = true;
            if (String.IsNullOrEmpty(this._tipoPedimento.TipoPedimentoName) || String.IsNullOrEmpty(this._tipoPedimento.Clave) || String.IsNullOrEmpty(this._tipoPedimento.Nota) || String.IsNullOrEmpty(this._tipoPedimento.Regimen))
                _canAddTipoPedimento = false;

            return _canAddTipoPedimento;
        }

        public void AttempAddTipoPedimento()
        {
            this._tipoPedimento.saveTipoPedimento();

            //Puede ser que para pruebas unitarias catalogItemStatusViewModel sea nulo ya que
            //es una dependencia inyectada
            if (this._catalogTipoPedimentoViewModel != null)
            {
                this._catalogTipoPedimentoViewModel.loadItems();
            }
        }
        #endregion
    }
}
