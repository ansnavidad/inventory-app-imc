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

        public MenuModel(MENU menu, MenuModel parent, IDataMapper menuDataMapper)
        {
            MenuDataMapper mdm = menuDataMapper as MenuDataMapper;
            this.dataMapper = mdm;
            this.GetChildren(menu, parent);
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
