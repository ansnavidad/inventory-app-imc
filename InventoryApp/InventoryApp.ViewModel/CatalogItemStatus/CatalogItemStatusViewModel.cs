using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using InventoryApp.DAL;
using System.Windows.Input;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.ViewModel.CatalogItemStatus
{
    public class CatalogItemStatusViewModel : IPageViewModel
    {
        #region Fields
        private RelayCommand _deleteItemCommand;
        private CatalogItemStatusModel _catalogItemStatusModel;
        public USUARIO ActualUser;
        #endregion

        //Exponer la propiedad item status
        #region Props
        
        public ICommand DeleteItemCommand
        {
            get
            {
                if (_deleteItemCommand == null)
                {
                    _deleteItemCommand = new RelayCommand(p => this.AttempDeleteItemStatus(), p => this.CanAttempDeleteItemStatus());
                }
                return _deleteItemCommand;
            }
        }

        public CatalogItemStatusModel CatalogItemStatusModel
        {
            get
            {
                return _catalogItemStatusModel;
            }
            set
            {
                _catalogItemStatusModel = value;
            }
        }
        #endregion

        #region Contructor
        public CatalogItemStatusViewModel()
        {            
            try
            {
                IDataMapper dataMapper = new ItemStatusDataMapper();
                this._catalogItemStatusModel = new CatalogItemStatusModel(dataMapper);   
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

        public CatalogItemStatusViewModel(USUARIO u)
        {
            try
            {
                IDataMapper dataMapper = new ItemStatusDataMapper();
                this._catalogItemStatusModel = new CatalogItemStatusModel(dataMapper);
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
        
        #endregion

        public void loadItems()
        {
            this._catalogItemStatusModel.loadItems();
        }

        /// <summary>
        /// Crea una nueva instancia de addItemStatus y se pasa asi mismo como parámetro
        /// Referenca pag 232 del libro MVVM
        /// </summary>
        /// <returns></returns>
        public AddItemStatusViewModel CreateAddItemStatusViewModel()
        {
            return new AddItemStatusViewModel(this);
        }

        /// <summary>
        /// Crea una nueva instancia de ModifyItemStatus y se pasa asi mismo como parámetro y el item seleccionado
        /// </summary>
        /// <returns></returns>
        public ModifyItemStatusViewModel CreateModifyItemStatusViewModel()
        {
            ItemStatusModel itemStatusModel=new ItemStatusModel(new ItemStatusDataMapper());
            if (this._catalogItemStatusModel != null && this._catalogItemStatusModel.SelectedItemStatus != null)
            {
                itemStatusModel.ItemStatusName = this._catalogItemStatusModel.SelectedItemStatus.ITEM_STATUS_NAME;
                itemStatusModel.UnidItemStatus = this._catalogItemStatusModel.SelectedItemStatus.UNID_ITEM_STATUS;
            }
            return new ModifyItemStatusViewModel(this,itemStatusModel);
        }
        
        #region Methods
        /// <summary>
        /// Hace las validaciones necesarias para habilitar el command
        /// Si esta función retorna false, el command es deshabilitado
        /// </summary>
        /// <returns></returns>
        public bool CanAttempDeleteItemStatus()
        {
            bool _canDeleteItemStatus =false;
            foreach (DeleteItemStatus d in this._catalogItemStatusModel.ItemStatus)
            {
                if (d.IsChecked==true)
                {
                    _canDeleteItemStatus = true;
                }
            }
            
            return _canDeleteItemStatus;
        }
        public void AttempDeleteItemStatus()
        {
            this._catalogItemStatusModel.deleteItem();
            
            //Puede ser que para pruebas unitarias catalogItemStatusViewModel sea nulo ya quef
            if (this._catalogItemStatusModel != null)
            {
                this._catalogItemStatusModel.loadItems();
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
