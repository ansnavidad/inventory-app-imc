using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using InventoryApp.DAL;
using System.Windows.Input;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.ViewModel.CatalogBanco
{
    public class CatalogBancoViewModel:IPageViewModel
    {
        private RelayCommand _deleteBancoCommand;
        private CatalogBancoModel _catalogBancoModel;
        public USUARIO ActualUser;

        public ICommand DeleteBancoCommand
        {
            get
            {
                if (_deleteBancoCommand == null)
                {
                    _deleteBancoCommand = new RelayCommand(p => this.AttempDeleteBanco(), p => this.CanAttempDeleteBanco());
                }
                return _deleteBancoCommand;
            }
        }

        public CatalogBancoViewModel(USUARIO u)
        {
            try
            {
                IDataMapper dataMapper = new BancoDataMapper();
                this._catalogBancoModel = new CatalogBancoModel(dataMapper);
                this.ActualUser = u;
            }
            catch (ArgumentException a)
            {

                ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CatalogBancoViewModel()
        {            
            try
            {
                IDataMapper dataMapper = new BancoDataMapper();
                this._catalogBancoModel = new CatalogBancoModel(dataMapper);   
            }
            catch (ArgumentException a)
            {

                ;
            }
            catch(Exception ex)
            {
                throw ex;
            }            
        }
        public CatalogBancoModel CatalogBancoModel
        {
            get
            {
                return _catalogBancoModel;
            }
            set
            {
                _catalogBancoModel = value;
            }
        }

        public void loadItems()
        {
            this._catalogBancoModel.loadBancos();
        }

        /// <summary>
        /// Crea una nueva instancia de addItemStatus y se pasa asi mismo como parámetro
        /// Referenca pag 232 del libro MVVM
        /// </summary>
        /// <returns></returns>
        public AddBancoViewModel CreateAddBancoViewModel()
        {
            return new AddBancoViewModel(this);
        }

        /// <summary>
        /// Crea una nueva instancia de ModifyItemStatus y se pasa asi mismo como parámetro y el item seleccionado
        /// </summary>
        /// <returns></returns>
        public ModifyBancoViewModel CreateBancoViewModel()
        {
            BancoModel bancoModel=new BancoModel(new BancoDataMapper(), this.ActualUser);
            if (this._catalogBancoModel != null && this._catalogBancoModel.SelectedBanco != null)
            {
                bancoModel.BancoName = this._catalogBancoModel.SelectedBanco.BANCO_NAME;
                bancoModel.UnidBanco = this._catalogBancoModel.SelectedBanco.UNID_BANCO;
            }
            return new ModifyBancoViewModel(this,bancoModel);
        }

        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempDeleteBanco()
        {
            bool _canDeleteBanco = false;
            foreach (DeleteBanco d in this._catalogBancoModel.Banco)
            {
                if (d.IsChecked == true)
                {
                    _canDeleteBanco = true;
                }
            }
            
            return _canDeleteBanco;
        }
        public void AttempDeleteBanco()
        {
            this._catalogBancoModel.deleteBancos(this.ActualUser);

            //Puede ser que para pruebas unitarias catalogItemStatusViewModel sea nulo ya quef
            if (this._catalogBancoModel != null)
            {
                this._catalogBancoModel.loadBancos();
            }
        }
        #endregion

        public string PageName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
