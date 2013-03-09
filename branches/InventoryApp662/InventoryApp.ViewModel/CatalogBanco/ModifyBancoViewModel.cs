using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;
using InventoryApp.ViewModel.Historial;

namespace InventoryApp.ViewModel.CatalogBanco
{
    public class ModifyBancoViewModel
    {
        #region Fields
        private BancoModel _banco;
        private RelayCommand _modifyBancoCommand;
        private CatalogBancoViewModel _catalogBancoViewModel;
        #endregion

        //Exponer la propiedad Banco
        #region Props
        public BancoModel Banco 
        {
            get
            {
                return _banco;
            }
            set
            {
                _banco = value;
            }
        }

        public ICommand ModifyBancoCommand
        {
            get 
            {
                if (_modifyBancoCommand == null)
                {
                    _modifyBancoCommand = new RelayCommand(p => this.AttempModifyBanco(), p => this.CanAttempModifyBanco());
                }
                return _modifyBancoCommand; 
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Ejecuta la acción del command
        /// </summary>
        /// <param name="catalogItemStatusViewModel"></param>
        public ModifyBancoViewModel(CatalogBancoViewModel catalogBancoViewModel,BancoModel selectedBancoModel)
        {
            this._banco = new BancoModel(new BancoDataMapper(), catalogBancoViewModel.ActualUser);
            this._catalogBancoViewModel = catalogBancoViewModel;
            this._banco.UnidBanco = selectedBancoModel.UnidBanco;
            this._banco.BancoName = selectedBancoModel.BancoName;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempModifyBanco()
        {
            bool _canAddBanco = true;
            if (String.IsNullOrEmpty(this._banco.BancoName))
                _canAddBanco = false;

            return _canAddBanco;
        }

         public void AttempModifyBanco()
        {
            this._banco.updateBanco();

            //Puede ser que para pruebas unitarias catalogItemStatusViewModel sea nulo ya que
            //es una dependencia inyectada
            if (this._catalogBancoViewModel != null)
            {
                this._catalogBancoViewModel.loadItems();
            }
        }

         public HistorialViewModel CreateHistorialViewModel()
         {
             HistorialViewModel historialViewModel = new HistorialViewModel(this.Banco);
             return historialViewModel;
         }

        #endregion
    }
}
