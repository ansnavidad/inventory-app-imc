using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using InventoryApp.DAL;

namespace InventoryApp.ViewModel.CatalogBanco
{
    public class CatalogBancoViewModel
    {
        private CatalogBancoModel _catalogBancoModel;

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
            BancoModel bancoModel=new BancoModel(new BancoDataMapper());
            if (this._catalogBancoModel != null && this._catalogBancoModel.SelectedBanco != null)
            {
                bancoModel.BancoName = this._catalogBancoModel.SelectedBanco.BANCO_NAME;
                bancoModel.UnidBanco = this._catalogBancoModel.SelectedBanco.UNID_BANCO;
            }
            return new ModifyBancoViewModel(this,bancoModel);
        }
    }
}
