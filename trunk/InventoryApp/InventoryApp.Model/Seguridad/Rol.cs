using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InventoryApp.DAL;
using InventoryApp.DAL.Recibo;
using System.Collections.ObjectModel;
using InventoryApp.DAL.POCOS;
using System.ComponentModel;

namespace InventoryApp.Model.Seguridad
{
    public class Rol : ModelBase
    {
        #region Properties

        public struct User
        {
            public long UnidUser
            {
                get { return _UnidUser; }
                set
                {
                    if (_UnidUser != value)
                    {
                        _UnidUser = value;
                        OnPropertyChanged(UnidUserPropertyName);
                    }
                }
            }
            private long _UnidUser;
            public const string UnidUserPropertyName = "UnidUser";

            public string Mail
            {
                get { return _Mail; }
                set
                {
                    if (_Mail != value)
                    {
                        _Mail = value;
                        OnPropertyChanged(MailPropertyName);
                    }
                }
            }
            private string _Mail;
            public const string MailPropertyName = "Mail";

            public bool Actived
            {
                get { return _Actived; }
                set
                {
                    if (_Actived != value)
                    {
                        _Actived = value;
                        OnPropertyChanged(ActivedPropertyName);
                    }
                }
            }
            private bool _Actived;
            public const string ActivedPropertyName = "Actived";

            public bool IsChecked
            {
                get { return _IsChecked; }
                set
                {
                    if (_IsChecked != value)
                    {
                        _IsChecked = value;
                        OnPropertyChanged(IsCheckedPropertyName);
                    }
                }
            }
            private bool _IsChecked;
            public const string IsCheckedPropertyName = "IsChecked";
            
            public event PropertyChangedEventHandler PropertyChanged;
            public void OnPropertyChanged(string propertyName)
            {
                var eh = this.PropertyChanged;
                if (eh != null)
                {
                    eh(this, new PropertyChangedEventArgs(propertyName));
                }
            }
        }

        public struct Menu
        {
            public long UnidMenu
            {
                get { return _UnidMenu; }
                set
                {
                    if (_UnidMenu != value)
                    {
                        _UnidMenu = value;
                        OnPropertyChanged(UnidMenuPropertyName);
                    }
                }
            }
            private long _UnidMenu;
            public const string UnidMenuPropertyName = "UnidMenu";

            public string MenuName
            {
                get { return _MenuName; }
                set
                {
                    if (_MenuName != value)
                    {
                        _MenuName = value;
                        OnPropertyChanged(MenuNamePropertyName);
                    }
                }
            }
            private string _MenuName;
            public const string MenuNamePropertyName = "MenuName";
            
            public bool Actived
            {
                get { return _Actived; }
                set
                {
                    if (_Actived != value)
                    {
                        _Actived = value;
                        OnPropertyChanged(ActivedPropertyName);
                    }
                }
            }
            private bool _Actived;
            public const string ActivedPropertyName = "Actived";

            public event PropertyChangedEventHandler PropertyChanged;
            public void OnPropertyChanged(string propertyName)
            {
                var eh = this.PropertyChanged;
                if (eh != null)
                {
                    eh(this, new PropertyChangedEventArgs(propertyName));
                }
            }
        }

        public ObservableCollection<User> UsuariosCollection
        {
            get { return _UsuariosCollection; }
            set
            {
                if (_UsuariosCollection != value)
                {
                    _UsuariosCollection = value;
                    OnPropertyChanged(UsuariosCollectionPropertyName);
                }
            }
        }
        private ObservableCollection<User> _UsuariosCollection;
        public const string UsuariosCollectionPropertyName = "UsuariosCollection";

        public ObservableCollection<Menu> MenuCollection
        {
            get { return _MenuCollection; }
            set
            {
                if (_MenuCollection != value)
                {
                    _MenuCollection = value;
                    OnPropertyChanged(MenuCollectionPropertyName);
                }
            }
        }
        private ObservableCollection<Menu> _MenuCollection;
        public const string MenuCollectionPropertyName = "MenuCollection";
        
        public long UnidRol
        {
            get { return _UnidRol; }
            set
            {
                if (_UnidRol != value)
                {
                    _UnidRol = value;
                    OnPropertyChanged(UnidRolPropertyName);
                }
            }
        }
        private long _UnidRol;
        public const string UnidRolPropertyName = "UnidRol";

        public bool IsChecked
        {
            get { return _IsChecked; }
            set
            {
                if (_IsChecked != value)
                {
                    _IsChecked = value;
                    OnPropertyChanged(IsCheckedPropertyName);
                }
            }
        }
        private bool _IsChecked;
        public const string IsCheckedPropertyName = "IsChecked";

        public string Name
        {
            get { return _Name; }
            set
            {
                if (_Name != value)
                {
                    _Name = value;
                    OnPropertyChanged(NamePropertyName);
                }
            }
        }
        private string _Name;
        public const string NamePropertyName = "Name";

        public bool IsModified
        {
            get { return _IsModified; }
            set
            {
                if (_IsModified != value)
                {
                    _IsModified = value;
                    OnPropertyChanged(IsModifiedPropertyName);
                }
            }
        }
        private bool _IsModified;
        public const string IsModifiedPropertyName = "IsModified";

        public bool IsActive
        {
            get { return _IsActive; }
            set
            {
                if (_IsActive != value)
                {
                    _IsActive = value;
                    OnPropertyChanged(IsActivePropertyName);
                }
            }
        }
        private bool _IsActive;
        public const string IsActivePropertyName = "IsActive";

        public bool IsSystemRol
        {
            get { return _IsSystemRol; }
            set
            {
                if (_IsSystemRol != value)
                {
                    _IsSystemRol = value;
                    OnPropertyChanged(IsSystemRolPropertyName);
                }
            }
        }
        private bool _IsSystemRol;
        public const string IsSystemRolPropertyName = "IsSystemRol";

        public bool RecibirMails
        {
            get { return _RecibirMails; }
            set
            {
                if (_RecibirMails != value)
                {
                    _RecibirMails = value;
                    OnPropertyChanged(RecibirMailsPropertyName);
                }
            }
        }
        private bool _RecibirMails;
        public const string RecibirMailsPropertyName = "RecibirMails";

        #endregion

        #region Constructors
        
        public Rol(ROL r) {

            this.UnidRol = r.UNID_ROL;
            this.Name = r.ROL_NAME;
            this.IsActive = r.IS_ACTIVE;
            this.IsModified = r.IS_MODIFIED;
            this.IsSystemRol = r.IS_SYSTEM_ROOL;
            this.RecibirMails = r.RECIBIR_MAILS;

            UsuariosCollection = new ObservableCollection<User>();
            MenuCollection = new ObservableCollection<Menu>();

            foreach (USUARIO_ROL ur in r.USUARIO_ROL) { 
            
                User u = new User();

                u.Actived = true;
                u.Mail = ur.USUARIO.USUARIO_MAIL;
                u.UnidUser = ur.USUARIO.UNID_USUARIO;

                UsuariosCollection.Add(u);
            }

            foreach (ROL_MENU rm in r.ROL_MENU)
            {
                Menu m = new Menu();

                m.Actived = true;
                m.MenuName = rm.MENU.MENU_NAME;
                m.UnidMenu = rm.MENU.UNID_MENU;

                MenuCollection.Add(m);
            }
        }

        public Rol()
        {            
            this.Name = "";
            this.IsActive = true;
            this.IsModified = true;

            UsuariosCollection = new ObservableCollection<User>();
            MenuCollection = new ObservableCollection<Menu>();
        }

        #endregion

        #region Methods

        public void saveRol() {

            AppRolDataMapper rolDM = new AppRolDataMapper();
            AppUsuarioRol ar = new AppUsuarioRol();
            AppRolMenuDataMapper am = new AppRolMenuDataMapper();
            AppMenuDataMapper menu = new AppMenuDataMapper();

            ROL rrr = new ROL();
            rrr.IS_ACTIVE = true;
            rrr.IS_MODIFIED = true;
            rrr.IS_SYSTEM_ROOL = this.IsSystemRol;
            rrr.RECIBIR_MAILS = this.RecibirMails;
            rrr.ROL_NAME = this.Name;
            this.UnidRol = UNID.getNewUNID();
            rrr.UNID_ROL = UnidRol;
            rolDM.insertElement(rrr);

            foreach (User u in UsuariosCollection) {
                                
                USUARIO_ROL ur = new USUARIO_ROL();
                ur.UNID_ROL = this.UnidRol;
                ur.UNID_USUARIO = u.UnidUser;
                ar.insertElement(ur);
            }

            foreach (Menu minim in MenuCollection){
            
                MENU mm = new MENU();
                mm.MENU_NAME = minim.MenuName;
                mm = (MENU)menu.getElementByName(mm);
                
                ROL_MENU rm = new ROL_MENU();
                rm.UNID_MENU = mm.UNID_MENU;
                rm.UNID_ROL = this.UnidRol;
                am.insertElement(rm);
            }
        }
        public void modifyRol() {

            AppRolDataMapper RolDM = new AppRolDataMapper();
            AppUsuarioRol Rol_UsuarioDM = new AppUsuarioRol();
            AppRolMenuDataMapper Rol_MenuDM = new AppRolMenuDataMapper();
            AppMenuDataMapper MenuDM = new AppMenuDataMapper();

            Rol_UsuarioDM.deleteElementsByRol(this.UnidRol);
            Rol_MenuDM.deleteElementsByRol(this.UnidRol);

            ROL rrr = new ROL();
            rrr.IS_ACTIVE = true;
            rrr.IS_MODIFIED = true;
            rrr.IS_SYSTEM_ROOL = this.IsSystemRol;
            rrr.RECIBIR_MAILS = this.RecibirMails;
            rrr.ROL_NAME = this.Name;
            rrr.UNID_ROL = UnidRol;
            RolDM.udpateElement(rrr);

            foreach (User u in UsuariosCollection)
            {
                USUARIO_ROL ur = new USUARIO_ROL();
                ur.UNID_ROL = this.UnidRol;
                ur.UNID_USUARIO = u.UnidUser;
                Rol_UsuarioDM.upsertElement(ur);
            }

            foreach (Menu minim in MenuCollection)
            {

                MENU mm = new MENU();
                mm.MENU_NAME = minim.MenuName;
                mm = (MENU)MenuDM.getElementByName(mm);

                ROL_MENU rm = new ROL_MENU();
                rm.UNID_MENU = mm.UNID_MENU;
                rm.UNID_ROL = this.UnidRol;
                Rol_MenuDM.upsertElement(rm);
            }
        }

        public void DeleteRol() {

            AppRolDataMapper rm = new AppRolDataMapper();
            ROL r = new ROL();
            r.UNID_ROL = UnidRol;
            rm.deleteElement(r);
        }

        #endregion
    }
}
