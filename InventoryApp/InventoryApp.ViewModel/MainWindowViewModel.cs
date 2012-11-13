using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using InventoryApp.Model;
using InventoryApp.ViewModel.CatalogArticulo;
using System.ComponentModel;
using InventoryApp.ViewModel.CatalogMarca;

namespace InventoryApp.ViewModel
{
    public class MainWindowViewModel:ViewModelBase
    {
        #region Properties
        public MenuModel SelectedMenu
        {
            get { return _SelectedMenu; }
            set
            {
                if (_SelectedMenu != value)
                {
                    _SelectedMenu = value;
                    OnPropertyChanged(SelectedMenuPropertyName);
                }
            }
        }
        private MenuModel _SelectedMenu;
        public const string SelectedMenuPropertyName = "SelectedMenu";

        public MenuViewModel MenuViewModel
        {
            get { return _MenuViewModel; }
            set
            {
                if (_MenuViewModel != value)
                {
                    _MenuViewModel = value;
                    OnPropertyChanged(MenuViewModelPropertyName);
                }
            }
        }
        private MenuViewModel _MenuViewModel;
        public const string MenuViewModelPropertyName = "MenuViewModel";

        /// <summary>
        /// Pila de menus hoja que han sido visitados. Sirve para en un futuro implementar el "back", regresar a la vista anterior
        /// </summary>
        public ObservableCollection<MenuModel> VisitedMenuStack
        {
            get { return _VisitedMenuStack; }
            set
            {
                if (_VisitedMenuStack != value)
                {
                    _VisitedMenuStack = value;
                    OnPropertyChanged(VisitedMenuStackPropertyName);
                }
            }
        }
        private ObservableCollection<MenuModel> _VisitedMenuStack;
        public const string VisitedMenuStackPropertyName = "VisitedMenuStack";

        /// <summary>
        /// La página (control de usuario) que actualmente está visible
        /// </summary>
        public IPageViewModel CurrentPageViewModel
        {
            get { return _CurrentPageViewModel; }
            set
            {
                if (_CurrentPageViewModel != value)
                {
                    _CurrentPageViewModel = value;
                    OnPropertyChanged(CurrentPageViewModelPropertyName);
                }
            }
        }
        private IPageViewModel _CurrentPageViewModel;
        public const string CurrentPageViewModelPropertyName = "CurrentPageViewModel";
        #endregion

        public MainWindowViewModel()
        {
            this._MenuViewModel = new MenuViewModel(this.ChagePage);
        }

        #region Methods
        public void ChagePage(object sender,EventArgs e)
        {
            if (sender!=null)
            {
                this._SelectedMenu = (MenuModel)sender;
                this.CurrentPageViewModel = this.PageFactory(this._MenuViewModel.SelectedMenu);
            }
        }

        /// <summary>
        /// Crea el page correspondiente de acuerdo al menú seleccionado
        /// </summary>
        /// <param name="selectedMenu">Menú seleccionado</param>
        /// <returns>Page creada, si no se crea ninguna, regresa el currentpage</returns>
        private IPageViewModel PageFactory(MenuModel selectedMenu)
        {
            IPageViewModel page = this._CurrentPageViewModel;
            if (this._SelectedMenu != null && this._SelectedMenu.IsLeaf)
            {
                switch (this._SelectedMenu.MenuName)
                {
                    case "Artículos":
                        page = new CatalogArticuloViewModel();
                        break;
                    case "Marca":
                        page = new CatalogMarcaViewModel();
                        break;
                    default:
                        break;
                }
            }
            return page;
        }

        public IPageViewModel GetCurrentPage()
        {
            return this._CurrentPageViewModel;
        }
        #endregion


    }
}
