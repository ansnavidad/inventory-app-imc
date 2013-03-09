using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.Model;
using System.Collections.ObjectModel;
using InventoryApp.DAL;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.ViewModel.CatalogSeguridad
{
    public class SelectMenuViewModel: ViewModelBase
    {
        #region Properties
        public MenuModel SelectedMenu
        {
            get { return _SelectedItem; }
            set
            {
                if (_SelectedItem != value)
                {
                    _SelectedItem = value;
                    OnPropertyChanged(SelectedItemPropertyName);
                }
            }
        }
        private MenuModel _SelectedItem;
        public const string SelectedItemPropertyName = "SelectedMenu";

        public ObservableCollection<MenuModel> MenuModel
        {
            get { return _MenuModel; }
            set
            {
                if (_MenuModel != value)
                {
                    _MenuModel = value;
                    OnPropertyChanged(MenuModelPropertyName);
                }
            }
        }
        private ObservableCollection<MenuModel> _MenuModel;
        public const string MenuModelPropertyName = "MenuModel"; 
        #endregion

        public SelectMenuViewModel():this(null)
        {      
        }

        public SelectMenuViewModel(InventoryApp.Model.EventHandler<EventArgs> selectItemChanged)
        {
            this.init(selectItemChanged);
        }

        public SelectMenuViewModel(InventoryApp.Model.EventHandler<EventArgs> selectItemChanged, USUARIO ActualUser)
        {
            this.init(selectItemChanged, ActualUser);
        }

        private void init(InventoryApp.Model.EventHandler<EventArgs> selectItemChanged)
        {
            MenuModel rootMenu = null;
            try
            {
                rootMenu = new MenuModel(new MenuDataMapper(), selectItemChanged);
            }
            catch (ArgumentException ae)
            {
                ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            this._MenuModel = new ObservableCollection<MenuModel>();
            this._MenuModel.Add(rootMenu);
        }

        private void init(InventoryApp.Model.EventHandler<EventArgs> selectItemChanged, USUARIO ActualUser)
        {
            MenuModel rootMenu = null;
            try
            {
                rootMenu = new MenuModel(new MenuDataMapper(), selectItemChanged, ActualUser);
            }
            catch (ArgumentException ae)
            {
                ;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            this._MenuModel = new ObservableCollection<MenuModel>();
            this._MenuModel.Add(rootMenu);
        }
    }
}
