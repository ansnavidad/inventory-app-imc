using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using InventoryApp.DAL;
using InventoryApp.DAL.POCOS;

namespace InventoryApp.Model
{
    public delegate void EventHandler<EventArgs>(object sender, EventArgs e);
    public class MenuModel:ModelBase
    {
        private MenuDataMapper dataMapper;

        #region Properties
        public bool IsLeaf
        {
            get { return _IsLeaf; }
            set
            {
                if (_IsLeaf != value)
                {
                    _IsLeaf = value;
                    OnPropertyChanged(IsLeafPropertyName);
                }
            }
        }
        private bool _IsLeaf;
        public const string IsLeafPropertyName = "IsLeaf";

        public bool IsCheck
        {
            get { return _IsCheck; }
            set
            {
                if (_IsCheck != value)
                {
                    if (value == true)
                        GetActivo();
                    else
                        GetDesactivo();

                    _IsCheck = value;
                    OnPropertyChanged(IsCheckPropertyName);
                }
            }
        }
        private bool _IsCheck;
        public const string IsCheckPropertyName = "IsCheck";

        public bool IsCollapsed
        {
            get { return _IsCollapsed; }
            set
            {
                if (_IsCollapsed != value)
                {
                    _IsCollapsed = value;
                    OnPropertyChanged(IsCollapsedPropertyName);
                }
            }
        }
        private bool _IsCollapsed;
        public const string IsCollapsedPropertyName = "IsCollapsed";

        public bool IsExpanded
        {
            get { return _IsExpanded; }
            set
            {
                if (_IsExpanded != value)
                {
                    _IsExpanded = value;
                    OnPropertyChanged(IsExpandedPropertyName);
                    
                }
            }
        }
        private bool _IsExpanded;
        public const string IsExpandedPropertyName = "IsExpanded";

        public bool IsSelected
        {
            get { return _IsSelected; }
            set
            {
                if (_IsSelected != value)
                {
                    _IsSelected = value;
                    OnPropertyChanged(IsSelectedPropertyName);
                    this.OnSelectedItemChanged();
                }
            }
        }
        private bool _IsSelected;
        public const string IsSelectedPropertyName = "IsSelected";

        public MenuModel Parent
        {
            get { return _Parent; }
            set
            {
                if (_Parent != value)
                {
                    _Parent = value;
                }
            }
        }
        private MenuModel _Parent;


        public string MenuName
        {
            get { return _MenuName; }
            set
            {
                if (_MenuName != value)
                {
                    _MenuName = value;
                }
            }
        }
        private string _MenuName;

        public ObservableCollection<MenuModel> ChildrenMenu
        {
            get { return _ChildrenMenu; }
            set
            {
                if (_ChildrenMenu != value)
                {
                    _ChildrenMenu = value;
                }
            }
        }
        private ObservableCollection<MenuModel> _ChildrenMenu; 
        #endregion

        #region Constructors
        public MenuModel(IDataMapper menuDataMapper,EventHandler<EventArgs> e)
        {
            MenuDataMapper mdm = menuDataMapper as MenuDataMapper;
            if (mdm != null)
            {
                //Obtener el root del árbol
                DAL.POCOS.MENU menu = (DAL.POCOS.MENU)mdm.getElements();
                this._Parent = null;
                if (menu != null)
                {
                    this.MenuName = menu.MENU_NAME;
                    this.dataMapper = mdm;
                    this.SelectedItemChanged += e;
                    this.GetChildren(menu, null);
                }
            }
        }

        public MenuModel(IDataMapper menuDataMapper, EventHandler<EventArgs> e, bool b)
        {
            MenuDataMapper mdm = menuDataMapper as MenuDataMapper;
            if (mdm != null)
            {
                //Obtener el root del árbol
                DAL.POCOS.MENU menu = (DAL.POCOS.MENU)mdm.getElements();
                this._Parent = null;
                if (menu != null)
                {
                    this.MenuName = menu.MENU_NAME;
                    this.dataMapper = mdm;
                    this.SelectedItemChanged += e;
                    this.GetChildren(menu, null, b);
                }
            }
        }

        public MenuModel(IDataMapper menuDataMapper, EventHandler<EventArgs> e, USUARIO ActualUser)
        {
            List<long> UnidsMenu = new List<long>();

            foreach (USUARIO_ROL ur in ActualUser.USUARIO_ROL) {

                foreach (ROL_MENU rm in ur.ROL.ROL_MENU) {

                    UnidsMenu.Add(rm.UNID_MENU);
                }                
            }

            MenuDataMapper mdm = menuDataMapper as MenuDataMapper;
            if (mdm != null)
            {
                //Obtener el root del árbol
                DAL.POCOS.MENU menu = (DAL.POCOS.MENU)mdm.getElements();
                this._Parent = null;
                if (menu != null && UnidsMenu.Contains(menu.UNID_MENU))
                {
                    this.MenuName = menu.MENU_NAME;
                    this.dataMapper = mdm;
                    this.SelectedItemChanged += e;
                    this.GetChildren(menu, null, UnidsMenu);
                }
            }
        }

        public MenuModel(MENU menu, MenuModel parent, IDataMapper menuDataMapper)
        {
            MenuDataMapper mdm = menuDataMapper as MenuDataMapper;
            this.dataMapper = mdm;
            this.GetChildren(menu, parent);
        }

        public MenuModel(MENU menu, MenuModel parent, IDataMapper menuDataMapper, bool b)
        {
            MenuDataMapper mdm = menuDataMapper as MenuDataMapper;
            this.dataMapper = mdm;
            this.GetChildren(menu, parent, b);
        }

        public MenuModel(MENU menu, MenuModel parent, IDataMapper menuDataMapper, List<long> UnidsMenu)
        {
            MenuDataMapper mdm = menuDataMapper as MenuDataMapper;
            this.dataMapper = mdm;
            this.GetChildren(menu, parent, UnidsMenu);
        } 
        #endregion

        private void GetChildren(MENU menu, MenuModel parent)
        {
            this._ChildrenMenu = new ObservableCollection<MenuModel>();
            this._Parent = parent;
            if (menu != null)
            {
                this._MenuName = menu.MENU_NAME;
                this._IsExpanded = this._Parent == null ? true : false;
                this._IsCheck = false;
                this._IsSelected = false;
                this._IsCollapsed = this._Parent==null?false:false;
                this._IsLeaf = menu.IS_LEAF;
                if (this._Parent != null && this._Parent.SelectedItemChanged != null)
                {
                    this.SelectedItemChanged += (EventHandler<EventArgs>)this._Parent.SelectedItemChanged;
                }
                List<MENU> res = this.dataMapper.getElement(menu) as List<MENU>;

                if (res != null)
                {
                    (from o in res
                     select o).ToList<MENU>().ForEach(o => this._ChildrenMenu.Add(new MenuModel(o, this,this.dataMapper)));
                }
            }
        }

        private void GetChildren(MENU menu, MenuModel parent, bool b)
        {
            this._ChildrenMenu = new ObservableCollection<MenuModel>();
            this._Parent = parent;
            if (menu != null)
            {
                this._MenuName = menu.MENU_NAME;
                this._IsExpanded = this._Parent == null ? true : false;
                this._IsCheck = false;
                this._IsSelected = false;
                this._IsCollapsed = this._Parent == null ? false : false;
                this._IsLeaf = menu.IS_LEAF;
                if (this._Parent != null && this._Parent.SelectedItemChanged != null)
                {
                    this.SelectedItemChanged += (EventHandler<EventArgs>)this._Parent.SelectedItemChanged;
                }
                List<MENU> res = this.dataMapper.getElement(menu, b) as List<MENU>;

                if (res != null)
                {
                    (from o in res
                     select o).ToList<MENU>().ForEach(o => this._ChildrenMenu.Add(new MenuModel(o, this, this.dataMapper, b)));
                }
            }
        }

        private void GetChildren(MENU menu, MenuModel parent, List<long> UnidsMenu)
        {
            this._ChildrenMenu = new ObservableCollection<MenuModel>();
            this._Parent = parent;
            if (menu != null)
            {
                this._MenuName = menu.MENU_NAME;
                this._IsExpanded = this._Parent == null ? true : false;
                this._IsSelected = false;
                this._IsCollapsed = this._Parent == null ? false : false;
                this._IsLeaf = menu.IS_LEAF;
                if (this._Parent != null && this._Parent.SelectedItemChanged != null)
                {
                    this.SelectedItemChanged += (EventHandler<EventArgs>)this._Parent.SelectedItemChanged;
                }
                List<MENU> res = this.dataMapper.getElement(menu, UnidsMenu) as List<MENU>;

                if (res != null)
                {
                    (from o in res
                     select o).ToList<MENU>().ForEach(o => this._ChildrenMenu.Add(new MenuModel(o, this, this.dataMapper, UnidsMenu)));
                }
            }
        }
        
        public void GetActivo()
        {
            foreach (var item in this.ChildrenMenu)
            {
                string name = item.MenuName;
                if (item.IsCheck ==false)
                {
                    item.IsCheck = true;
                    GetActivo();
                }
            }
        }

        public void GetDesactivo()
        {
            foreach (var item in this.ChildrenMenu)
            {
                string name = item.MenuName;
                if (item.IsCheck == true)
                {
                    item.IsCheck = false;
                    GetDesactivo();
                }
            }
        }

        public event EventHandler<EventArgs> SelectedItemChanged;
        public void OnSelectedItemChanged()
        {
            //if (SelectedItemChanged != null && this._IsLeaf && this._IsSelected)
            if (SelectedItemChanged != null && this._IsSelected)
            {
                SelectedItemChanged(this, new EventArgs());
            }
        }
    }
}
