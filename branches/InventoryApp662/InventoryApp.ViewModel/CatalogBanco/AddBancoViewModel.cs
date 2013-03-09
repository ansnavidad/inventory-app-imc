using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Windows.Input;
using InventoryApp.DAL;

namespace InventoryApp.ViewModel.CatalogBanco
{
    public class AddBancoViewModel
    {
        #region Fields
        private BancoModel _banco;
        private RelayCommand _addBancoCommand;
        private CatalogBancoViewModel _catalogBancoViewModel;
        #endregion

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

        public ICommand AddBancoCommand
        {
            get 
            {
                if (_addBancoCommand == null)
                {
                    _addBancoCommand = new RelayCommand(p => this.AttempAddBanco(), p => this.CanAttempAddBanco());
                }
                return _addBancoCommand; 
            }
        }
        #endregion

        #region Constructors

        public AddBancoViewModel(CatalogBancoViewModel catalogBancoViewModel)
        {
            this._banco = new BancoModel(new BancoDataMapper(), catalogBancoViewModel.ActualUser);
            this._catalogBancoViewModel = catalogBancoViewModel;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempAddBanco()
        {
            bool _canAddBanco = true;
            if (String.IsNullOrEmpty(this._banco.BancoName))
                _canAddBanco = false;

            return _canAddBanco;
        }

        public void AttempAddBanco()
        {
            this._banco.saveBanco();

            //Puede ser que para pruebas unitarias catalogItemStatusViewModel sea nulo ya que
            //es una dependencia inyectada
            if (this._catalogBancoViewModel != null)
            {
                this._catalogBancoViewModel.loadItems();
            }
        }
        #endregion
    }
}
